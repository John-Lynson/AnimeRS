using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Data.dto
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int AnimeId { get; set; }
        public int AnimeLoverId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
