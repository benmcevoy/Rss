using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Radio7.HtmlCleaner.Extractors.Content
{
    public class Normalizer
    {
        private readonly HtmlDocument _htmlDocument;
        private readonly Uri _documentUrl;

        private static readonly Regex BoilerPlateCandidatesRegex = new Regex(
            @"combx|community|contact|disqus|foot|footer|header|" +
            "menu|rss|shoutbox|sidebar|masthead|sponsor|ad-break|agegate|" +
            "pagination|pager|popup|tweet|twitter|aside|advert|footnote|" +
            "outbrain|promo|related|scroll|shopping|tags|tool|pop\\-up|" +
            "metadata|video|nav|sub\\-nav|toolbar|playlist|addthis|social|share",
            RegexOptions.Compiled & RegexOptions.IgnoreCase);

        public Normalizer(HtmlDocument htmlDocument, Uri documentUrl)
        {
            _htmlDocument = htmlDocument;
            _documentUrl = documentUrl;
        }

        public static Normalizer With(HtmlDocument htmlDocument, Uri documentUrl)
        {
            return new Normalizer(htmlDocument, documentUrl);
        }

        public Normalizer FindBestCandidateFrame()
        {
            // check if it has any frames
            // find the biggest frame on this domain...
            // need url

            return this;
        }

        private void ProcessElements(IList<HtmlNode> elements, Action<HtmlNode> action)
        {
            if (elements == null) return;

            for (var i = elements.Count - 1; i >= 0; i--)
            {
                var element = elements[i];
                action(element);
            }
        }

        public Normalizer CleanImages()
        {
            var elements = _htmlDocument.DocumentNode.SelectNodes("//img");

            // TODO: consider a HEAD request and try and get content-length
            // if image size is very small it is probably rubbish?

            ProcessElements(elements, image =>
                {
                    var src = image.GetAttributeValue("src", "");

                    if (src.Contains(".gif") || string.IsNullOrEmpty(src))
                    {
                        image.ParentNode.Remove();
                    }

                    var altText = image.GetAttributeValue("alt", "");
                    var figure = _htmlDocument.CreateElement("div");

                    figure.AppendChild(image.Clone());

                    if (!string.IsNullOrEmpty(altText))
                    {
                        var caption = _htmlDocument.CreateElement("sub");

                        caption.InnerHtml = altText;
                        figure.AppendChild(caption);
                    }

                    image.ParentNode.ReplaceChild(figure, image);
                });

            return this;
        }

        public Normalizer RemoveImages()
        {
            var elements = _htmlDocument.DocumentNode.SelectNodes("//img");

            ProcessElements(elements, image => image.Remove());

            return this;
        }

        public Normalizer ReplaceBrCandidates()
        {
            // Turn all double br's into p's 
            var elements = _htmlDocument.DocumentNode.SelectNodes("//br");

            ProcessElements(elements, element =>
                {
                    var emptyParagraph = _htmlDocument.CreateElement("p");
                    emptyParagraph.InnerHtml = "&nbsp;";

                    if (element.NextSibling == null || element.NextSibling.Name != "br") return;

                    element.NextSibling.Remove();
                    element.ParentNode.ReplaceChild(emptyParagraph, element);
                });

            return this;
        }

        public Normalizer RebaseUrls()
        {
            var anchors = _htmlDocument.DocumentNode.SelectNodes("//a");

            ProcessElements(anchors, element =>
                {
                    var href = element.GetAttributeValue("href", "");
                    var url = EnsureUrlAbsolute(href);

                    element.SetAttributeValue("href", url);
                });

            var images = _htmlDocument.DocumentNode.SelectNodes("//img");

            ProcessElements(images, element =>
            {
                var href = element.GetAttributeValue("src", "");
                var url = EnsureUrlAbsolute(href);

                element.SetAttributeValue("src", url);
            });

            return this;
        }

        private string EnsureUrlAbsolute(string url)
        {
            if (string.IsNullOrEmpty(url)) return "#";
            if (IsAbsoluteUri(url)) return url;

            if (url.StartsWith("?"))
            {
                var result = string.Format("{0}://{1}{2}{3}",
                                           string.IsNullOrEmpty(_documentUrl.Scheme) ? "http" : _documentUrl.Scheme,
                                           _documentUrl.Host,
                                           _documentUrl.AbsolutePath,
                                           url);

                if (IsAbsoluteUri(result))
                {
                    return result;
                }
            }

            Uri absoluteUri;

            return Uri.TryCreate(_documentUrl, url, out absoluteUri) ? absoluteUri.ToString() : "#";
        }

        private bool IsAbsoluteUri(string url)
        {
            Uri result;
            var isOk = Uri.TryCreate(url, UriKind.Absolute, out result);

            if (isOk)
            {
                // default scheme seems to be file:
                // some url's will come in as "valid", but with no scheme.  
                // e.g. //upload.wikimedia.org/wikipedia/etc etc
                if (result.Scheme != "http" || result.Scheme != "https")
                    return false;
            }

            return isOk;
        }

        public Normalizer EnsureBodyElement()
        {
            // if body element is missing then add it
            // why? do i care?

            return this;
        }

        public Normalizer ReplaceFonts()
        {
            var elements = _htmlDocument.DocumentNode.SelectNodes("//font");

            ProcessElements(elements, element =>
                {
                    var value = element.InnerHtml;
                    var span = _htmlDocument.CreateElement("span");
                    span.InnerHtml = value;

                    element.ParentNode.ReplaceChild(span, element);
                });

            return this;
        }

        public Normalizer RemoveEmptyCandidateElements()
        {
            var elements = _htmlDocument.DocumentNode
                                      .Descendants()
                                      .Where(n =>
                                          n.Name.ToUpperInvariant() != "BODY" &&
                                          (n.Name == "div" || n.Name == "span"))
                                          .ToList();

            ProcessElements(elements, element =>
                {
                    if (string.IsNullOrEmpty(element.InnerHtml.RemoveWhitespace()))
                    {
                        element.Remove();
                    }
                });

            return this;
        }

        public Normalizer RemoveBoilerPlateCandidates()
        {
            // search by class
            var byClassName = _htmlDocument.DocumentNode
                                          .Descendants()
                                          .Where(n =>
                                              n.Name.ToUpperInvariant() != "BODY" &&
                                              n.Attributes.Contains("class") &&
                                              IsBoilerPlateCandidate(n.Attributes["class"].Value))
                                              .ToList();

            ProcessElements(byClassName, element => element.Remove());

            // search by id
            var byId = _htmlDocument.DocumentNode
                                        .Descendants()
                                        .Where(n =>
                                            n.Name.ToUpperInvariant() != "BODY" &&
                                            IsBoilerPlateCandidate(n.Id))
                                            .ToList();

            ProcessElements(byId, element => element.Remove());

            return this;
        }

        private bool IsBoilerPlateCandidate(string value)
        {
            return !string.IsNullOrEmpty(value) && BoilerPlateCandidatesRegex.IsMatch(value);
        }

        public Normalizer RemoveXoXo()
        {
            var elements = _htmlDocument.DocumentNode
                                         .Descendants()
                                         .Where(n =>
                                             n.Name.ToUpperInvariant() != "BODY" &&
                                             (n.Name == "ul" || n.Name == "ol" || n.Name == "dl"))
                                             .ToList();

            ProcessElements(elements, element =>
            {
                // TODO: refactor to reduce coupling
                if (Scorer.GetLinkDensityScore(element) > .5)
                {
                    element.Remove();
                }
            });

            return this;
        }
    }
}
