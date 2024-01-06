using AnimeRS.Core.Models;
using System.Collections.Generic;

namespace AnimeRS.Core.ViewModels
{
    public class FavoriteAnimeViewModel
    {
        public int AnimeId { get; set; }
        public int AnimeLoverId { get; set; }
        public string AnimeTitle { get; set; }
        // Voeg andere eigenschappen toe indien nodig
    }
}