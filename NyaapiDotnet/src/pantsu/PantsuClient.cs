using System.Linq;
using System;
using System.Net.Http;
using System.Collections.Generic;
using NyaapiDotnet.Models;
using NyaapiDotnet.Interfaces;
using NyaapiDotnet.Pantsu.Models;
using System.Text.Json;
using System.Collections;

namespace NyaapiDotnet.Pantsu
{
    public class PantsuClient : INyaaClient
    {
        public async IAsyncEnumerable<Torrent> SearchTorrents(Fansubs fansubs, Quality quality, string search, int limit)
        {
            var queryParams = CreateParamsForPantsu(fansubs, quality, search, limit);
            await foreach (Torrent t in SearchTorrents(queryParams))
            {
                yield return t;
            }
        }


        private async IAsyncEnumerable<Torrent> SearchTorrents(SearchRequestParams queryParams)
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

        private SearchRequestParams CreateParamsForPantsu(Fansubs fansubs = Fansubs.None, Quality quality = Quality.None, string search = "", int limit = 0)
        {
            var queryParams = new SearchRequestParams
            {
                Query = CreateSearchQuery(fansubs, quality, search)
            };
            var categories = new ArrayList
            {
                "1_0"
            };
            queryParams.Categories = categories;
            queryParams.Limit = limit;
            return queryParams;
        }

        private string CreateSearchQuery(Fansubs fansubs = Fansubs.None, Quality quality = Quality.None, string search = "")
        {
            string fansub = fansubs switch
            {
                Fansubs.EraiRaws => "[Erai-raws] - ",
                Fansubs.Puya => "[PuyaSubs!] - ",
                Fansubs.Fuyu => "[Fuyu] - ",
                Fansubs.Durandal => "[DurandalSubs] - ",
                _ => ""
            };
            string qual = quality switch
            {
                Quality.SD => " [480p]",
                Quality.HD => " [720p]",
                Quality.FHD => " [1080p]",
                _ => ""
            };

            return string.Concat(fansub, search, qual);
        }

    }
}