using System;
using System.Collections.Generic;
using System.Text;

namespace TennisExplorer.Models
{
    public class NavigationEntry
    {
        public string Name { get; set; }
        public PageModels.BasePageModel PageModel { get; set; }
    }
}
