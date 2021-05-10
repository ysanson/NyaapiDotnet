using System.Linq;
using System;
using System.Net.Http;
using System.Collections.Generic;
using NyaapiDotnet.Models;
using NyaapiDotnet.Pantsu.Models;
using System.Text.Json;

namespace NyaapiDotnet.Pantsu
{
    public class PantsuClient
    {
        public async IAsyncEnumerable<Torrent> SearchTorrents(SearchRequestParams queryParams)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(PantsuConstants.url);
            string url = queryParams != null ? $"/search?{queryParams.buildQueryParams()}" : $"/search";
            
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var pantsuResponse = await JsonSerializer.DeserializeAsync<PantsuResponse>(await response.Content.ReadAsStreamAsync());
            foreach(var torrent in pantsuResponse.torrents)
            {
                yield return new Torrent(torrent);
            }
        }
    }
}