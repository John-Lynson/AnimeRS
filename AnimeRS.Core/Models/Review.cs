using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Core.Models
{
    internal class Review
    {
        public int Id { get; set; }
        public int AnimeId { get; set; } // Fkey
        public int UserId { get; set; } // Fkey
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime PostDate { get; set; }
    }
}
