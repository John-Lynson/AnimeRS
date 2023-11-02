using AnimeRS.Core.Models;
using System.Collections.Generic;

namespace AnimeRS.Core.Interfaces
{
    public interface IAnimeLoverRepository
    {
        IEnumerable<AnimeLover> GetAllAnimeLovers();
        AnimeLover GetAnimeLoverById(int id);
        AnimeLover GetAnimeLoverByUsername(string username);
        bool AddAnimeLover(AnimeLover animeLover);
        bool UpdateAnimeLover(AnimeLover animeLover);
        bool DeleteAnimeLover(int id);
    }
}
