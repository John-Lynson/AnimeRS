using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Core.Models
{
    public class AnimeRating
    {
        public int Id { get; private set; }
        public int AnimeId { get; private set; }
        public int AnimeLoverId { get; private set; }
        public int Rating { get; private set; }

        public AnimeRating(int animeId, int animeLoverId, int rating)
        {
            AnimeId = animeId;
            AnimeLoverId = animeLoverId;
            Rating = rating;
        }

    }

}
