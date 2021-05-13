using System;
using NyaapiDotnet.Pantsu.Models;
using NyaapiDotnet.Si.Models;

namespace NyaapiDotnet.Models
{
    public record Torrent
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Hash { get; init; }
        public DateTime Date { get; init; }
        public string Category { get; init; }
        public string SubCategory { get; init; }
        public string Magnet { get; init; }
        public string TorrentAddress { get; init; }
        public int Seeders { get; init; }
        public int Leechers { get; init; }
        public bool Completed { get; init; }
        public string Status { get; init; }

        public Torrent(PantsuTorrent pantsuTorrent)
        {
            Id = pantsuTorrent.Id;
            Name = pantsuTorrent.Name;
            Hash = pantsuTorrent.Hash;
            Date = pantsuTorrent.Date;
            Category = pantsuTorrent.Category;
            SubCategory = pantsuTorrent.SubCategory;
            Magnet = pantsuTorrent.Magnet;
            TorrentAddress = pantsuTorrent.Torrent;
            Seeders = pantsuTorrent.Seeders;
            Leechers = pantsuTorrent.Leechers;
            Completed = pantsuTorrent.Completed.Equals(1);
            Status = pantsuTorrent.Status.ToString();
        }

        public Torrent(SiTorrent siTorrent)
        {
            Id = siTorrent.Id;
            Name = siTorrent.Name;
            Hash = siTorrent.Hash;
            Date = siTorrent.Date;
            Category = siTorrent.Category;
            SubCategory = siTorrent.SubCategory;
            Magnet = siTorrent.Magnet;
            TorrentAddress = siTorrent.Torrent;
            Seeders = siTorrent.Seeders;
            Leechers = siTorrent.Leechers;
            Completed = siTorrent.Completed.Equals(1);
            Status = siTorrent.Status.ToString();
        }
    };
}