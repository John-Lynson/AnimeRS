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
        IEnumerable<Anime> GetAllAnimes();
        Anime GetAnimeById(int id);
        Task AddAnime(Anime anime);
        Task UpdateAnime(Anime anime);
        Task DeleteAnime(int id);
    }
}
