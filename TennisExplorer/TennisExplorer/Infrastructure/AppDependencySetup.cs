using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Linq;
using TennisExplorer.Entity.Repository;
using TennisExplorer.Services;

namespace TennisExplorer.Infrastructure
{
    public static class AppDependencySetup
    {
        private static IServiceProvider _provider;
        private static IServiceCollection _services;
        private static ConcurrentDictionary<string, object> _namedRegistrations = new ConcurrentDictionary<string, object>();

        public static void ConfigureDependencies(IServiceCollection services)
        {
            services.AddSingleton<TennisMatchRetriever>();
            RegisterServiceIfNotPresent<IHtmlDownloader, HtmlDownloader>(services);

            services.AddScoped<ITennisMatchService, TennisMatchService>();
            services.AddScoped<FavoriteService>();
            services.AddScoped<FavoriteRepository>();
            
            services.AddScoped<PageModels.TodaysMatchesPageModel>();
            services.AddScoped<PageModels.FavoritesListPageModel>();
            services.AddSingleton<PageModels.Menu.TennisExplorerMasterDetailPageModel>();
            services.AddSingleton<PageModels.Menu.MenuDrawerMasterPageModel>();
            
            services.AddScoped<Pages.TodaysMatchesPage>();
            services.AddScoped<Pages.FavoritesListPage>();
            services.AddSingleton<Pages.Menu.TennisExplorerMasterDetailPage>();
            services.AddSingleton<Pages.Menu.MenuDrawerMasterPage>();

            _services = services;
            _provider = BuildServiceProvider();
        }

        public static TService Resolve<TService>()
        {
            return _provider.GetRequiredService<TService>();
        }

        public static object Resolve(Type type)
        {
            return _provider.GetService(type);
        }

        public static TService Resolve<TService>(string name)
        {
            var service = _namedRegistrations[name];
            return (TService) service;
        }

        public static void Register(object instance, string name)
        {
            var test = _namedRegistrations.AddOrUpdate(name, instance, (key, oldValue) => instance);

            _services.AddSingleton(instance.GetType(), instance);
            _provider = BuildServiceProvider();
        }

        private static void RegisterServiceIfNotPresent<IService, TService>(IServiceCollection services) where TService : class, IService where IService : class
        {
            // only register the TService if it was not registered (by the test env)
            if (!IsServiceTypeRegistered(services, typeof(IService)))
            {
                services.AddScoped<IService, TService>();
            }
        }

        private static bool IsServiceTypeRegistered(IServiceCollection services, Type serviceType)
        {
            return services.Any(descriptor => descriptor.ServiceType == serviceType);
        }

        private static IServiceProvider BuildServiceProvider()
        {
            return _services.BuildServiceProvider();
        }
    }
}
