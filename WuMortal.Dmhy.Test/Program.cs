using System;
using System.Net.Http;
using WuMortal.Dmhy.Methods;
using WuMortal.Dmhy.Models;

namespace WuMortal.Dmhy.Test
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("请输入地址Id:");
            string url = Console.ReadLine();

            using (HttpClient client = new HttpClient())
            {
                DmhyPost dmhyPost = new DmhyPost(client);

                //DPost[] posts = dmhyPost.GetTopsDataByPageIndexAsync(1).Result;
                DPost[] posts2 = dmhyPost.GetTopsDataByKeyWordAsync("动漫", 1).Result;
                //DPost[] posts3 = dmhyPost.GetTopsDataByCategoryIdAsync(3, 1).Result;
                DmhyPostDetailed postDetailed = new DmhyPostDetailed(client);
                var detailed = postDetailed.GetPostDetailedAsync(url).Result;

                DmhyInfo dmhyInfo = new DmhyInfo(client);
                var categoies = dmhyInfo.GetDramaCategoryAsync().Result;
                var teams = dmhyInfo.GetTeamAsync().Result;

                var hotPosts = dmhyInfo.GetHotPostAsync().Result;
                //var detailed = postDetailed.GetCommentsAsync("500991").Result;
            }

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
