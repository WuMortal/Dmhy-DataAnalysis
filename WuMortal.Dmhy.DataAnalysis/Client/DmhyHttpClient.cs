using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WuMortal.Dmhy.DataAnalysis.IClient;

namespace WuMortal.Dmhy.DataAnalysis.Client
{
    public class DmhyHttpClient : IDmhyHttpClient
    {
        readonly HttpClient _client;

        public DmhyHttpClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://share.dmhy.org");
            _client = client;
        }

        #region Implementation of IWechatHttpClient

        public async Task<string> GetStringAsync(string requestUrl)
        {
            return await _client.GetStringAsync(requestUrl);
        }

        public async Task<string> GetStringAsync(Uri requestUri)
        {
            return await _client.GetStringAsync(requestUri);
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUrl)
        {
            return await _client.GetAsync(requestUrl);
        }

        public async Task<HttpResponseMessage> GetAsync(Uri requestUri)
        {
            return await _client.GetAsync(requestUri);
        }

        public async Task<Stream> GetStreamAsync(string requestUrl)
        {
            return await _client.GetStreamAsync(requestUrl);
        }

        public async Task<Stream> GetStreamAsync(Uri requestUri)
        {
            return await _client.GetStreamAsync(requestUri);
        }
        #endregion
    }
}
