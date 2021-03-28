using HtmlAgilityPack;

namespace Radio7.HtmlCleaner.Extractors.Title
{
    public class TitleExtractor : ITitleExtractor
    {
        private readonly Candidate _rootCandidate;

        public TitleExtractor()
        {
            var titleByIdCandidate = new TitleByIdCandidate();
            var titleByH1TagCandidate = new TitleByH1TagCandidate();
            var titleByTitleTagCandidate = new TitleByTitleTagCandidate();

            // set chain of responsiblity
            titleByIdCandidate.SetNext(titleByH1TagCandidate);
            titleByH1TagCandidate.SetNext(titleByTitleTagCandidate);

            _rootCandidate = titleByIdCandidate;
        }

        public string Extract(string html)
        {
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(html);

            var result = _rootCandidate.Evaluate(htmlDocument);

            return result.IsSuccess ?
                Clean(result.Value) :
                "Can't find a title candidate";
        }

        private static string Clean(string value)
        {
            return value.Decode()
                        .StripTags()
                        .RemoveDodgyCharacters()
                        .RemoveWhitespace();
        }
    }
}
