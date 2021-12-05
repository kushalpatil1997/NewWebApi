namespace NewWebApi.Models
{
    public class NewsModel
    {
        public string category { get; set; }
        public List<Data> data { get; set; }
        public Boolean Success { get; set; }
        public decimal average_score_per_category { get; set; }
    }
}
