using System;
using System.IO;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Radio7.HtmlCleaner.Entities;
using Radio7.HtmlCleaner.Extractors.Title;

namespace Radio7.HtmlCleaner.Extractors.Content
{
    public class ContentExtractor : IContentExtractor
    {
        public ExtractedContent Extract(string html, Uri documentUrl)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.Load(AsStream(html), Encoding.GetEncoding("windows-1252"));
            //htmlDocument.LoadHtml(html);
            
            var extractedContent = ExtractContent(htmlDocument, documentUrl);

            var title = new TitleExtractor().Extract(html);
            var text = NormalizeText(extractedContent);

            return new ExtractedContent
                {
                    Url = documentUrl,
                    Title = title,
                    Html = extractedContent.ConvertToString(),
                    Text = text,
                    Domain = GetDomain(documentUrl)
                };
        }

        private Stream AsStream(string value)
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            sw.Write(value);
            sw.Flush();
            ms.Position = 0;
            return ms;
        }

        private string GetDomain(Uri documentUrl)
        {
            var host = documentUrl.Host;

            if (host.StartsWith("www.")) return host.Remove(0, 4);
            if (host.StartsWith("m.")) return host.Remove(0, 2);
            if (host.StartsWith("mobile.")) return host.Remove(0, 7);
            if (host.StartsWith("mobi.")) return host.Remove(0, 5);

            return host;
        }

        private string NormalizeText(HtmlDocument htmlDocument)
        {
            Cleaners.HtmlCleaner.With(htmlDocument).RemoveElements("sub");

            var result = new StringBuilder();
            var innerText = htmlDocument.DocumentNode.InnerText;
            var sentances = innerText.Split(new[] { ".", "?", "!", ";", ".\"", "?\"", "!\"", "|", ".)" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var sentance in sentances)
            {
                var cleanSentance = sentance.Decode().RemoveDodgyCharacters().RemoveWhitespace();

                if (cleanSentance.StartsWith("("))
                {
                    cleanSentance = cleanSentance.Remove(0, 1).Trim();
                }

                if (cleanSentance.StartsWith(")"))
                {
                    cleanSentance = cleanSentance.Remove(0, 1).Trim();
                }

                if (cleanSentance.Split(' ').Length <= 1) continue;

                result.AppendFormat("{0}{1}{2}{2}", cleanSentance, ".", Environment.NewLine);
            }

            return result.ToString();
        }

        private HtmlDocument ExtractContent(HtmlDocument htmlDocument, Uri documentUrl)
        {
            // pre-process
            CleanAndNormalize(htmlDocument, documentUrl);

            // score
            var scorer = new Scorer();
            var candidates = scorer.Score(htmlDocument).ToArray();

            // post-process
            var topCandidate = candidates.OrderByDescending(c => c.Score).FirstOrDefault();
            var result = new HtmlDocument();
            var container = result.CreateElement("div");

            if (topCandidate != null) container.AppendChild(topCandidate.HtmlNode);

            result.DocumentNode.AppendChild(container);

            Cleaners.HtmlCleaner.With(result).Clean().RemoveAllAttributesExcept("src", "href");

            return result;
        }

        private void CleanAndNormalize(HtmlDocument htmlDocument, Uri documentUrl)
        {
            Cleaners.HtmlCleaner.With(htmlDocument).Clean();

            Normalizer.With(htmlDocument, documentUrl)
                .FindBestCandidateFrame()
                .EnsureBodyElement()
                .RemoveBoilerPlateCandidates()
                .ReplaceFonts()
                .ReplaceBrCandidates()
                .RemoveXoXo()
                .RemoveEmptyCandidateElements()
                .RebaseUrls();
            //.CleanImages();
        }
    }
}

