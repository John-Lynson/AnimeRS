using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Core.Models
{
    public class Anime
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public int Episodes { get; set; }
        public string Status { get; set; }
        public DateTime ReleaseDate { get; set; }

        public string ImageURL { get; set; }

    }
}