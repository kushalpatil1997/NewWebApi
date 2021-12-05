using Microsoft.AspNetCore.Mvc;
using NewWebApi.Models;

namespace NewWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class getMostPositiveNewsAuthorController : ControllerBase
    {
        [HttpGet]
        public TopAuthor getMostPositiveNewsAuthor()
        {
            var NewsLogic = new NewsLogicController();
            var res= NewsLogic.GetNewsListAndScore();
            var topAuthor = new TopAuthor();
            topAuthor.avgSentimentPolarity=0;
            List<Data> result = new List<Data>();

            foreach (var item in res)
            {
                foreach (var item2 in item.data)
                {
                    result.Add(item2);
                }
            }
            var authorList = result.GroupBy(i => i.author).ToList();

            foreach(var author in authorList)
            {
                var sum= author.Sum(a => a.score);
                sum = sum / author.Count();
                if (sum > topAuthor.avgSentimentPolarity)
                {
                    topAuthor.avgSentimentPolarity=sum;
                    topAuthor.author = author.Key.ToString();
                }
            }

            return topAuthor;
        }
    }
}
