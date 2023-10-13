using AnimeRS.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimeRS.Core.Interfaces
{
    public interface IFavoriteAnimeRepository
    {
        Task<IEnumerable<FavoriteAnime>> GetFavoriteAnimesByAnimeLoverIdAsync(int animeLoverId);
        Task<bool> AddFavoriteAnimeAsync(FavoriteAnime favoriteAnime);
        Task<bool> RemoveFavoriteAnimeAsync(int id);
    }
}
