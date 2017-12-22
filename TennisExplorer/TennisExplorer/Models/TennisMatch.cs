using System;

namespace TennisExplorer.Models
{
    public class TennisMatch
    {
        public long Id { get; set; }
        public string Time { get; set; }
        public string Tour { get; set; }
        public string Players { get; set; }
        public bool IsFavorite { get; set; }
    }
}
