using System;

namespace AnimeRS.Core.ViewModels
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public int AnimeId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime DatePosted { get; set; }
        public string AnimeTitle { get; set; }
    }
}
