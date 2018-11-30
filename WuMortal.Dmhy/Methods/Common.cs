using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace System.Net.Http
{
    internal class Common
    {
        public async static Task<string> DownloadHtmlAsync(HttpClient client, string url)
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
