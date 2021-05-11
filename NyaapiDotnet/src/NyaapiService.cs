using System.Collections;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using NyaapiDotnet.Pantsu;
using NyaapiDotnet.Pantsu.Models;
using NyaapiDotnet.Models;
using NyaapiDotnet.Interfaces;

namespace NyaapiDotnet.Service
{
    public class NyaapiService : INyaapiService
    {
        private readonly INyaaClient pantsuClient;

        public NyaapiService()
        {
            pantsuClient = new PantsuClient();
        }

        public async IAsyncEnumerable<Torrent> SearchTorrents(Feeds feeds, Fansubs fansubs = Fansubs.None, Quality quality = Quality.None, string search = "", int limit = 0)
        {
            if (Feeds.Pantsu.Equals(feeds))
            {
                await foreach (Torrent t in pantsuClient.SearchTorrents(fansubs, quality, search, limit))
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
    }
}