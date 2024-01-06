using AnimeRS.Core.Models;
using System.Collections.Generic;

public class AnimeDetailsViewModel
{
    public Anime Anime { get; set; }
    public bool FavoriteAnime { get; set; } 
    public IEnumerable<Review> Reviews { get; set; }
}
