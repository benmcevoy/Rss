using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Rss.Manager.Import
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

            if (opmlElement != null)
            {
                var element = opmlElement.Element("body");

                if (element != null)
                {
                    var topLevel = element.Elements("outline");

                    foreach (var outlineElement in topLevel)
                    {
                        if (outlineElement.HasElements)
                        {
                            root.AddFolder(new Folder
                                {
                                    Name = outlineElement.Attribute("title").Value,
                                    Feeds = (from feed in outlineElement.Elements("outline")
                                             select new Feed(new Uri(feed.Attribute("xmlUrl").Value),
                                                      feed.Attribute("title").Value,
                                                      new Uri(feed.Attribute("htmlUrl").Value)
                                                 ))
                                });
                        }
                        else
                        {
                            root.AddFeed(
                                new Feed(new Uri(outlineElement.Attribute("xmlUrl").Value),
                                         outlineElement.Attribute("title").Value,
                                         new Uri(outlineElement.Attribute("htmlUrl").Value))
                                );
                        }
                    }

                    return root;
                }
            }

            return new RootFolder();
        }
    }
}
