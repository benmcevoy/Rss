using HtmlAgilityPack;

namespace Radio7.HtmlCleaner.Extractors.Title
{
    public class TitleByTitleTagCandidate : Candidate
    {
        public override HtmlResult Evaluate(HtmlDocument htmlDocument)
        {
            var result = htmlDocument.TryGetInnerTextByTagName("title");
            return result.IsSuccess ? result : base.Evaluate(htmlDocument);
        }
    }
}