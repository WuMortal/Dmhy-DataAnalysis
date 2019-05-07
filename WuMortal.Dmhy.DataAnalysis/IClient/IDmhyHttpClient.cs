using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WuMortal.Dmhy.DataAnalysis.IClient
{
    public interface IDmhyHttpClient
    {
        Task<string> GetStringAsync(string requestUrl);
        Task<string> GetStringAsync(Uri requestUri);

        Task<HttpResponseMessage> GetAsync(string requestUrl);
        Task<HttpResponseMessage> GetAsync(Uri requestUri);

        Task<Stream> GetStreamAsync(string requestUrl);
        Task<Stream> GetStreamAsync(Uri requestUri);
    }
}
