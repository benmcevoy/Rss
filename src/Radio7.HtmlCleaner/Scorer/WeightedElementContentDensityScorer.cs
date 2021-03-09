using System;
using System.Collections.Generic;
using Radio7.HtmlCleaner.Entities;
using System.Linq;

namespace Radio7.HtmlCleaner.Scorer
{
    public class WeightedElementContentDensityScorer : INodeScorer
    {
        public SentanceStatistics Score(HtmlAgilityPack.HtmlNode htmlNode)
        {
            // TODO: refactor to INodeScorer or something
            // TODO: try with an exponential modifier to push good results
            // through the roof.  then we could try and extract just the content
            // and build our own tree
            // might let us avoid the whole normalize and clean crap
            // might also let us test any node instead of these candidates

            var score = 0D;
            var weight = 1D;

            if (htmlNode.Name == "p") weight = 10D;
            if (htmlNode.Name == "div") weight = 8D;
            if (htmlNode.Name == "pre") weight = 5D;
            if (htmlNode.Name == "td") weight = 2D;
            if (htmlNode.Name == "#text") weight = 5D;
            if (htmlNode.Name == "h2") weight = 5D;
            if (htmlNode.Name == "h3") weight = 5D;
            if (htmlNode.Name == "h4") weight = 2D;
            if (htmlNode.Name == "h5") weight = 2D;

            var text = htmlNode.InnerText.RemoveWhitespace();

            if (string.IsNullOrEmpty(text)) return new SentanceStatistics();

            // TODO: DRY - to sentance extractor?
            var sentances = text.Split(new[] { ".", "?", "!", ";", ".\"", "?\"", "!\"", "|" }, StringSplitOptions.RemoveEmptyEntries);
            var sentanceCount = sentances.Count();
            var sentanceScores = new List<SentanceScore>(sentanceCount);

            foreach (var sentance in sentances)
            {
                if (string.IsNullOrWhiteSpace(sentance)) continue;

                var wordCount = sentance.Split(' ').Length;

                if (wordCount > 8 && wordCount < 12) score += 50D;
                if (wordCount > 12 && wordCount < 30) score += 300D;
                if (wordCount > 30 && wordCount < 35) score += 200D;
                if (wordCount > 35 && wordCount < 50) score += 50D;

                sentanceScores.Add(new SentanceScore
                    {
                        WordCount = wordCount,
                        Score = score * weight
                    });
            }

            return new SentanceStatistics
                {
                    SentanceCount = sentanceCount,
                    SentanceScores = sentanceScores
                };
        }
    }
}
