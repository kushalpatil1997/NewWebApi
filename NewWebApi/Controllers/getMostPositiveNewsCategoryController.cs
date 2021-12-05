using Microsoft.AspNetCore.Mvc;
using NewWebApi.Models;

namespace NewWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class getMostPositiveNewsCategoryController : ControllerBase
    {
        [HttpGet]
        public TopCategory getMostPositiveNewsCategory()
        {
            var NewsLogic = new NewsLogicController();
            var res= NewsLogic.GetNewsListAndScore();
            var topCategory = new TopCategory();
            topCategory.avgSentimentPolarity = 0;

            foreach (var item in res)
            {
                if (item.average_score_per_category > topCategory.avgSentimentPolarity)
                {
                    topCategory.avgSentimentPolarity = item.average_score_per_category;
                    topCategory.category = item.category;
                }

            }

            return topCategory;
        }
    }
}
