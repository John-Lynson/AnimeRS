using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Core.Models
{
    public class FavoriteAnime
    {
        public int Id { get; set; }
        public int AnimeLoverId { get; set; } 
        public int AnimeId { get; set; } 

    }
}
