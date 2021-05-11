using System.Collections;
using Xunit;
using System.Threading.Tasks;
using NyaapiDotnet;
using NyaapiDotnet.Models;

namespace NyaapiDotnet.UnitTests.Pantsu
{
    public class NyaapiDotnet_SearchPantsuTest
    {
        [Fact]
        public async Task SearchRandom_ReturnResults()
        {
            var nyaapiService = new NyaapiService();
            ArrayList torrents = new ArrayList();
            await foreach (Torrent t in nyaapiService.searchTorrents(Feeds.Pantsu))
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
            ArrayList torrents = new ArrayList();
            await foreach (Torrent t in nyaapiService.searchTorrents(Feeds.Pantsu, limit: limit))
            {
                torrents.Add(t);
            }
            Assert.NotEmpty(torrents);
            Assert.Equal(limit, torrents.Count);
        }
    }
}