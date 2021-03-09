using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using Radio7.HtmlCleaner.Entities;
using Radio7.HtmlCleaner.Scorer;

namespace Radio7.HtmlCleaner.Extractors.Content
{
    class AllNodeScorer
    {
        private readonly INodeScorer _nodeScorer;

        private static readonly string[] ElementNamesToIgnore = new[] { 
            "img", "input", "submit", "button", "textarea", "canvas", "audio", "video", 
            "svg", "title", "html", "script", "style", "link", "meta", 
            "iframe", "select", "option", "head", "hr", "br", "#comment", "noscript", "object" };

        private const string IdAttributeName = "id";
        private const string ScoreAttributeName = "__content__score";
        private const string RawScoreAttributeName = "__content__raw";
        private readonly List<CandidateNode> _candidateNodes = new List<CandidateNode>(64);

        public AllNodeScorer()
        {
            _nodeScorer = new GaussianContentDensityScorer();
        }

        public IEnumerable<CandidateNode> Score(HtmlDocument htmlDocument)
        {
            Traverse(htmlDocument.DocumentNode.SelectSingleNode("//body"));
            Debug.WriteLine(_counter);
            //ScaleCandidateScoresByLinkDensity(_candidateNodes);

            return _candidateNodes;
        }

        private void ScaleCandidateScoresByLinkDensity(IEnumerable<CandidateNode> candidateNodes)
        {
            foreach (var candidateNode in candidateNodes)
            {
                var linkDensity = GetLinkDensityScore(candidateNode.HtmlNode);
                candidateNode.Score = candidateNode.Score * (1 - linkDensity);
            }
        }

        public static double GetLinkDensityScore(HtmlNode htmlNode)
        {
            var links = htmlNode.SelectNodes("//a");

            if (links == null) return 0D;

            var linkLength = (double)links.Sum(l => l.InnerText.Length);
            var textLength = (double)htmlNode.InnerText.Length;

            return linkLength / textLength;
        }

        private int _counter;

        private void Traverse(HtmlNode root)
        {
            // damn need tree walk, but can't afford recursion?
            // time to relearn some algo
            // use unit test and stop hammering bbc

            _counter++;

            if (root == null) return;

            var isSkip = false || ((string.IsNullOrWhiteSpace(root.InnerText)) ||
                                   (root.InnerText.RemoveWhitespace().Length < 10) ||
                                   (root.ParentNode == null) || (ElementNamesToIgnore.Contains(root.Name)));

            if (!isSkip)
            {
                EnsureCandidateScoreAttributes(root);
                ScoreImpl(root);
            }

            if (root.HasChildNodes)
            {
                Traverse(root.FirstChild);
            }
            else
            {
                Traverse(root.NextSibling);
            }
        }

        private void ScoreImpl(HtmlNode root)
        {
            if (string.IsNullOrWhiteSpace(root.InnerText)) return;
            if (root.ParentNode == null) return;
            if (root.ParentNode.Name == "body") return;

            var score = _nodeScorer.Score(root);

            Debug.WriteLine("{0},{1}", score.TotalScore, root.OuterHtml.TrimWithEllipsis(50));

            EnsureCandidateScoreAttributes(root);
            EnsureCandidateScoreAttributes(root.ParentNode);
            // add raw score to self
            AddToHtmlNodeRawScoreAttribute(root, score.TotalScore);

            // add to parent
            AddToHtmlNodeScoreAttribute(root.ParentNode, score.TotalScore);
            UpdateCandidateNode(root.ParentNode);

            if (root.ParentNode.ParentNode == null) return;
            if (root.ParentNode.ParentNode.Name == "body") return;

            EnsureCandidateScoreAttributes(root.ParentNode.ParentNode);
            // add to grandparent
            AddToHtmlNodeScoreAttribute(root.ParentNode.ParentNode, score.TotalScore / 2D);
            UpdateCandidateNode(root.ParentNode.ParentNode);
            //UpdateCandidateRawScore(root);
        }

        private void UpdateCandidateNode(HtmlNode htmlNode)
        {
            var score = Convert.ToDouble(htmlNode.GetAttributeValue(ScoreAttributeName, "0.0"));
            var id = Guid.Parse(htmlNode.GetAttributeValue(IdAttributeName, Guid.Empty.ToString("D")));

            if (_candidateNodes.Any(c => c.Id == id))
            {
                _candidateNodes.First(c => c.Id == id).Score = score;
                return;
            }

            var candidateNode = new CandidateNode
            {
                //HtmlNode = htmlNode,
                //RawHtml = htmlNode.InnerText,
                Score = score,
                Id = id,
                //Name = htmlNode.Name
            };

            _candidateNodes.Add(candidateNode);
        }

        private void EnsureCandidateScoreAttributes(HtmlNode htmlNode)
        {
            if (htmlNode.Attributes.Contains(ScoreAttributeName)) return;

            htmlNode.SetAttributeValue(IdAttributeName, Guid.NewGuid().ToString("D"));
            htmlNode.SetAttributeValue(RawScoreAttributeName, "0.0");
            AddToHtmlNodeScoreAttribute(htmlNode, 0D);
        }

        private void AddToHtmlNodeScoreAttribute(HtmlNode htmlNode, double score)
        {
            var current = Convert.ToDouble(htmlNode.GetAttributeValue(ScoreAttributeName, "0.0"));
            htmlNode.SetAttributeValue(ScoreAttributeName, (current + score).ToString(CultureInfo.InvariantCulture));
        }

        private void AddToHtmlNodeRawScoreAttribute(HtmlNode htmlNode, double score)
        {
            var current = Convert.ToDouble(htmlNode.GetAttributeValue(RawScoreAttributeName, "0.0"));
            htmlNode.SetAttributeValue(RawScoreAttributeName, (current + score).ToString(CultureInfo.InvariantCulture));
        }
    }
}
