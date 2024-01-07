using AnimeRS.Core.Models;
using AnimeRS.Core.ViewModels;
using AnimeRS.Data.dto;
using AnimeRS.Data.Interfaces;
using AnimeRS.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimeRS.Core.Services
{
    public class FavoriteAnimeService
    {
        private readonly IFavoriteAnimeRepository _favoriteAnimeRepository;
        private readonly IAnimeRepository _animeRepository; 

        public FavoriteAnimeService(IFavoriteAnimeRepository favoriteAnimeRepository, IAnimeRepository animeRepository)
        {
            _favoriteAnimeRepository = favoriteAnimeRepository;
            _animeRepository = animeRepository;
        }

        public IEnumerable<FavoriteAnimeViewModel> GetFavoriteAnimesByAnimeLoverId(int animeLoverId)
        {
            var favoriteAnimeDTOs = _favoriteAnimeRepository.GetFavoriteAnimesByAnimeLoverId(animeLoverId);
            Console.WriteLine($"Aantal opgehaalde favoriete anime DTO's: {favoriteAnimeDTOs.Count()}");

            var favoriteAnimes = favoriteAnimeDTOs.Select(AnimeRSConverter.ConvertToDomain);

            var animeIds = favoriteAnimes.Select(fa => fa.AnimeId).Distinct();
            Console.WriteLine($"Anime IDs: {string.Join(", ", animeIds)}");

            var animeDTOs = _animeRepository.GetAnimesByIds(animeIds); 
            Console.WriteLine($"Aantal opgehaalde anime DTO's: {animeDTOs.Count()}");

            var animeLookup = animeDTOs.ToDictionary(a => a.Id, a => a); 

            var favoriteAnimeViewModels = favoriteAnimes.Select(fa =>
                new FavoriteAnimeViewModel
                {
                    AnimeId = fa.AnimeId,
                    AnimeLoverId = fa.AnimeLoverId,
                    AnimeTitle = animeLookup.ContainsKey(fa.AnimeId) ? animeLookup[fa.AnimeId].Title : "Niet gevonden"
                });

            return favoriteAnimeViewModels;
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
