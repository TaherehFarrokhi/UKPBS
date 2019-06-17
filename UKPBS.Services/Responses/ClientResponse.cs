namespace UKPBS.Services.Responses
{
    public class ClientResponse<T>
    {
        public bool Success { get; set; }
        public T Payload { get; set; }
        public string ErrorMessage { get; set; }
    }
}