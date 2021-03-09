using System;
using System.Linq;
using System.Xml.Linq;

namespace Radio7.Rss.Import
{
    public static class OpmlImporter
    {
        public static RootFolder Import(string opml)
        {
            try
            {
                return Import(XDocument.Parse(opml));
            }
            catch
            {
                return new RootFolder();
            }
        }

        public static RootFolder Import(XDocument opml)
        {
            var root = new RootFolder();
            var opmlElement = opml.Element("opml");

            if (opmlElement == null) return new RootFolder();

            var element = opmlElement.Element("body");

            if (element == null) return new RootFolder();

            var topLevel = element.Elements("outline");

            foreach (var outlineElement in topLevel)
            {
                if (outlineElement.HasElements)
                {
                    root.AddFolder(new Folder
                    {
                        Name = outlineElement.Attribute("title").Value,
                        Feeds = (from feed in outlineElement.Elements("outline")
                            select new Feed(new Uri(feed.Attribute("xmlUrl").Value), new Uri(feed.Attribute("htmlUrl").Value)))
                    });
                }
                else
                {
                    root.AddFeed(
                        new Feed(new Uri(outlineElement.Attribute("xmlUrl").Value), new Uri(outlineElement.Attribute("htmlUrl").Value)));
                }
            }

            return root;
        }
    }
}
