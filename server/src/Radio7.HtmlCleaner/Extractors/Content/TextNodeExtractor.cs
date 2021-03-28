using System;
using System.Linq;
using HtmlAgilityPack;
using Radio7.HtmlCleaner.Entities;
using Radio7.HtmlCleaner.Extractors.Title;

namespace Radio7.HtmlCleaner.Extractors.Content
{
    public class TextNodeExtractor : IContentExtractor
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
            var scorer = new TextNodeScorer();
            var candidates = scorer.Score(htmlDocument).ToArray();
            var topCandidate = candidates.OrderByDescending(c => c.Score).FirstOrDefault();
            var result = new HtmlDocument();
            var container = result.CreateElement("div");

            if (topCandidate != null) container.AppendChild(topCandidate.HtmlNode);

            result.DocumentNode.AppendChild(container);

            Cleaners.HtmlCleaner.With(result).Clean().RemoveAllAttributesExcept("src", "href");

            return result;
        }
    }
}
