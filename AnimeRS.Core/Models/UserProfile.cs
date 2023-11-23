using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Core.Models
{
    public class UserProfile
    {
        public int Id { get; private set; }
        public int AnimeLoverId { get; private set; }
        public string FavoriteGenre { get; private set; }
        public List<int> WatchedAnimeIds { get; private set; }

        public UserProfile(int animeLoverId, string favoriteGenre, List<int> watchedAnimeIds)
        {
            AnimeLoverId = animeLoverId;
            FavoriteGenre = favoriteGenre;
            WatchedAnimeIds = watchedAnimeIds ?? new List<int>();
        }
    }
}