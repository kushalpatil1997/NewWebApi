namespace NewWebApi.Models
{
    public class TopThreeArticle
    {
        public string title { get; set; }
        public string category { get; set; }
        public string author { get; set; }
        public decimal sentimentPolarity { get; set; }
    }
}
