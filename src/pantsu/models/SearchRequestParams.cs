using System.Runtime.Serialization.Json;
using System;
using System.Text.Json.Serialization;

namespace NyaapiDotnet.Pantsu.Models
{
    public class SearchRequestParams
    {

        [JsonPropertyName("c")]
        public string[] Categories  { get; set; }
        [JsonPropertyName("q")]
        public string Query { get; set; }
        public int Page { get; set; }
        public string Limit { get; set; }
        [JsonPropertyName("userID")]
        public string UserID { get; set; }
        [JsonPropertyName("fromID")]
        public string FromID { get; set; }
        [JsonPropertyName("s")]
        public string Status { get; set; }
        [JsonPropertyName("maxage")]
        public string MaxAge { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string DateType { get; set; }
        public string MinSize { get; set; }
        public string MaxSize { get; set; }
        public string SizeType { get; set; }
        public string Sort { get; set; }
        public string Order { get; set; }
        public string[] Languages { get; set; }
    }
}