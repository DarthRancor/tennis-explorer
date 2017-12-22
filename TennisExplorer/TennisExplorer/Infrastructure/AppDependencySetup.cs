using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using TennisExplorer.Entity.Repository;
using TennisExplorer.Services;

namespace TennisExplorer.Infrastructure
{
    public static class AppDependencySetup
    {
        private static IServiceProvider _provider;
        private static IServiceCollection _services;

        public static void ConfigureDependencies(IServiceCollection services)
        {
            services.AddSingleton<TennisMatchRetriever>();
            services.AddSingleton<IHtmlDownloader, HtmlDownloader>();
            services.AddScoped<ITennisMatchService, TennisMatchService>();
            services.AddScoped<FavoriteService>();
            services.AddScoped<FavoriteRepository>();

            services.AddScoped<PageModels.TodaysMatchesPageModel>();
            services.AddScoped<PageModels.FavoritesPageModel>();
            services.AddScoped<PageModels.MenuPageModel>();
            services.AddScoped<Pages.TodaysMatchesPage>();
            services.AddScoped<Pages.FavoritesPage>();
            services.AddScoped<Pages.MenuPage>();

            services.AddDbContext<Entity.TennisMatchSpyDbContext>();
            services.AddEntityFrameworkSqlite();

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

        public static object Resolve(string name)
        {
            var names = _services.Select(d => d.ServiceType.Name).ToList();
            var test = names.FirstOrDefault(n => n.Contains("Fresh"));
            return _services.Where(d => d.ServiceType.Name == name).FirstOrDefault();
        }

        public static void Register(object instance)
        {
            _services.AddSingleton(instance.GetType(), instance);
            _provider = BuildServiceProvider();
        }

        private static IServiceProvider BuildServiceProvider()
        {
            return _services.BuildServiceProvider();
        }
    }
}
