using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly : LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace lambda_dotnet_console
{
    public class Function
    {

        /// <summary>
        /// Lamda Function
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns>string</returns>
        public string FunctionHandler(string input, ILambdaContext context)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional : false, reloadOnChange : true)
                .AddEnvironmentVariables(prefix: "LAMBDA_")
                .Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var appSettings = new AppSettings();
            configuration.GetSection("App").Bind(appSettings);
            serviceCollection.AddSingleton<AppSettings>(appSettings);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider.GetService<App>().Run(input);
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<App>();
        }
    }

}