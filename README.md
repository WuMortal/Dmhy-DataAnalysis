# 介绍：

该项目是之前用于分析 「动漫花园」 网站，现在分享给大家。

Nuget:[https://www.nuget.org/packages/WuMortal.Dmhy](https://www.nuget.org/packages/WuMortal.Dmhy)

安装：

> Install-Package WuMortal.Dmhy

项目使用 `HtmlAgilityPack` 解析「动漫花园」所爬取到的网页数据。

## 开源地址：
「动漫花园」数据分析：[进入查看](https://github.com/WuMortal/Dmhy-DataAnalysis)

「动漫花园」WebAPI：DotNet Core 开发，[进入查看](https://github.com/WuMortal) 待整理中。

「动漫花园」WebAPP：Angular6 开发， [进入查看](https://github.com/WuMortal) 待整理中。
## 使用方法 ： 

所有方法均为异步方法

- `DmhyInfo` 获取动漫花园信息的类 
- `DmhyPost` 用于获取帖子信息
- `DmhyPostDetailed` 用于获取帖子详细和帖子评论的类

``` csharp
using (HttpClient client = new HttpClient())
{
	DmhyPost dmhyPost = new DmhyPost(client);
	//获取帖子最新帖子
	DPost[] posts = dmhyPost.GetTopsDataByPageIndexAsync(1).Result;
	//搜索帖子
	DPost[] posts2 = dmhyPost.GetTopsDataByKeyWordAsync("动漫", 1).Result;
	//获取分类 id 为 3 的帖子
	DPost[] posts3 = dmhyPost.GetTopsDataByCategoryIdAsync(3, 1).Result;
	
	
	
	
	DmhyPostDetailed postDetailed = new DmhyPostDetailed(client);
	//url 为 https://share.dmhy.org/topics/view/503270_DHR_Seishun_Buta_Yaro_05_720P_MP4.html 
	//中的 503270_DHR_Seishun_Buta_Yaro_05_720P_MP4
	var detailed = postDetailed.GetPostDetailedAsync(url).Result; 

	DmhyInfo dmhyInfo = new DmhyInfo(client);
	var categoies = dmhyInfo.GetDramaCategoryAsync().Result;
	var teams = dmhyInfo.GetTeamAsync().Result;
	var hotPosts = dmhyInfo.GetHotPostAsync().Result;
	var detailed = postDetailed.GetCommentsAsync("500991").Result;
}
```
