using FreshMvvm;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TennisExplorer.Pages.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TennisExplorerMasterDetailPage : MasterDetailPage, IFreshNavigationService
    {
        public TennisExplorerMasterDetailPage()
        {
            InitializeComponent();
            BindingContext = Infrastructure.AppDependencySetup.Resolve<PageModels.Menu.TennisExplorerMasterDetailPageModel>();

            Master = FreshPageModelResolver.ResolvePageModel<PageModels.Menu.MenuDrawerMasterPageModel>();
            Detail = new NavigationPage(FreshPageModelResolver.ResolvePageModel<PageModels.TodaysMatchesPageModel>());

            RegisterNavigationService();
            RegisterToNavigationRequests();
        }

        private void RegisterToNavigationRequests()
        {
            MessagingCenter.Subscribe<Models.Messages.NavigationMessage>(this, string.Empty, (message) =>
            {
                NavigateToPage(message.NavigationEntry);
            });
        }

        public string NavigationServiceName => Constants.DefaultNavigationServiceName;

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

        public void NavigateToPage(Models.NavigationEntry navigationEntry)
        {
            var targetPage = new NavigationPage(navigationEntry.Page)
            {
                BindingContext = navigationEntry.PageModel
            };
            Detail = targetPage;
            IsPresented = false;
        }

        public Task<FreshBasePageModel> SwitchSelectedRootPageModel<T>() where T : FreshBasePageModel
        {
            throw new NotImplementedException();
        }

        protected void RegisterNavigationService()
        {
            FreshIOC.Container.Register<IFreshNavigationService>(this, NavigationServiceName);
        }
    }
}