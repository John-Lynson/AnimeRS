using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Core.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        public int AnimeLoverId { get; set; }
        public string FavoriteGenre { get; set; }
        public List<int> WatchedAnimeIds { get; set; }

    }
}