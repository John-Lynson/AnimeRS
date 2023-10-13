using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Core.Models
{
    public class FavoriteAnime
    {
        public int Id { get; private set; }
        public int AnimeLoverId { get; private set; } // Fkey
        public int AnimeId { get; private set; } // Fkey

        public FavoriteAnime(int animeLoverId, int animeId)
        {
            AnimeLoverId = animeLoverId;
            AnimeId = animeId;
        }

    }
}
