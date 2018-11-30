using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WuMortal.Dmhy.Models;

namespace WuMortal.Dmhy.Methods
{
    public class DmhyPostDetailed
    {
        private HttpClient _httpClient;

        public DmhyPostDetailed(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// 获取帖子详细内容
        /// </summary>
        /// <param name="Id">帖子Id</param>
        /// <returns></returns>
        public async Task<DPostDetailed> GetPostDetailedAsync(string Id)
        {
            string url = $"https://share.dmhy.org/topics/view/{Id}.html";


            string html = await Common.DownloadHtmlAsync(_httpClient, url);

            return ToDetailedModel(html);
        }

        /// <summary>
        /// 获取帖子的评论信息
        /// </summary>
        /// <param name="postId">帖子Id</param>
        /// <returns></returns>
        public async Task<DComment[]> GetCommentsAsync(string postId)
        {
            List<DComment> commentsList = new List<DComment>();

            string url = $"https://share.dmhy.org/comment/recent/topic_id/{postId}?stamp={DateTime.Now}";

            string html = await Common.DownloadHtmlAsync(_httpClient, url);

            //分析获取数据
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            HtmlNode mainNodes = doc.DocumentNode.SelectSingleNode("//*[@id='comment_recent']");

            HtmlNodeCollection commentNodes = mainNodes.SelectNodes("./tr");

            //判断是否有评论
            if (commentNodes == null)
            {
                return null;
            }

            //遍历评论tr节点
            foreach (HtmlNode comment in commentNodes)
            {
                long id = Convert.ToInt64(comment.Attributes["id"].Value.Replace("comment", ""));
                string content = comment.SelectSingleNode("./td[@class='comment_con']/span").InnerHtml;

                DCommentUserInfo userInfo = new DCommentUserInfo()
                {
                    Name = comment.SelectSingleNode("./td[@class='infotable']/span[@class='username']").InnerText,
                    DateTime = comment.SelectSingleNode("./td[@class='infotable']/span[@class='date']").InnerText,
                    IPAddress = comment.SelectSingleNode("./td[@class='infotable']/span[@class='ip']").InnerText,
                    ProfilePictureUrl = comment.SelectSingleNode("./td[@class='infotable']/p/img[@class='user_avatar_img']")
                                        .Attributes["src"].Value
                };

                commentsList.Add(new DComment
                {
                    Id = id,
                    Content = content,
                    UserInfo = userInfo
                });

            }

            return commentsList.ToArray();

        }

        private DPostDetailed ToDetailedModel(string html)
        {
            try
            {
                DPostDetailed model = new DPostDetailed();


                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                HtmlNode contentNode = doc.DocumentNode.SelectSingleNode("//div[@class='topic-main']");

                if (contentNode == null)
                {
                    return null;
                }

                //用户和组相关
                DPostDetailedTeam team = new DPostDetailedTeam();
                DPostDetailedUser user = new DPostDetailedUser();

                HtmlNodeCollection userSidebar = doc.DocumentNode.SelectNodes("/html/body/div/div/div[2]/div[7]/div[1]/div[@class='avatar box ui-corner-all']");
                HtmlNode userInfoNode = userSidebar[0];

                //发布人头像
                user.UserAvatarImg = userInfoNode.SelectSingleNode("./p[1]/img").Attributes["src"].Value;
                //发布姓名
                user.UserName = userInfoNode.SelectSingleNode("./p[2]/a").InnerText;
                //发布人Id
                user.UserId = userInfoNode.SelectSingleNode("./p[2]/a")
                    .Attributes["href"].Value.Replace("/topics/list/user_id/", "");


                if (userSidebar.Count >= 2)
                {

                    HtmlNode teamNode = userSidebar[1];
                    team.TeamLogoUrl = teamNode.SelectSingleNode("./p[1]/img").Attributes["src"].Value;
                    team.TeamName = teamNode.SelectSingleNode("./p[2]/a").InnerText;
                    team.TeamId = teamNode.SelectSingleNode("./p[2]/a")
                    .Attributes["href"].Value.Replace("/topics/list/team_id/", "");
                }


                //标题相关
                HtmlNode tiltleNode = contentNode.SelectSingleNode("./div[@class]");

                string title = tiltleNode.SelectSingleNode("./h3").InnerText;

                //分类
                HtmlNode categoryNode = tiltleNode.SelectSingleNode("./div/ul/li/span/a[last()]");
                string category = categoryNode.InnerText.Trim();
                string categoryHref = categoryNode.Attributes["href"].Value;
                string categoryId = categoryHref.Substring(categoryHref.LastIndexOf("/")).Replace("/", "");

                string datetime = tiltleNode.SelectSingleNode("./div/ul/li[2]/span").InnerText.Trim();

                //如果出现修改时间的话节点不是第四个
                HtmlNode sizeNode = tiltleNode.SelectSingleNode("./div/ul/li[4]/span");
                if (sizeNode == null)
                {
                    sizeNode = tiltleNode.SelectSingleNode("./div/ul/li[5]/span");
                }

                string size = sizeNode.InnerText.Trim();

                //文章内容
                string content = contentNode.SelectSingleNode("./div[2]").InnerHtml.Trim().Replace("<strong>簡介:&nbsp;</strong><br>", "");

                //BT链接
                //Dictionary<string, string> btDic = new Dictionary<string, string>();

                List<object> btList = new List<object>();

                var btNodes = doc.DocumentNode.SelectNodes("//div[@id='resource-tabs']/div[@id='tabs-1']//strong");

                foreach (var btNode in btNodes)
                {
                    var aNode = btNode.SelectSingleNode("./following-sibling::a");
                    string name = aNode.InnerText;
                    string href = aNode.Attributes["href"].Value == "#" ? name : aNode.Attributes["href"].Value;
                    btList.Add(new { Name = name, Href = href });
                    //btDic[name] = href;
                }

                //BT 详细
                List<object> btContentDict = new List<object>();
                var btContentNodes = doc.DocumentNode.SelectNodes("//div[@id='resource-tabs']/div[@id='tabs-1']/div[@class]/ul/li");

                if (btContentNodes.Count() > 0)
                {
                    foreach (var btContentNode in btContentNodes)
                    {
                        string name = btContentNode.SelectSingleNode("./img").NextSibling.InnerText.Trim();
                        string btSize = btContentNode.SelectSingleNode("./span").InnerText.Trim();
                        btContentDict.Add(new { Name = name, FileSize = btSize });
                        //btContentDict[name] = btSize;
                    }

                }


                DCategory dCategory = new DCategory()
                {
                    Id = long.Parse(categoryId),
                    Name = category
                };

                DPost post = new DPost()
                {
                    Id = Guid.NewGuid().ToString(),
                    Category = dCategory,
                    DateTime = datetime,
                    FileSize = size,
                    Title = title,
                };

                model = new DPostDetailed()
                {
                    User = user,
                    Team = team,
                    Post = post,
                    Content = content,
                    BTDict = btList.ToArray(),
                    BTContentDict = btContentDict.ToArray()
                };

                return model;
            }
            catch (Exception e)
            {

                throw new Exception($"在分析数据时发生错误:{e.Message}");
            }


        }
    }
}
