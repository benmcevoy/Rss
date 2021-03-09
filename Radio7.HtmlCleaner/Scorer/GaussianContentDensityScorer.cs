using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Radio7.HtmlCleaner.Entities;

namespace Radio7.HtmlCleaner.Scorer
{
    class GaussianContentDensityScorer : INodeScorer
    {
        public SentanceStatistics Score(HtmlNode htmlNode)
        {
            if (!htmlNode.HasChildNodes) return new SentanceStatistics();

            var children = htmlNode.ChildNodes.Where(c => c.NodeType == HtmlNodeType.Text);

            if(!children.Any()) return new SentanceStatistics();
            
            var text = htmlNode.ChildNodes.Where(c => c.NodeType == HtmlNodeType.Text)
                               .Select(n => n.InnerText)
                               .Aggregate((current, next) => current + " " + next).RemoveWhitespace();

            if (string.IsNullOrEmpty(text)) return new SentanceStatistics();

            var sentances = text.Split(new[] { ".", "?", "!", ";", ".\"", "?\"", "!\"", "|" }, StringSplitOptions.RemoveEmptyEntries);
            var sentanceCount = sentances.Count();
            var sentanceScores = new List<SentanceScore>(sentanceCount);

            foreach (var sentance in sentances)
            {
                if (string.IsNullOrWhiteSpace(sentance)) continue;

                var wordCount = sentance.Split(' ').Length;

                var score = GetProbabilty(wordCount) * 500D;

                sentanceScores.Add(new SentanceScore
                    {
                        Score = score,
                        WordCount = wordCount
                    });
            }

            return new SentanceStatistics
                {
                    SentanceCount = sentanceCount,
                    SentanceScores = sentanceScores
                };
        }

        private double GetProbabilty(double x)
        {
            const double mean = 20.0D; // average sentance length
            //const double standardDeviation = 5D;
            //const double squareRoot2Pi = 2.50662827463D;
            const double standardDeviationSquareBy2 = 50D;
            const double inverseStandardDeviationBySquareRoot2Pi = 0.0797884560800001D;

            return (inverseStandardDeviationBySquareRoot2Pi *
                    Math.Exp(-(Math.Pow(x - mean, 2D) / standardDeviationSquareBy2))) * 100D;

        }

        private double GetThresholdScore(double x)
        {
            var result = Math.Exp(x - 200);

            if (result > 10000000000D) result = 10000000000D;
            if (double.IsPositiveInfinity(result)) result = 10000000000D;

            return result;
        }
    }
}
