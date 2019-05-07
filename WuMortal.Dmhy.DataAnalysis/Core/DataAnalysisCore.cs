using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            if (serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));

            var types = Assembly.GetAssembly(typeof(DmhyInfo)).GetTypes().Where(w => w.Name.StartsWith("Dmhy") && w.IsClass && !w.IsAbstract);

            foreach (var type in types)
            {
                var interfaceType = type.GetInterfaces().FirstOrDefault(w => w.Name == $"I{type.Name}");
                if (interfaceType == null) throw new NotImplementedException($"Not found Interface:{nameof(type)}");

                serviceCollection.AddSingleton(interfaceType, type);
            }

            serviceCollection.AddHttpClient<IDmhyHttpClient, DmhyHttpClient>();

            return serviceCollection;
        }
    }
}
