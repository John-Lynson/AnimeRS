using AnimeRS.Core.Models;
using System.Collections.Generic;

public class UserProfileViewModel
{
    public string Username { get; set; }
    public string Role { get; set; }
    public List<Anime> FavoriteAnimes { get; set; }

    public AnimeLover AnimeLover { get; set; }
}


