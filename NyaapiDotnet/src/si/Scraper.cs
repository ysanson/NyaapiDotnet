﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using NyaapiDotnet.Models;
using NyaapiDotnet.Si.Models;
using System.Text.RegularExpressions;

namespace NyaapiDotnet.Si
{
    public class Scraper
    {
        public async IAsyncEnumerable<Torrent> scrapeTorrent(SiRequestParams queryParams)
        {
            string queryUrl = queryParams.buildQueryParams();

            // Load default configuration
            var config = Configuration.Default.WithDefaultLoader();
            // Create a new browsing context
            var context = BrowsingContext.New(config);
            // This is where the HTTP request happens, returns <IDocument> that we can query later
            var document = await context.OpenAsync($"{SiConstants.url}?{queryUrl}");
            var torrentRows = document.QuerySelectorAll("tr.default");
            foreach(var row in torrentRows)
            {
                MatchCollection regxMatches = Regex.Matches(GetNthTd(row, 3).QuerySelector("a:nth-child(2)").GetAttribute("href"), @"btih:(\w+)");
                long.TryParse(GetNthTd(row, 2).QuerySelector("a:not('comments')").GetAttribute("href").Replace("/view/", ""), out long id);
                DateTime dt = DateTimeOffset.FromUnixTimeSeconds(long.Parse(GetNthTd(row, 5).GetAttribute("data-timestamp"))).DateTime;
                int.TryParse(GetNthTd(row, 6).TextContent, out int seeders);
                int.TryParse(GetNthTd(row, 7).TextContent, out int leechers);
                int.TryParse(GetNthTd(row, 8).TextContent, out int completed);


                SiTorrent torrent = new SiTorrent()
                {
                    Id = id,
                    Name = GetNthTd(row, 2).QuerySelector("a:not('comments')").TextContent.Trim(),
                    Hash = regxMatches.First().Value,
                    Date = dt,
                    Filesize = GetNthTd(row, 4).TextContent,
                    Category = GetNthTd(row, 1).QuerySelector("a").GetAttribute("href").Replace("/?c=", "").Replace(@"/\d{1,2}$/", "0"),
                    SubCategory = GetNthTd(row, 1).QuerySelector("a").GetAttribute("href").Replace("/?c=", ""),
                    Magnet = GetNthTd(row, 3).QuerySelector("a:nth-child(2)").GetAttribute("href"),
                    Torrent = SiConstants.url + GetNthTd(row, 3).QuerySelector("a:nth-child(1)").GetAttribute("href"),
                    Status = row.GetAttribute("class")
                };
                yield return new Torrent(torrent);
            }
        }

        private IElement GetNthTd(IElement input, int element)
        {
            return input.QuerySelector($"td:nth-child({element})");
        }


    }
}
