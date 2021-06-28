namespace ProxyAPI.Helpers
{
    public interface IQuoteHelper
    {
        Quote GetBestQuote(int maxPrice, int x, int y, int z);
    }
}