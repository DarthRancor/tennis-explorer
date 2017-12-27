using System;
using System.Collections.Generic;
using TennisExplorer.Services;

namespace TennisExplorer.PageModels
{
    public class TodaysMatchesPageModel : BasePageModel
    {
        private readonly ITennisMatchService _tennisMatchService;
        public List<Models.TennisMatch> Matches { get; set; }

        public TodaysMatchesPageModel(ITennisMatchService tennisMatchService)
        {
            _tennisMatchService = tennisMatchService;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            Matches = new List<Models.TennisMatch>
            {
                new Models.TennisMatch { Players = "Kerber - Hingis", Time = "13.07.2017 15:30", Tour = "ATP", IsFavorite = true},
                new Models.TennisMatch { Players = "Test3 - Test4", Time = "13.07.2017 15:45"},
                new Models.TennisMatch { Players = "Test3 - Test4", Time = "13.07.2017 15:45"}
            };
        }
    }
}
