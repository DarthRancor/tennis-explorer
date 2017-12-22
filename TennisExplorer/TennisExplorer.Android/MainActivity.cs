using Android.App;
using Android.Content.PM;
using Android.OS;
using TennisExplorer.Infrastructure;

namespace TennisExplorer.Droid
{
	[Activity(Label = "TennisExplorer", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
			AppDependencySetup.ConfigureDependencies(services);
			LoadApplication(new App());
		}
	}
}

