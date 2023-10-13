using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Core.DTOs
{
    public class AnimeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public int Episodes { get; set; }
        public string Status { get; set; }
        public string AiringDate { get; set; }
    }
}

