using System;
using Radio7.HtmlCleaner.Entities;

namespace Radio7.HtmlCleaner.Extractors.Content
{
    public interface IContentExtractor
    {
        ExtractedContent Extract(string html, Uri documentUrl);
    }
}
