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
            var defaultNavigationEntry = new NavigationEntry
            {
                PageModel = AppDependencySetup.Resolve<PageModels.TodaysMatchesPageModel>(),
                Page = FreshPageModelResolver.ResolvePageModel<PageModels.TodaysMatchesPageModel>(),
                Name = "Matches Today"
            };

            NavigationEntries = new List<NavigationEntry>
            {
                defaultNavigationEntry,
                new NavigationEntry
                {
                    PageModel = AppDependencySetup.Resolve<PageModels.FavoritesPageModel>(),
                    Page = FreshPageModelResolver.ResolvePageModel<PageModels.FavoritesPageModel>(),
                    Name = "Favorites"
                }
            };

            Detail = new FreshNavigationContainer(defaultNavigationEntry.Page);
        }

        public void NavigateToPage(NavigationEntry entry)
        {
            var page = NavigationEntries.Single(e => e == entry).Page;
            
            var navigationContainer = new NavigationPage(page);
            Detail = navigationContainer;
            IsPresented = false;
        }

        protected void CreateMenuPage(string menuPageTitle)
        {
            var menuPage = FreshPageModelResolver.ResolvePageModel<PageModels.MenuPageModel>();
            var menuPageNav = new NavigationPage(menuPage)
            {
                Title = "Master",
                BarBackgroundColor = Color.FromHex("#81c784"),
                BarTextColor = Color.Black
            };

            Master = menuPageNav;
            Detail = new FreshNavigationContainer(NavigationEntries.First().Page);
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
