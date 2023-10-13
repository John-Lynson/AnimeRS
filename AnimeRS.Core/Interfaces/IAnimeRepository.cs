using AnimeRS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Core.Interfaces
{
    public interface IAnimeRepository
    {
        Task<IEnumerable<Anime>> GetAllAnimesAsync();
        Task<Anime> GetAnimeByIdAsync(int id);
        Task AddAnimeAsync(Anime anime);
        Task UpdateAnimeAsync(Anime anime);
        Task DeleteAnimeAsync(int id);
    }
}
