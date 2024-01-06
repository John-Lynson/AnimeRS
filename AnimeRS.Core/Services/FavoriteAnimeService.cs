using AnimeRS.Core.Models;
using AnimeRS.Data.dto;
using AnimeRS.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimeRS.Core.Services
{
    public class FavoriteAnimeService
    {
        private readonly IFavoriteAnimeRepository _favoriteAnimeRepository;

        public FavoriteAnimeService(IFavoriteAnimeRepository favoriteAnimeRepository)
        {
            _favoriteAnimeRepository = favoriteAnimeRepository;
        }

        public IEnumerable<FavoriteAnime> GetFavoriteAnimesByAnimeLoverId(int animeLoverId)
        {
            var favoriteAnimeDTOs = _favoriteAnimeRepository.GetFavoriteAnimesByAnimeLoverId(animeLoverId);
            return favoriteAnimeDTOs.Select(AnimeRSConverter.ConvertToDomain);
        }

        public bool AddFavoriteAnime(int animeLoverId, int animeId)
        {
            var favoriteAnime = new FavoriteAnimeDTO
            {
                AnimeLoverId = animeLoverId,
                AnimeId = animeId
            };
            return _favoriteAnimeRepository.AddFavoriteAnime(favoriteAnime);
        }

        public bool RemoveFavoriteAnime(int animeLoverId, int animeId)
        {
            // Je moet mogelijk de logica aanpassen om de juiste ID te vinden voor het verwijderen
            var favoriteAnimes = _favoriteAnimeRepository.GetFavoriteAnimesByAnimeLoverId(animeLoverId);
            var favoriteAnime = favoriteAnimes.FirstOrDefault(fa => fa.AnimeId == animeId);
            if (favoriteAnime != null)
            {
                return _favoriteAnimeRepository.RemoveFavoriteAnime(favoriteAnime.AnimeLoverId);
            }
            return false;
        }

public void ToggleFavoriteAnime(int animeLoverId, int animeId)
{
    var favoriteAnimes = GetFavoriteAnimesByAnimeLoverId(animeLoverId);
    var favoriteAnime = favoriteAnimes.FirstOrDefault(fa => fa.AnimeId == animeId);

    if (favoriteAnime != null)
    {
        RemoveFavoriteAnime(animeLoverId, animeId);
    }
    else
    {
        AddFavoriteAnime(animeLoverId, animeId);
    }
}

    }
}
