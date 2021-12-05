namespace NewWebApi.Models
{
    public class SentimentModel
    {
        public Result result { get; set; }

    }
    public class Result
    {
        public decimal polarity { get; set; }
        public string type { get; set; }
    }
}
