using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WuMortal.Dmhy.DataAnalysis.Core;
using WuMortal.Dmhy.DataAnalysis.IClient;
using WuMortal.Dmhy.DataAnalysis.Interface;
using WuMortal.Dmhy.DataAnalysis.Models;
using WuMortal.Dmhy.DataAnalysis.Models.Models;

namespace WuMortal.Dmhy.DataAnalysis
{
    public class DmhyPost : IDmhyPost
    {
        readonly IDmhyHttpClient _httpClient;

        public DmhyPost(IDmhyHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// 根据分类Id获取帖子
        /// </summary>
        /// <param name="categoryId">类别Id</param>
        /// <param name="pageIndex">当前页码</param>
        /// <returns></returns>
        public async Task<DPost[]> GetTopsDataByCategoryIdAsync(long categoryId, long pageIndex)
        {
            string url = "/topics/list/sort_id/" + categoryId + "/page/" + pageIndex;

            string html = await Common.DownloadHtmlAsync(_httpClient, url);

            return ToModels(html);

        }

        /// <summary>
        /// 根据关键词获取帖子
        /// </summary>
        /// <param name="keyWord">搜索的关键词</param>
        /// <param name="pageIndex">当前页码</param>
        /// <returns></returns>
        public async Task<DPost[]> GetTopsDataByKeyWordAsync(string keyWord, long pageIndex)
        {
            string url = "//topics/list/page/" + pageIndex + $"?keyword={keyWord}";

            string html = await Common.DownloadHtmlAsync(_httpClient, url);

            return ToModels(html);
        }

        /// <summary>
        /// 获取最新帖子
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <returns></returns>
        public async Task<DPost[]> GetTopsDataByPageIndexAsync(long pageIndex)
        {
            string url = "//topics/list/page/" + pageIndex;

            string html = await Common.DownloadHtmlAsync(_httpClient, url);

            return ToModels(html);
        }

        /// <summary>
        /// 获取字幕组的帖子
        /// </summary>
        /// <param name="userId">字幕组Id</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public async Task<DPost[]> GetTopsDataByTeamIdAsync(long userId, long pageIndex)
        {
            string url = $"/topics/list/team_id/{userId}/page/{pageIndex}";

            string html = await Common.DownloadHtmlAsync(_httpClient, url);

            return ToModels(html);
        }

        /// <summary>
        /// 获取用户发布的帖子
        /// </summary>
        /// <param name="userId">字幕组Id</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public async Task<DPost[]> GetTopsDataByUserIdAsync(long userId, long pageIndex)
        {
            string url = $"/topics/list/user_id/{userId}/page/{pageIndex}";

            string html = await Common.DownloadHtmlAsync(_httpClient, url);

            return ToModels(html);
        }

        private DPost[] ToModels(string html)
        {

            List<DPost> models = new List<DPost>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            HtmlNodeCollection trNodes = doc.DocumentNode.SelectNodes("//tbody/tr");

            if (trNodes == null)
            {
                return null;
            }

            foreach (var tr in trNodes)
            {
                HtmlNodeCollection tdNodes = tr.SelectNodes(".//td");

                //时间
                string time = tdNodes[0].FirstChild.InnerText.Trim();
                //string datetime = tdNodes[0].SelectSingleNode("./span").InnerText.Trim();

                //分类
                string category = tdNodes[1].SelectSingleNode("./a//font").InnerText.Trim();
                string categoryHref = tdNodes[1].SelectSingleNode("./a").Attributes["href"].Value;
                string categoryId = categoryHref.Substring(categoryHref.LastIndexOf("/")).Replace("/", "");

                //团队
                HtmlNode teamNode = tdNodes[2].SelectSingleNode("./span/a");
                string team = "";
                string teamHref = "";
                string teamId = "";
                if (teamNode != null)
                {
                    team = teamNode.InnerText.Trim();

                    teamHref = teamNode.Attributes["href"].Value.Trim();
                    teamId = teamHref.Substring(teamHref.LastIndexOf("/")).Replace("/", "");
                }

                //番剧相关
                string title = tdNodes[2].SelectSingleNode("./a").InnerText.Trim();
                string htmlId = tdNodes[2].SelectSingleNode("./a").Attributes["href"].Value.Replace(".html", "");
                htmlId = htmlId.Substring(htmlId.LastIndexOf("/") + 1);
                string downloadArrow = tdNodes[3].SelectSingleNode("./a").Attributes["href"].Value;
                string size = tdNodes[4].InnerText;
                string userName = tdNodes[8].InnerText;
                string userId = tdNodes[8].SelectSingleNode("./a").Attributes["href"].Value.Replace("/topics/list/user_id/", "");
                DCategory dCategory = new DCategory()
                {
                    Id = long.Parse(categoryId),
                    Name = category
                };

                DTeam dTeam = new DTeam()
                {
                    Id = string.IsNullOrEmpty(teamId) ? null : (long?)long.Parse(teamId),
                    Name = team
                };

                models.Add(new DPost
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = Convert.ToInt64(userId),
                    UserName = userName,
                    Category = dCategory,
                    DateTime = time,
                    DownloadArrow = downloadArrow,
                    FileSize = size,
                    Team = dTeam,
                    Title = title,
                    HtmlId = htmlId
                });

            }

            return models.ToArray();
        }
    }
}
