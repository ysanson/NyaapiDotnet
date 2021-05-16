using System.Collections;
using Xunit;
using System.Threading.Tasks;
using System.Linq;
using NyaapiDotnet.Models;
using NyaapiDotnet.Service;

namespace NyaapiDotnet.UnitTests.Si
{
    public class NyaapiDotnet_SearchSiTest
    {
        [Fact]
        public async Task SearchRandom_ReturnResults()
        {
            var nyaapiService = new NyaapiService();
            ArrayList torrents = new();
            await foreach (Torrent t in nyaapiService.SearchTorrents(Feeds.Si))
            {
                torrents.Add(t);
            }
            Assert.NotEmpty(torrents);
        }
    }
}