using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisExplorer.Models;
using Xamarin.Forms;

namespace TennisExplorer.Infrastructure
{
    public class MasterDetailNavigationContainer : MasterDetailPage, IFreshNavigationService
    {
        public string NavigationServiceName { get; private set; }
        public List<NavigationEntry> NavigationEntries { get; private set; }

        public MasterDetailNavigationContainer()
        {
            NavigationServiceName = Constants.DefaultNavigationServiceName;

            InitializeNavigationEntries();
            CreateMenuPage("Menu");
            RegisterNavigation();
        }

        protected void RegisterNavigation()
        {
            FreshIOC.Container.Register<IFreshNavigationService>(this, NavigationServiceName);
        }

        protected void InitializeNavigationEntries()
        {
            NavigationEntries = new List<NavigationEntry>
            {
                new NavigationEntry
                {
                    PageModel = AppDependencySetup.Resolve<PageModels.TodaysMatchesPageModel>(),
                    Page = FreshPageModelResolver.ResolvePageModel<PageModels.TodaysMatchesPageModel>(),
                    Name = "Matches Today",
                    Icon = "ic_today_black_24dp.png"
                },
                new NavigationEntry
                {
                    PageModel = AppDependencySetup.Resolve<PageModels.FavoritesPageModel>(),
                    Page = FreshPageModelResolver.ResolvePageModel<PageModels.FavoritesPageModel>(),
                    Name = "Favorites",
                    Icon = "ic_grade_black_24dp.png"
                }
            };
        }

        public void NavigateToPage(NavigationEntry entry)
        {
            var page = NavigationEntries.Single(e => e == entry).Page;

            var navigationContainer = CreateNavigationPage<NavigationPage>(entry.Name, page);
            Detail = navigationContainer;
            IsPresented = false;
        }

        protected void CreateMenuPage(string menuPageTitle)
        {
            var menuPage = FreshPageModelResolver.ResolvePageModel<PageModels.MenuPageModel>();

            Master = CreateNavigationPage<NavigationPage>(menuPageTitle, menuPage);
            NavigateToPage(NavigationEntries.First());
        }

        private TNavigationPage CreateNavigationPage<TNavigationPage>(string title, Page page) where TNavigationPage : NavigationPage
        {
            var navigationPage = (TNavigationPage)Activator.CreateInstance(typeof(TNavigationPage), page);
            navigationPage.Title = title;
            navigationPage.BarBackgroundColor = Color.FromHex("#81c784");
            navigationPage.BarTextColor = Color.Black;

            return navigationPage;
        }

        public void NotifyChildrenPageWasPopped()
        {
            throw new NotImplementedException();
        }

        public Task PopPage(bool modal = false, bool animate = true)
        {
            throw new NotImplementedException();
        }

        public Task PopToRoot(bool animate = true)
        {
            throw new NotImplementedException();
        }

        public virtual Task PushPage(Page page, FreshBasePageModel model, bool modal = false, bool animate = true)
        {
            throw new NotImplementedException();
        }

        public Task<FreshBasePageModel> SwitchSelectedRootPageModel<T>() where T : FreshBasePageModel
        {
            throw new NotImplementedException();
        }
    }
}
