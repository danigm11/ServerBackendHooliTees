using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerBackendHooliTees.Models.Database;
using ServerBackendHooliTees.Models.Database.Entities;


namespace ServerBackendHooliTees.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private MyDbContext _dbContextHoolitees;

    public TransactionController(MyDbContext dbContextHoolitees)
    {
        _dbContextHoolitees = dbContextHoolitees;
    }

    [HttpGet("productosHistorial")]
    public IEnumerable<ProductOrder> GetProductosHistorial()
    {
        return _dbContextHoolitees.ProductOrders;
    }

    [HttpGet("transactions")]
    public IEnumerable<Transaction> GetTransactions()
    {
        return _dbContextHoolitees.Transactions;
    }

}