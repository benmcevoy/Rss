using HtmlAgilityPack;

namespace Rss.Api.Data
{
    internal static class HtmlCleanerHelper
    {
        internal static string Clean(string raw)
        {
            var htmlDoc = new HtmlDocument();
            
            htmlDoc.LoadHtml(raw);
            
            return htmlDoc.DocumentNode.OuterHtml;
        }

        internal static string GetSnippet(string html, int length)
        {
            html = System.Web.HttpUtility.HtmlDecode(StripTagsCharArray(html)) ?? "";

            if (html.Length <= length)
            {
                return html;
            }

            return html.Substring(0, length - 3) + "...";
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        internal static string StripTagsCharArray(string source)
        {
            var array = new char[source.Length];
            var arrayIndex = 0;
            var inside = false;

            foreach (var letter in source)
            {
                if (letter == '<')
                {
                    inside = true;
                    continue;
                }
                if (letter == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = letter;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }
}
