using System.Linq;
using HtmlAgilityPack;

namespace Radio7.HtmlCleaner.Cleaners
{
    public class HtmlCleaner
    {
        private readonly HtmlDocument _htmlDocument;

        public HtmlCleaner(HtmlDocument htmlDocument)
        {
            _htmlDocument = htmlDocument;
        }

        public static HtmlCleaner With(HtmlDocument htmlDocument)
        {
            return new HtmlCleaner(htmlDocument);
        }

        public HtmlDocument ToHtmlDocument()
        {
            return _htmlDocument;
        }

        public static string Clean(string html)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(html);

            return new HtmlCleaner(htmlDocument)
                    .Clean()
                    .ToHtmlDocument()
                    .ConvertToString()
                    .RemoveWhitespace();
        }

        public HtmlCleaner Clean()
        {
            var elementsToRemove = new[] { "script", "noscript", "style", "link", "meta", "textarea", 
                "iframe", "input", "button", "select", "option", "audio", "canvas", "head", "fieldset",
                "h1", "header", "footer", "aside", "hr", "nav", "video", "object", "embed", "#comment", "svg" };

            return RemoveElements(elementsToRemove);
        }

        public HtmlCleaner RemoveElements(params string[] elementName)
        {
            _htmlDocument.DocumentNode.Descendants()
                .Where(n => elementName.Contains(n.Name))
                .ToList()
                .ForEach(n => n.Remove());

            return this;
        }

        public HtmlCleaner RemoveAllAttributesExcept(params string[] whitelist)
        {
            var elements = _htmlDocument.DocumentNode.SelectNodes("//*");

            if (elements == null)
            {
                return this;
            }

            foreach (var element in elements)
            {
                if (element.Attributes == null) continue;

                for (var i = element.Attributes.Count - 1; i >= 0; i--)
                {
                    var attribute = element.Attributes[i];

                    if (whitelist.Contains(attribute.Name)) continue;

                    element.Attributes.Remove(attribute);
                }
            }

            return this;
        }

        public HtmlCleaner RemoveAttributes(string attribute)
        {
            var elements = _htmlDocument.DocumentNode.SelectNodes(string.Format("//*[@{0}]", attribute));

            if (elements == null) return this;

            for (var i = elements.Count - 1; i >= 0; i--)
            {
                var element = elements[i];
                element.Attributes[attribute].Remove();
            }

            return this;
        }
    }
}
