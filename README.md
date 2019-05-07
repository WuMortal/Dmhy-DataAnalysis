# 介绍：

该项目是之前用于分析 「动漫花园」 网站，现在分享给大家。

Nuget: [https://www.nuget.org/packages/WuMortal.Dmhy](https://www.nuget.org/packages/WuMortal.Dmhy)

安装：`Install-Package WuMortal.Dmhy`

项目使用 `HtmlAgilityPack` 解析「动漫花园」所爬取到的网页数据。

# 开源地址：

「动漫花园」数据分析：[进入查看](https://github.com/WuMortal/Dmhy-DataAnalysis)

「动漫花园」WebAPI：DotNet Core 开发，[进入查看](https://github.com/WuMortal) 待整理中。

「动漫花园」WebApp：Angular 6 开发， [进入查看](https://github.com/WuMortal) 待整理中。

Dmhy-Mobile：.NET + Angular.js 开发，[进入查看](https://github.com/WuMortal/Dmhy-Mobile) 。

# 说明

> ps：数据获取过程可能需要梯子，所有方法都是异步的

核心类：

| 类              | 方法签名                                                                 | 说明                           |
| ---------------- | ---------------------------------------------------------------------------- | -------------------------------- |
| DmhyInfo         |                                                                              | 获取「动漫花园」一些信息 |
|                  | Task<DDramaIndex[]> GetDramaIndexDataAsync();                                | 获取番剧索引数据         |
|                  | Task<DCategory[]> GetDramaCategoryAsync();                                   | 获取番剧类别信息         |
|                  | Task<DTeam[]> GetTeamAsync();                                                | 获取字幕组信息            |
|                  | Task<DHotPost[]> GetHotPostAsync();                                          | 热门资源                     |
| DmhyPost         |                                                                              | 该类负责不同参数获取帖子 |
|                  | Task<DPost[]> GetTopsDataByCategoryIdAsync(long categoryId, long pageIndex); | 根据分类Id获取帖子       |
|                  | Task<DPost[]> GetTopsDataByKeyWordAsync(string keyWord, long pageIndex);     | 根据关键词获取帖子      |
|                  | Task<DPost[]> GetTopsDataByPageIndexAsync(long pageIndex);                   | 获取最新帖子               |
|                  | Task<DPost[]> GetTopsDataByTeamIdAsync(long userId, long pageIndex);         | 获取字幕组的帖子         |
|                  | Task<DPost[]> GetTopsDataByUserIdAsync(long userId, long pageIndex);         | 获取帖                        |
| DmhyPostDetailed |                                                                              | 获取帖子详情和评论      |
|                  | Task<DPostDetailed> GetPostDetailedAsync(string id);                         | 获取帖子详细内容         |
|                  | Task<DComment[]> GetCommentsAsync(string postId);                            | 获取帖子的评论信息      |

DmhyFactory：在不使用依赖注入的情况下需要使用该类生成所需要的核心类 ，包含：DmhyInfo、DmhyPost、DmhyPostDetailed

DmhyExtensions：对 ASP.NET Core 支持，依赖注入。

# v2.0 使用方法 ：

> 1. 调整项目结构，增加了单元测试、ASP.NET Core 测试项目。
> 2. 增加对 ASP.NET Core 支持， 增加依赖注入，同时保留非依赖注入的方式使用。

首先安装 Nuget 包：`Install-Package WuMortal.Dmhy`

## 常规使用

``` cs
using (HttpClient client = new HttpClient())
{
	DmhyFactory dmhyFactory = new DmhyFactory(new System.Net.Http.HttpClient());
	//生成 dmhyInfo 类
	var _dmhyInfo = dmhyFactory.BuilderDmhyInfo();
	//生成 dmhyPost 类
	var _dmhyPost = dmhyFactory.BuilderDmhyPost();
	//调用该类获取热门帖子的方法
	var hotPost =await _dmhyInfo.GetHotPostAsync();
	//获取最新帖子的方法
	var newPost=await _dmhyPost.GetTopsDataByPageIndexAsync(1);
}
```

## ASP.NET Core 使用

v2.0 主要是提供了对 ASP.NET Core 的支持，所以在 ASP.NET Core 中使用是非常简单的。

在 `Startup.cs` 类的 `ConfigureServices()` 方法中添加如下代码：

``` cs
services.AddDmhyAnalysis();
```

之后在需要使用的地方直接注入即可，此处是获取热门帖子的案例：

``` cs
[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
	readonly IDmhyInfo _dmhyInfo;

	public ValuesController(IDmhyInfo dmhyInfo)
	{
		_dmhyInfo = dmhyInfo;
	}

	[HttpGet]
	public async Task<DHotPost[]> Get()
	{
		return await _dmhyInfo.GetHotPostAsync();
	}
}
```

# v1.0 使用方法 ：

推荐使用 v2.0

v1.0 项目地址：[https://github.com/WuMortal/Dmhy-DataAnalysis/tree/v1.0](https://github.com/WuMortal/Dmhy-DataAnalysis/tree/v1.0)
