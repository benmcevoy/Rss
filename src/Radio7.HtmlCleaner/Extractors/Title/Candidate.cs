using HtmlAgilityPack;

namespace Radio7.HtmlCleaner.Extractors.Title
{
    public abstract class Candidate
    {
        private Candidate _nextCandidate;

        public virtual void SetNext(Candidate candidate)
        {
            _nextCandidate = candidate;
        }

        public virtual HtmlResult Evaluate(HtmlDocument htmlDocument)
        {
            return _nextCandidate == null ?
                       new HtmlResult() :
                       _nextCandidate.Evaluate(htmlDocument);
        }
    }
}