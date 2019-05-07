using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WuMortal.Dmhy.DataAnalysis.IClient;

namespace WuMortal.Dmhy.DataAnalysis.Core
{
    internal class Common
    {
        public async static Task<string> DownloadHtmlAsync(IDmhyHttpClient client, string url)
        {
            using (HttpResponseMessage message = await client.GetAsync(url))
            {
                if (message.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"请求:{url}时，未得到正确的响应。错误响应为:{message.StatusCode},响应内容为：{message.Content}");
                }

                return await message.Content.ReadAsStringAsync();

            }
        }
    }
}
