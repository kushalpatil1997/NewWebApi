using Microsoft.AspNetCore.Mvc;
using NewWebApi.Models;

namespace NewWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class getTop3PositiveArticlesController : ControllerBase
    {
        [HttpGet]
        public TopThreeArticle[] getTop3PositiveArticles()
        {
            TopThreeArticle[] topThreeArticles = new TopThreeArticle[3];
            var NewsLogic = new NewsLogicController();
            var res = NewsLogic.GetNewsListAndScore();
            List<Data> result = new List<Data>();

            foreach (var item in res)
            {
                foreach (var item2 in item.data)
                {
                    result.Add(item2);
                }
            }

            result.Sort(delegate (Data x, Data y) {
                return y.score.CompareTo(x.score);
            });

            for (int i = 0; i < 3; i++)
            {
                topThreeArticles[i] = new TopThreeArticle();
                topThreeArticles[i].category = result[i].category;
                topThreeArticles[i].author = result[i].author;
                topThreeArticles[i].sentimentPolarity = result[i].score;
                topThreeArticles[i].title = result[i].title;
            }

            return topThreeArticles;
        }
    }
}
