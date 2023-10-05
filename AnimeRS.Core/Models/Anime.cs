using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Core.Models
{
    public class Anime
    {
        public int ID { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }   
        public string Genre { get; set; } // later weizigen.

    }
}
