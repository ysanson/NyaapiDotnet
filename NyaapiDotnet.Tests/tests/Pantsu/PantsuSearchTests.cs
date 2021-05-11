using System.Collections;
using Xunit;
using System.Threading.Tasks;
using System.Linq;
using NyaapiDotnet.Models;
using NyaapiDotnet.Service;

namespace NyaapiDotnet.UnitTests.Pantsu
{
    public class NyaapiDotnet_SearchPantsuTest
    {
        [Fact]
        public async Task SearchRandom_ReturnResults()
        {
            var nyaapiService = new NyaapiService();
            ArrayList torrents = new();
            await foreach (Torrent t in nyaapiService.SearchTorrents(Feeds.Pantsu))
            {
                torrents.Add(t);
            }
            Assert.NotEmpty(torrents);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(50)]
        public async Task SearchLimit_ReturnsLimitedItems(int limit)
        {
            var nyaapiService = new NyaapiService();
            ArrayList torrents = new();
            await foreach (Torrent t in nyaapiService.SearchTorrents(Feeds.Pantsu, limit: limit))
            {
                torrents.Add(t);
            }
            Assert.NotEmpty(torrents);
            Assert.Equal(limit, torrents.Count);
        }

        [Fact]
        public async Task SearchForErai_ReturnEraiSubs()
        {
            var nyaapiService = new NyaapiService();
            await foreach (Torrent t in nyaapiService.SearchTorrents(Feeds.Pantsu, Fansubs.EraiRaws, limit: 10))
            {
                Assert.Contains("[Erai-raws]", t.Name);
            }
        }

        [Fact]
        public async Task SearchForQuality_ReturnsOnlyQuality()
        {
            var nyaapiService = new NyaapiService();
            await foreach (Torrent t in nyaapiService.SearchTorrents(Feeds.Pantsu, quality: Quality.FHD, limit: 10))
            {
                Assert.Contains("[1080p]", t.Name);
            }
        }
    }
}