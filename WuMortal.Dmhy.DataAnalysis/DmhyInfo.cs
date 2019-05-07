using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WuMortal.Dmhy.DataAnalysis.Core;
using WuMortal.Dmhy.DataAnalysis.IClient;
using WuMortal.Dmhy.DataAnalysis.Interface;
using WuMortal.Dmhy.DataAnalysis.Models;
using WuMortal.Dmhy.DataAnalysis.Models.Models;

namespace WuMortal.Dmhy.DataAnalysis
{
    public class DmhyInfo : IDmhyInfo
    {
        readonly IDmhyHttpClient _httpClient;

        public DmhyInfo(IDmhyHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// 获取番剧索引数据
        /// </summary>
        /// <returns></returns>
        public async Task<DDramaIndex[]> GetDramaIndexDataAsync()
        {
            List<DDramaIndex> dramaIndexModels = new List<DDramaIndex>();

            string url = "/";

            string html = await Common.DownloadHtmlAsync(_httpClient, url);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            HtmlNode scriptNode = doc.DocumentNode.SelectSingleNode(".//div[@class=\"main\"]");

            string scriptData = scriptNode.InnerHtml;

            List<string> dataList = new List<string>();


            //数据处理
            int startIndex = 0;
            int endIndex = 0;

            //获取所有星期的动漫数据
            while (true)
            {
                startIndex = scriptData.IndexOf("//星期", endIndex);

                endIndex = scriptData.IndexOf("//星期", startIndex + 7);

                if (endIndex < 0)
                {
                    string endText = scriptData.Substring(startIndex);
                    dataList.Add(endText);
                    break;
                }

                string text = scriptData.Substring(startIndex, endIndex - startIndex);
                dataList.Add(text);

            }

            //遍历所有星期数据
            foreach (string dramaData in dataList)
            {
                //获取数据正则
                MatchCollection matchs = Regex.Matches(dramaData, @".+?push\(\['(?<imgSrc>.+?)','(?<name>.+?)'.+?keyword=(?<keyword>.+?)""");

                if (matchs.Count <= 0)
                {
                    throw new Exception("获取番剧索引数据失败，无匹配数据!");
                }

                //保存当前星期的数据
                List<DDrama> dramaModels = new List<DDrama>();

                //获取当前星期匹配到的数据
                foreach (Match match in matchs)
                {
                    string keyword = match.Groups["keyword"].Value;

                    int index = keyword.IndexOf("+team_id");

                    keyword = keyword.Substring(0, index);

                    //转换成 DramaModel 类
                    dramaModels.Add(new DDrama
                    {
                        Id = Guid.NewGuid().ToString(),
                        Names = match.Groups["name"].Value,
                        ImgSrc = match.Groups["imgSrc"].Value,
                        KeyWord = keyword
                    });
                }

                dramaIndexModels.Add(new DDramaIndex
                {
                    Dramas = dramaModels.ToArray()
                });
            }


            return dramaIndexModels.ToArray();
        }

        /// <summary>
        /// 获取番剧类别信息
        /// </summary>
        /// <returns></returns>
        public async Task<DCategory[]> GetDramaCategoryAsync()
        {
            List<DCategory> listCategories = new List<DCategory>();

            string url = "/topics/advanced-search";

            string html = await Common.DownloadHtmlAsync(_httpClient, url);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            HtmlNodeCollection categoryNodes = doc.DocumentNode.SelectNodes("//select[@name='sort_id']/option");

            if (categoryNodes == null || categoryNodes.Count <= 0)
            {
                return null;
            }

            foreach (var category in categoryNodes)
            {
                DCategory dCategory = new DCategory()
                {
                    Id = Convert.ToInt64(category.Attributes["value"].Value),
                    Name = category.InnerText
                };

                listCategories.Add(dCategory);
            }

            listCategories.RemoveAt(0);

            return listCategories.ToArray();
        }


        /// <summary>
        /// 获取字幕组信息
        /// </summary>
        /// <returns></returns>
        public async Task<DTeam[]> GetTeamAsync()
        {
            List<DTeam> listTeams = new List<DTeam>();

            string url = "/topics/advanced-search";

            string html = await Common.DownloadHtmlAsync(_httpClient, url);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            HtmlNodeCollection teamNodes = doc.DocumentNode.SelectNodes("//select[@name='team_id']/option");

            if (teamNodes == null || teamNodes.Count <= 0)
            {
                return null;
            }

            foreach (var team in teamNodes)
            {
                DTeam dTeam = new DTeam()
                {
                    Id = Convert.ToInt64(team.Attributes["value"].Value),
                    Name = team.InnerText
                };

                listTeams.Add(dTeam);
            }

            listTeams.RemoveAt(0);

            return listTeams.ToArray();
        }

        /// <summary>
        /// 热门资源
        /// </summary>
        /// <returns></returns>
        public async Task<DHotPost[]> GetHotPostAsync()
        {
            List<DHotPost> listHotPosts = new List<DHotPost>();

            string url = "/topics/view/495541_LAC_Gintama_356_720P.html";

            string html = await Common.DownloadHtmlAsync(_httpClient, url);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            HtmlNodeCollection postNodes = doc.DocumentNode.SelectNodes("//div[@class='topics_cult box ui-corner-all nocontent']/div");

            if (postNodes == null)
            {
                return null;
            }

            foreach (var post in postNodes)
            {
                string imgUrl = post.SelectSingleNode("./p/img").Attributes["src"].Value;
                string name = post.SelectSingleNode("./a").InnerText;
                string id = post.SelectSingleNode("./a").Attributes["href"].Value;
                id = id.Substring(id.LastIndexOf("/") + 1).Replace(".html", "");

                listHotPosts.Add(new DHotPost()
                {
                    Id = id,
                    Name = name,
                    ImgUrl = imgUrl
                });
            }

            return listHotPosts.ToArray();
        }
    }
}
