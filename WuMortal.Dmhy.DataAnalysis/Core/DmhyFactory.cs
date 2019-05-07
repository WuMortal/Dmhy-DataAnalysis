using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WuMortal.Dmhy.DataAnalysis.Client;
using WuMortal.Dmhy.DataAnalysis.IClient;
using WuMortal.Dmhy.DataAnalysis.Interface;

namespace WuMortal.Dmhy.DataAnalysis.Core
{
    public class DmhyFactory
    {
        readonly IDmhyHttpClient _dmhyHttpClient;

        public DmhyFactory(HttpClient httpClient)
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            _dmhyHttpClient = new DmhyHttpClient(httpClient);
        }

        public IDmhyPostDetailed BuilderDmhyPostDetailed()
        {
            return new DmhyPostDetailed(_dmhyHttpClient);
        }

        public IDmhyInfo BuilderDmhyInfo()
        {
            return new DmhyInfo(_dmhyHttpClient);
        }

        public IDmhyPost BuilderDmhyPost()
        {
            return new DmhyPost(_dmhyHttpClient);
        }
    }
}
