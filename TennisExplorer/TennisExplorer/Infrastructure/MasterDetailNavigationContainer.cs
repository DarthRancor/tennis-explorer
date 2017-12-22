using FreshMvvm;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TennisExplorer.Infrastructure
{
    public class MasterDetailNavigationContainer : MasterDetailPage, IFreshNavigationService
    {
        public string NavigationServiceName { get; private set; }

        private FreshNavigationContainer _navHome;
        private FreshNavigationContainer _navFavorites;

        private Page _todaysMatchesPage;
        private Page _favoritesPage;

        public MasterDetailNavigationContainer()
        {
            NavigationServiceName = nameof(CustomFreshMvvmIocContainer);

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
            _favoritesPage = FreshPageModelResolver.ResolvePageModel<PageModels.FavoritesPageModel>();
            _navFavorites = new FreshNavigationContainer(_favoritesPage);

            _todaysMatchesPage = FreshPageModelResolver.ResolvePageModel<PageModels.TodaysMatchesPageModel>();
            _navHome = new FreshNavigationContainer(_todaysMatchesPage);

            Detail = _navHome;
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

            //var listView = new ListView
            //{
            //    ItemsSource = new string[] { "Matches Today", "Favorites" }
            //};

            //listView.ItemSelected += (sender, args) =>
            //{

            //    switch ((string)args.SelectedItem)
            //    {
            //        case "Matches Today":
            //            Detail = _navHome;
            //            break;

            //        case "Favorites":
            //            Detail = _navFavorites;
            //            break;

            //        default:
            //            break;
            //    }

            //    IsPresented = false;
            //};

            //menuPage.Content = listView;

            //Master = new NavigationPage(menuPage) { Title = "Menu" };
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

        public virtual async Task PushPage(Page page, FreshBasePageModel model, bool modal = false, bool animate = true)
        {
            await Navigation.PushModalAsync(new NavigationPage(page), animate);
        }

        public Task<FreshBasePageModel> SwitchSelectedRootPageModel<T>() where T : FreshBasePageModel
        {
            throw new NotImplementedException();
        }
    }
}
