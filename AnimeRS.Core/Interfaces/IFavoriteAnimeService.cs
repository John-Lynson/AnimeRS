using AnimeRS.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Core.Interfaces
{
    public interface IFavoriteAnimeService
    {
        void ToggleFavoriteAnime(int animeLoverId, int animeId);
        IEnumerable<FavoriteAnimeViewModel> GetFavoriteAnimesByAnimeLoverId(int animeLoverId);
    }
}
