using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Core.Models
{
    public class Anime
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Genre { get; private set; }
        public int Episodes { get; private set; }
        public string Status { get; private set; }
        public DateTime ReleaseDate { get; private set; }

        public virtual ICollection<Review> Reviews { get; private set; } = new List<Review>();

        public Anime(int id, string title, string description, string genre, int episodes, string status, DateTime releaseDate)
        {
            Id = id;
            Title = title;
            Description = description;
            Genre = genre;
            Episodes = episodes;
            Status = status;
            ReleaseDate = releaseDate;
        }
    }
}