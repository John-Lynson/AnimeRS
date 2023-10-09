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
        public int Id { get; private set; }
        public int AnimeId { get; private set; } // Foreign Key
        public string Comment { get; private set; }
        public int Rating { get; private set; }
        public DateTime DatePosted { get; private set; }

        public Review(int id, int animeId, string comment, int rating, DateTime datePosted)
        {
            Id = id;
            AnimeId = animeId;
            Comment = comment ?? throw new ArgumentNullException(nameof(comment));
            Rating = rating;
            DatePosted = datePosted;
        }
    }
}
