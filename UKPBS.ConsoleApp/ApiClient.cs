using System;
using System.Net;
using RestSharp;

namespace UKPBS.ConsoleApp
{
    public class ApiClient : IApiClient
    {
        private readonly IRestClient _client;

        public ApiClient(string baseUrl)
        {
            _client = new RestClient { BaseUrl = new Uri(baseUrl, UriKind.Absolute) };
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var response = _client.Execute<T>(request);
            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                return response.Data;
            }
            throw new Exception($"{response.ErrorMessage}");
        }
    }
}