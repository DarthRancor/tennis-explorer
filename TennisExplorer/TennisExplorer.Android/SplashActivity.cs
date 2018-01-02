using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System.Threading.Tasks;
using TennisExplorer.Infrastructure;

namespace TennisExplorer.Droid
{
    // @style/MyTheme.Splash
    [Activity(Theme = "@android:style/Theme.NoTitleBar", MainLauncher = true, Immersive = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SplashScreen);
            FindViewById<TextView>(Resource.Id.txtAppVersion).Text = $"Version {PackageManager.GetPackageInfo(PackageName, 0).VersionName}";
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            InitializeApplication().ContinueWith((task) =>
            {
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            });
        }

        // Prevent the back button from canceling the startup process
        public override void OnBackPressed() { }
        
        private async Task InitializeApplication()
        {
            // initialize the depenendencies from here to enable native services
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            await Bootstrapper.InitializeApplicationDependencies(services);
        }
    }
}