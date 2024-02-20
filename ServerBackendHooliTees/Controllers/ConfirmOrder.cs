using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerBackendHooliTees.Models.Database;
using ServerBackendHooliTees.Models.Database.Entities;
using ServerBackendHooliTees.Services;
using System.Numerics;
using Nethereum.Web3;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.TransactionReceipts;

namespace ServerBackendHooliTees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfirmOrder : ControllerBase
    {

        private const string OUR_WALLET = "0x123D68808120295662d794753E3519202B1F1Fc2";
        private const string NETWORK_URL = "https://rpc.sepolia.org";

        private readonly MyDbContext _hooliteesDataBase;

        public ConfirmOrder(MyDbContext hooliteesDataBase)
        {
            _hooliteesDataBase = hooliteesDataBase;
        }

        [HttpPost("buyProducts")]
        public async Task<TransactionToSing> BuyAsync([FromForm] string clientWallet, int totalPrice, int userID)
        {
            CartProducts cartProducts = _hooliteesDataBase.CartProducts
                .FirstOrDefault(id => id.ShoppingCartId  == userID);

            using CoincGeckoApi coincGeckoApi = new CoincGeckoApi();
            decimal ethereumEur = await coincGeckoApi.GetEthereumPriceAsync();
            BigInteger priceWei = Web3.Convert.ToWei(totalPrice / ethereumEur);

            Web3 web3 = new Web3(NETWORK_URL);

            TransactionToSing transactionToSing = new TransactionToSing()
            {
                From = clientWallet,
                To = OUR_WALLET,
                Value = new HexBigInteger(priceWei).HexValue,
                Gas = new HexBigInteger(30000).HexValue,
                GasPrice = (await web3.Eth.GasPrice.SendRequestAsync()).HexValue
            };

            Orders orders = new Orders()
            {
                Id = _hooliteesDataBase.Orders.Count(),
                ClientWallet = transactionToSing.From,
                TotalPrice = transactionToSing.Value,

            };

            _hooliteesDataBase.Orders.Add(orders);
            transactionToSing.Id = orders.Id;


            return transactionToSing;
        }


        [HttpPost("check/{transactionID}")]
        public async Task<bool> CheckTransactionAsync(int transactionId, [FromBody] string txHash)
        {
            bool success = false;
            Orders order = _hooliteesDataBase.Orders.FirstOrDefault(id => id.Id == transactionId);
            order.Hash = txHash;

            Web3 web3 = new Web3(NETWORK_URL);
            var receiptPollingService = new TransactionReceiptPollingService(
                web3.TransactionManager, 1000);

            try
            {
                // Esperar a que la transacción se confirme en la cadena de bloques
                var transactionReceipt = await receiptPollingService.PollForReceiptAsync(txHash);

                // Obtener los datos de la transacción
                var transactionEth = await web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(txHash);

                Console.WriteLine(transactionEth.TransactionHash == transactionReceipt.TransactionHash);
                Console.WriteLine(transactionReceipt.Status.Value == 1);
                Console.WriteLine(transactionReceipt.TransactionHash == order.Hash);
                Console.WriteLine(transactionReceipt.From == order.ClientWallet);
                Console.WriteLine(transactionReceipt.To.Equals(OUR_WALLET, StringComparison.OrdinalIgnoreCase));

                success = transactionEth.TransactionHash == transactionReceipt.TransactionHash
                    && transactionReceipt.Status.Value == 1 // Transacción realizada con éxito
                    && transactionReceipt.TransactionHash == order.Hash // El hash es el mismo
                    && transactionReceipt.From == order.ClientWallet // El dinero viene del cliente
                    && transactionReceipt.To.Equals(OUR_WALLET, StringComparison.OrdinalIgnoreCase); // El dinero se ingresa en nuestra cuenta
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al esperar la transacción: {ex.Message}");
            }

            order.Status = success;

            return success;
        }

    }
}
