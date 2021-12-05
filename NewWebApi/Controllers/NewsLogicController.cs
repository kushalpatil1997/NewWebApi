using Microsoft.AspNetCore.Mvc;
using NewWebApi.Models;
using System.Net;
using System.Text.Json;

namespace NewWebApi.Controllers
{
    public class NewsLogicController : Controller
    {
        public List<NewsModel> GetNewsListAndScore()
        {
            List<NewsModel> newsModel = new List<NewsModel>();
            List<string> category= new List<string>();
            category.Add("national");
            category.Add("business");
            category.Add("sports");
            category.Add("world");
            category.Add("politics");
            category.Add("technology");
            category.Add("startup");
            category.Add("entertainment");
            category.Add("miscellaneous");
            category.Add("hatke");
            category.Add("science");
            category.Add("automobile");

            Parallel.ForEach(category, i =>
            {
                newsModel.Add(GetFinalList(i));
            });



            return newsModel;
        }

        public SentimentModel GetNewsScore(string title)
        {
            string URL = "https://sentim-api.herokuapp.com/api/v1/";
            WebRequest request = WebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/json";
            var body = JsonSerializer.Serialize(new { text = title });

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(body);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            string result2 = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result2 = streamReader.ReadToEnd();
            }

            SentimentModel sentimentResult = JsonSerializer.Deserialize<SentimentModel>(result2);

            return sentimentResult;
        }

        public NewsModel GetFinalList(string category)
        {
            decimal total_score_per_category = 0;
            string URL = "https://inshortsapi.vercel.app/news?category="+category;
            WebRequest request = WebRequest.Create(URL);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string Result = "";
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                Result = sr.ReadToEnd();
                sr.Close();
            }

            NewsModel newsResult = JsonSerializer.Deserialize<NewsModel>(Result);



            foreach (var m in newsResult.data)
            {
                m.category = newsResult.category;
                var sentimentResult = GetNewsScore(m.title);
                m.score = sentimentResult.result.polarity;
                m.type = sentimentResult.result.type;
                total_score_per_category += m.score;
            }
            newsResult.average_score_per_category = total_score_per_category / newsResult.data.Count();

            return newsResult;
        }
    }
}
