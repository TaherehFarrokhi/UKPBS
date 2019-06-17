using System;
using System.Net;
using System.Threading.Tasks;
using RestSharp;

namespace UKPBS.Services.ApiClients
{
    public abstract class ApiClient
    {
        private readonly IRestClient _client;

        protected ApiClient(IRestClient client)
        {
            _client = client;
        }

        protected async Task<T> ExecuteAsync<T>(RestRequest request) where T : new()
        {
            var response = await _client.ExecuteTaskAsync<T>(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Data;

            throw new Exception($"{response.ErrorMessage}");
        }
    }
}