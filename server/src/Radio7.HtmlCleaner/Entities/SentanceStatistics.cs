using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace Radio7.HtmlCleaner.Entities
{
    public class SentanceStatistics
    {
        public SentanceStatistics()
        {
            SentanceScores = Enumerable.Empty<SentanceScore>();
        }

        public int SentanceCount { get; set; }

        public HtmlNode HtmlNode { get; set; }

        public IEnumerable<SentanceScore> SentanceScores { get; set; }

        public double TotalScore
        {
            get { return SentanceScores.Sum(s => s.Score); }
        }

        public int TotalWordCount
        {
            get { return SentanceScores.Sum(s => s.WordCount); }
        }

        public double SentanceLengthAverage
        {
            get
            {
                if (SentanceCount == 0) return 0D;

                return (double)TotalWordCount / SentanceCount;
            }
        }
    }
}
