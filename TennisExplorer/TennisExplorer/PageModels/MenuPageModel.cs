using System;
using System.Collections.Generic;
using System.Linq;
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
            var navigationContainer = Infrastructure.AppDependencySetup.Resolve<Infrastructure.MasterDetailNavigationContainer>();
            NavigationEntries = navigationContainer.NavigationEntries.ToList();
        }

        public Command<Models.NavigationEntry> NavigateToPageCommand
        {
            get
            {
                return new Command<Models.NavigationEntry>((entry) =>
                {
                    var navigationContainer = Infrastructure.AppDependencySetup.Resolve<Infrastructure.MasterDetailNavigationContainer>();
                    navigationContainer.NavigateToPage(entry);
                });
            }
        }
    }


}
