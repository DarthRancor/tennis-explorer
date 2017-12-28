namespace TennisExplorer.Models
{
    public class NavigationEntry
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public PageModels.BasePageModel PageModel { get; set; }
        public Xamarin.Forms.Page Page { get; set; }
    }
}
