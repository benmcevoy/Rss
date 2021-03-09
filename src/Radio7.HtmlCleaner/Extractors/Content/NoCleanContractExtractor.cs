using System;
using System.Diagnostics;
using System.Linq;
using HtmlAgilityPack;
using Radio7.HtmlCleaner.Entities;
using Radio7.HtmlCleaner.Extractors.Title;

namespace Radio7.HtmlCleaner.Extractors.Content
{
    public class NoCleanContractExtractor : IContentExtractor
    {
        public ExtractedContent Extract(string html, Uri documentUrl)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(html);

            var extractedContent = ExtractContent(htmlDocument, documentUrl);
            var title = new TitleExtractor().Extract(html);

            return new ExtractedContent
            {
                Url = documentUrl,
                Title = title,
                Html = extractedContent.ConvertToString()
            };
        }

        private HtmlDocument ExtractContent(HtmlDocument htmlDocument, Uri documentUrl)
        {
            // score
            var scorer = new AllNodeScorer();

            GC.Collect();

            var candidates = scorer.Score(htmlDocument).ToArray();

            GC.Collect();
            Debug.WriteLine("----------------------------------");
            //foreach (var candidateNode in candidates.OrderByDescending(c => c.Score))
            //{
            //    Debug.WriteLine("{0}:{1}\t{2}", candidateNode.HtmlNode.Name, candidateNode.Score, "");
            //}

            // post-process
            //var topCandidate = candidates.OrderByDescending(c => c.Score).FirstOrDefault();


            CandidateNode topCandidate = null;
            double score = 0D;

            foreach (var candidateNode in candidates)
            {
                if (candidateNode.Score > score)
                {
                    score = candidateNode.Score;
                    topCandidate = candidateNode;
                }
            }

            GC.Collect();

            Debug.WriteLine(topCandidate.Score);
            Debug.WriteLine(topCandidate.Id);

            var result = new HtmlDocument();
            //result.LoadHtml(topCandidate.RawHtml);

            //var container = result.CreateElement("div");

            //var query = string.Format("//{0}[@id='{1}']", topCandidate.Name, topCandidate.Id.ToString("D"));

            //var htmlNode = htmlDocument.DocumentNode.SelectSingleNode(query);
            var id = topCandidate.Id.ToString("D");
            HtmlNode found = null;

            foreach (var htmlNode in htmlDocument.DocumentNode.Descendants())
            {
                if (htmlNode.Id == id)
                {
                    found = htmlNode;
                    break;
                }
            }

            if (found != null) result.DocumentNode.AppendChild(found);

            Cleaners.HtmlCleaner.With(result).Clean().RemoveAllAttributesExcept("src", "href");

            return result;
        }
    }
}
