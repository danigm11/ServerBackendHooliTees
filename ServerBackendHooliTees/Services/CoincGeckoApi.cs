using Newtonsoft.Json;

namespace ServerBackendHooliTees.Services;

public class CoincGeckoApi : IDisposable
{

    private const string API_URL = "https://api.coingecko.com/api/v3/";

    private HttpClient HttpClient { get; set; }

    public CoincGeckoApi()
    {
        HttpClient = new HttpClient()
        {
            BaseAddress = new Uri(API_URL)
        };
    }

    public async Task<decimal> GetEthereumPriceAsync()
    {
        string json = await HttpClient.GetStringAsync("coins/ethereum");
        var data = JsonConvert.DeserializeObject<dynamic>(json);
        decimal price = (decimal)data.market_data.current_price.eur;
        return price;
    }

    public void Dispose()
    {
        HttpClient.Dispose();
    }

}
