using FreshMvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace TennisExplorer.PageModels.Menu
{
    public class MenuDrawerMasterPageModel : BasePageModel
    {
        public ObservableCollection<Models.NavigationEntry> NavigationEntries { get; set; }

        public Models.NavigationEntry SelectedNavigationEntry { get; set; }

        public override void Init(object initData)
        {
			NavigationEntries = new ObservableCollection<Models.NavigationEntry>(new[]
            {
                new Models.NavigationEntry
                {
                    PageModel = Infrastructure.AppDependencySetup.Resolve<TodaysMatchesPageModel>(),
                    Page = FreshPageModelResolver.ResolvePageModel<TodaysMatchesPageModel>(),
                    Title = "Matches Today",
                    Icon = "ic_today_black_24dp.png"
                },
                new Models.NavigationEntry
                {
                    PageModel = Infrastructure.AppDependencySetup.Resolve<FavoritesListPageModel>(),
                    Page = FreshPageModelResolver.ResolvePageModel<FavoritesListPageModel>(),
                    Title = "Favorites",
                    Icon = "ic_grade_black_24dp.png"
                }
            });

            
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            var startNavigationEntry = NavigationEntries.First(entry => entry.PageModel.GetType() == typeof(TodaysMatchesPageModel));
            SelectedNavigationEntry = startNavigationEntry;
        }

        public Command<Models.NavigationEntry> NavigateToPageCommand
        {
            get
            {
                return new Command<Models.NavigationEntry>(
                  (entry) =>
                  {
                      var message = new Models.Messages.NavigationMessage() { NavigationEntry = entry };
                      MessagingCenter.Send(message, string.Empty);
                  },
                  (entry) => 
                  {
                          // CanExecute delegate
                          return true;
                  });
            }
        }
    }
}
