using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Neleus.DependencyInjection.Extensions;
using System;
using System.Linq;
using System.Reflection;
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
            RegisterServiceIfNotPresent<IHtmlDownloader, HtmlDownloader>(services);
            
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
            var test = names.Where(n => n.Contains(name)).ToList();
            var navigation = names.Where(n => n.Contains(FreshMvvm.Constants.DefaultNavigationServiceName)).ToList();
            return _services.Where(d => d.ServiceType.Name == name).FirstOrDefault();
        }

        public static void Register(object instance, string name)
        {
            // _services.AddByName<IService>()
            MethodInfo addByNameMethod = _services.GetType().GetMethod("AddByName");
            MethodInfo addByNameGenericMethod = addByNameMethod.MakeGenericMethod(instance.GetType());
            var servicesByNameBuilder = addByNameGenericMethod.Invoke(_services, null);

            // .Add<ServiceA>("key1")
            MethodInfo addMethod = servicesByNameBuilder.GetType().GetMethod("Add");
            MethodInfo addMethodGenericMethod = addMethod.MakeGenericMethod(instance.GetType());
            var addedMethod = addMethodGenericMethod.Invoke(servicesByNameBuilder, new object[] { name });

            //_services.AddSingleton(instance.GetType(), instance);
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
