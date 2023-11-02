
using AnimeRS.Core.Models;
using System.Collections.Generic;

namespace AnimeRS.Core.Interfaces
{
    public interface IFavoriteAnimeRepository
    {
        IEnumerable<FavoriteAnime> GetFavoriteAnimesByAnimeLoverId(int animeLoverId);
        bool AddFavoriteAnime(FavoriteAnime favoriteAnime);
        bool RemoveFavoriteAnime(int id);
    }
}
