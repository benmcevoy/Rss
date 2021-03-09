using System;
using HtmlAgilityPack;

namespace Radio7.HtmlCleaner.Entities
{
    public class CandidateNode
    {
        public Guid Id { get; set; }

        public double Score { get; set; }

        public double RawScore { get; set; }

        public HtmlNode HtmlNode { get; set; }

        public string RawHtml { get; set; }

        public string Name { get; set; }
    }
}
