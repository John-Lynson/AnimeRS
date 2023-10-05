using AnimeRS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Data.Repositories
{
    public interface IAnimeRepository
    {
        Task<IEnumerable<Anime>> GetAllAsync();
        Task<Anime> GetAsync(int id);
        Task AddAsync (Anime anime);
    }
}
