using Newtonsoft.Json;

namespace ConsoleApp.ToteLink.Models
{
    public class ResultRequest<T>
    {
        public bool isSuccessful { get; set; }
        public int statusCode { get; set; }
        public string statusError { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T data { get; set; }
    }
}
