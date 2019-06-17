using UKPBS.Services.Responses;

namespace UKPBS.Services.Helpers
{
    internal static class ResponseHelper
    {
        internal static ClientResponse<T> SuccessFrom<T>(T payload)
        {
            return new ClientResponse<T>
            {
                Success = true,
                Payload = payload
            };
        }

        internal static ClientResponse<T> FailFrom<T>(string errorMessage)
        {
            return new ClientResponse<T>
            {
                ErrorMessage = errorMessage
            };
        }
    }
}