using System.Net.Http;
using System.Collections.Generic;
using NyaapiDotnet.Models;
using NyaapiDotnet.Pantsu.Models;

namespace NyaapiDotnet.Pantsu
{
    public class PantsuClient
    {
        public async IAsyncEnumerable<Torrent> SearchTorrents(SearchRequestParams queryParams)
        {
            string url = PantsuConstants.url + "/search";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            throw new System.Exception();
        }
    }
}