using AnimeRS.Data.dto;
using System.Xml.Linq;

namespace AnimeRS.Data.Interfaces
{
    public interface IAnimeLoverRepository
    {
        IEnumerable<AnimeLoverDTO> GetAllAnimeLovers();
        AnimeLoverDTO GetAnimeLoverById(int id);
        AnimeLoverDTO GetAnimeLoverByUsername(string username);
        AnimeLoverDTO GetByAuth0UserId(string auth0UserId);
        bool AddAnimeLover(AnimeLoverDTO animeLover);
        bool UpdateAnimeLover(AnimeLoverDTO animeLover);
        bool DeleteAnimeLover(int id);
    }
}
