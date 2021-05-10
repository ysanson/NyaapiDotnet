using System;
namespace NyaapiDotnet.Models
{
    public record Torrent
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Hash { get; init; }
        public DateTime date { get; init; }
        public string Category { get; init; }
        public string SubCategory { get; init; }
        public string Magnet { get; init; }
        public string TorrentAddress { get; init; }
        public int Seeders { get; init; }
        public int Leechers { get; init; }
        public bool Completed { get; init; }
        public string Status { get; init; }
    };
}