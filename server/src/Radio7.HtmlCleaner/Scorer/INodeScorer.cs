using HtmlAgilityPack;
using Radio7.HtmlCleaner.Entities;

namespace Radio7.HtmlCleaner.Scorer
{
    public interface INodeScorer
    {
        SentanceStatistics Score(HtmlNode htmlNode);
    }
}
