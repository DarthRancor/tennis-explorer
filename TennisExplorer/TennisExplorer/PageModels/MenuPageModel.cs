using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace TennisExplorer.PageModels
{
    public class MenuPageModel : BasePageModel
    {
        public List<Models.NavigationEntry> NavigationEntries { get; private set; }

        private Models.NavigationEntry _selectedNavigationEntry;
        public Models.NavigationEntry SelectedNavigationEntry
        {
            get { return _selectedNavigationEntry; }
            set
            {
                _selectedNavigationEntry = value;
                NavigateToPageCommand.Execute(value);
            }
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            NavigationEntries = new List<Models.NavigationEntry>
            {
                new Models.NavigationEntry { Name = "Matches Today", PageModel = Infrastructure.AppDependencySetup.Resolve<TodaysMatchesPageModel>() },
                new Models.NavigationEntry { Name = "Favorites", PageModel = Infrastructure.AppDependencySetup.Resolve<FavoritesPageModel>() }
            };
        }

        public Command<Models.NavigationEntry> NavigateToPageCommand
        {
            get
            {
                return new Command<Models.NavigationEntry>(async (entry) =>
                {
                    await CoreMethods.PushPageModel(entry.PageModel.GetType());
                });
            }
        }
    }


}
