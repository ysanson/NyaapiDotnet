using System.Collections;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NyaapiDotnet.Pantsu;
using NyaapiDotnet.Pantsu.Models;
using NyaapiDotnet.Models;

namespace NyaapiDotnet
{
    public class NyaapiService
    {
        private PantsuClient pantsuClient;

        public NyaapiService()
        {
            pantsuClient = new PantsuClient();
        }

        public async IAsyncEnumerable<Torrent> searchTorrents(Feeds feeds, Fansubs fansubs = Fansubs.None, Quality quality = Quality.None, string search = "", int limit = 0)
        {
            if (Feeds.Pantsu.Equals(feeds))
            {
                var queryParams = createParamsForPantsu(fansubs, quality, search, limit);
                await foreach (Torrent t in pantsuClient.SearchTorrents(queryParams))
                {
                    yield return t;
                }
            } else if (Feeds.Si.Equals(feeds))
            {
                throw new NotImplementedException();
            } else 
            {
                throw new NotSupportedException("The feed must be either Pantsu or SI");
            }
        }

        private SearchRequestParams createParamsForPantsu(Fansubs fansubs = Fansubs.None, Quality quality = Quality.None, string search = "", int limit = 0)
        {
            var queryParams = new SearchRequestParams();
            queryParams.Query = createSearchQuery(fansubs, quality, search);
            var categories = new ArrayList();
            categories.Add("1_0");
            queryParams.Categories = categories;
            queryParams.Limit = limit;
            return queryParams;
        }

        private string createSearchQuery(Fansubs fansubs = Fansubs.None, Quality quality = Quality.None, string search = "")
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

            return String.Concat(fansub, search, qual);
        }
    }
}