using AnimeRS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Data.Repositories
{
    public interface IAnimeLoverRepository
    {
        Task<IEnumerable<AnimeLover>> GetAllAnimeLoversAsync();
        Task<AnimeLover> GetAnimeLoverByIdAsync(int id);
        Task<AnimeLover> GetAnimeLoverByUsernameAsync(string username);
        Task<bool> AddAnimeLoverAsync(AnimeLover animeLover);
        Task<bool> UpdateAnimeLoverAsync(AnimeLover animeLover);
        Task<bool> DeleteAnimeLoverAsync(int id);
    }
}
