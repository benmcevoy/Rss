using HtmlAgilityPack;

namespace Radio7.HtmlCleaner.Extractors.Title
{
    public class TitleByH1TagCandidate : Candidate
    {
        public override HtmlResult Evaluate(HtmlDocument htmlDocument)
        {
            var result = htmlDocument.TryGetInnerTextByTagName("h1");
            return result.IsSuccess ? result : base.Evaluate(htmlDocument);
        }
    }
}