using RestSharp;

namespace UKPBS.ConsoleApp
{
    public interface IApiClient
    {
        T Execute<T>(RestRequest request) where T : new();
    }
}