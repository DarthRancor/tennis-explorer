using System.ComponentModel;

namespace TennisExplorer.PageModels
{
    public class BasePageModel : FreshMvvm.FreshBasePageModel
    {
        public bool IsBusy { get; set; }
        public string Title { get; set; }
    }
}
