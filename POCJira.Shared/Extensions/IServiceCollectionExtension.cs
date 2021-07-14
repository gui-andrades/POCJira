using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POCJira.Shared.Configurations;

namespace POCJira.Shared.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static TConfig ConfigureAppConfiguration<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var config = new TConfig();
            configuration.Bind(config);
            services.AddSingleton(config);
            return config;
        }

        public static TConfig ConfigureAppConfiguration<TConfig>(this IServiceCollection services, IConfiguration configuration, Func<TConfig> provider) where TConfig : class
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            var config = provider();
            configuration.Bind(config);
            services.AddSingleton(config);
            return config;
        }

        public static IServiceCollection AddJiraHttpClient(this IServiceCollection services,
            Application application)
        {

            services.AddTransient<AuthorizationHandler>();

            services.AddHttpClient(Constants.HttpClientName, c =>
            {
                c.BaseAddress = new Uri(application.JiraApiBaseUrl);
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            }).AddHttpMessageHandler<AuthorizationHandler>();

            return services;
        }
    }
}
