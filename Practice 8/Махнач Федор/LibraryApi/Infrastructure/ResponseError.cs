using Newtonsoft.Json;

namespace LibraryApi.Infrastructure
{
    public class ResponseError
    {
        [JsonProperty]
        public string Message { get; }

        public ResponseError(string message)
        {
            Message = message;
        }
    }
}