using AnimeRS.Core.Models;
using System.Collections.Generic;

namespace AnimeRS.Core.Interfaces
{
    public interface IAnimeLoverRepository
    {
        IEnumerable<AnimeLover> GetAllAnimeLovers();
        AnimeLover GetAnimeLoverById(int id);
        AnimeLover GetAnimeLoverByUsername(string username);
        AnimeLover GetByAuth0UserId(string auth0UserId);
        bool AddAnimeLover(AnimeLover animeLover);
        bool UpdateAnimeLover(AnimeLover animeLover);
        bool DeleteAnimeLover(int id);
    }
}
