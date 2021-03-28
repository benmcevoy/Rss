
namespace Radio7.HtmlCleaner
{
    public class HtmlResult
    {
        public HtmlResult()
        {
            IsSuccess = false;
            Value = "";
        }

        public bool IsSuccess { get; set; }

        public string Value { get; set; }
    }
}
