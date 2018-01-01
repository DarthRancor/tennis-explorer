using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using System.Threading.Tasks;
using TennisExplorer.Infrastructure;

namespace TennisExplorer.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            InitializeApplication().ContinueWith((task) =>
            {
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            });
            //Task startupWork = new Task(() => { InitializeApplication(); });
            //startupWork.Start();

           
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