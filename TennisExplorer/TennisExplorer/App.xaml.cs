using Xamarin.Forms;

namespace TennisExplorer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitializeApplication();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void InitializeApplication()
        {
            InitializeIocContainer();
            InitializeNavigation();
        }

        private void InitializeIocContainer()
        {
            var customIoc = new Infrastructure.CustomFreshMvvmIocContainer();
            FreshMvvm.FreshIOC.OverrideContainer(customIoc);
        }

        private void InitializeNavigation()
        {
            MainPage = new Pages.Menu.TennisExplorerMasterDetailPage();
        }
    }
}
