using HtmlAgilityPack;
using System.IO;

namespace Rss.Manager
{
    internal static class HtmlCleaner
    {
        internal static string Clean(string raw)
        {
            var htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(raw);

            using (var fixedHtmlStream = new MemoryStream())
            {
                htmlDoc.Save(fixedHtmlStream);
                return ConvertToString(fixedHtmlStream);
            }
        }

        private static string ConvertToString(Stream stream)
        {
            string contents;

            using (var reader = new StreamReader(stream))
            {
                stream.Position = 0;
                contents = reader.ReadToEnd();
            }

            return contents;
        }

        internal static string GetSnippet(string html, int length)
        {
            // TODO: should get "content" or text out of html, ignore images and so on.
            return "";
        }
    }
}
