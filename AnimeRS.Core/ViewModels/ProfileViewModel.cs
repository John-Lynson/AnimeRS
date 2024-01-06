using AnimeRS.Core.Models;
using System.Collections.Generic;

namespace AnimeRS.Core.ViewModels
{
    public class ProfileViewModel
    {
        public AnimeLover AnimeLover { get; set; }
        public IEnumerable<FavoriteAnimeViewModel> FavoriteAnimes { get; set; }
    }
}


