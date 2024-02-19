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

        return 1000;
    }

    public void Dispose()
    {
        HttpClient.Dispose();
    }

}
