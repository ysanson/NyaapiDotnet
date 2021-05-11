using System.Collections.Generic;
namespace NyaapiDotnet.Pantsu.Models
{
    public class PantsuResponse
    {
        public IEnumerable<PantsuTorrent> Torrents { get; set; }
        public long? QueryRecordCount { get; set; }
        public long? TotalRecordCount { get; set; }
    }
}