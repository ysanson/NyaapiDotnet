using System.Collections.Generic;
namespace NyaapiDotnet.Pantsu.Models
{
    public class PantsuResponse
    {
        public IEnumerable<PantsuTorrent> torrents { get; set; }
    }
}