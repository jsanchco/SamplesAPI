using System;
using System.Text.Json.Serialization;

namespace API.Lock.Models
{
    public class DoSomethingDTO
    {
        [JsonIgnore]
        public DateTime Start { get; set; }

        [JsonIgnore]
        public DateTime Stop { get; set; }

        public string Type { get; set; }

        public string TimeStamp => $"Start process [{Start:HH:mm:ss}], Stop process [{Stop:HH:mm:ss}]";
    }
}
