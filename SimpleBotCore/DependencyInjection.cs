using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleBotCore.Config;
using SimpleBotCore.Repositories;
using System;
using System.Data.SqlClient;

namespace SimpleBotCore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongoDBConfiguration(configuration);

            services.AddSQLServerConfiguration(configuration);

            return services;
        }

        private static IServiceCollection AddMongoDBConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            bool mongoEnabled;

            bool.TryParse(configuration.GetSection("ConnectionStrings:MongoDBConnection:Enabled").Value, out mongoEnabled);

            services.AddOptions<MongoDBConnection>()
                    .Configure<IConfiguration>((settings, configuration) =>
                    {
                        configuration.GetSection("ConnectionStrings:MongoDBConnection").Bind(settings);
                    });           

            if (mongoEnabled)
            {
                services.AddSingleton<IQuestionRepository, QuestionRepository>();
            }           
            else
            {
                services.AddSingleton<IQuestionRepository, QuestionMockRepository>();
            }

            return services;
        }

        private static IServiceCollection AddSQLServerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            string sqlEnabled = configuration.GetSection("ConnectionStrings:SQLSeverConnection").Value;

            services.AddOptions<SQLConfigConnection>()
                    .Configure<IConfiguration>((settings, configuration) =>
                    {
                        configuration.GetSection("ConnectionStrings:SQLSeverConnection").Bind(settings);
                    });

            if (!string.IsNullOrEmpty(sqlEnabled))
            {
                services.AddSingleton<IUserProfileRepository, UserProfileSqlRepository>();
            }
            else
            {
                services.AddSingleton<IUserProfileRepository, UserProfileMockRepository>();
            }

            return services;
        }
    }
}