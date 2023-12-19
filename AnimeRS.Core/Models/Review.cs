using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Core.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int AnimeId { get; set; } // Foreign Key
        public int AnimeLoverId { get; set; } // Foreign Key
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime DatePosted { get; set; }

        public virtual Anime Anime { get; set; }
        public virtual AnimeLover AnimeLover { get; set; }
    }
}
