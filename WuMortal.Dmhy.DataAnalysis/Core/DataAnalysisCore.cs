using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using WuMortal.Dmhy.DataAnalysis.Client;
using WuMortal.Dmhy.DataAnalysis.IClient;

namespace WuMortal.Dmhy.DataAnalysis.Core
{
    public static class DataAnalysisCore
    {
        public static IServiceCollection UseDmhyAnalysis(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient<IDmhyHttpClient, DmhyHttpClient>();

            return serviceCollection;
        }
    }
}
