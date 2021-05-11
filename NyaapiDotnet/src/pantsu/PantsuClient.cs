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
            string url = queryParams != null ? $"{PantsuConstants.url}/search?{queryParams.buildQueryParams()}" : $"{PantsuConstants.url}/search";

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var pantsuResponse = await JsonSerializer.DeserializeAsync<PantsuResponse>(await response.Content.ReadAsStreamAsync(), options);
            foreach (var torrent in pantsuResponse.Torrents)
            {
                yield return new Torrent(torrent);
            }
        }
    }
}