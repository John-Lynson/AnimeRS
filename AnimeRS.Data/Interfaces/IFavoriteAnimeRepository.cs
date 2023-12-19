using AnimeRS.Data.dto;
using System.Xml.Linq;

namespace AnimeRS.Data.Interfaces
{
    public interface IFavoriteAnimeRepository
    {
        IEnumerable<FavoriteAnimeDTO> GetFavoriteAnimesByAnimeLoverId(int animeLoverId);
        bool AddFavoriteAnime(FavoriteAnimeDTO favoriteAnime);
        bool RemoveFavoriteAnime(int id);
    }
}
