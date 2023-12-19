using AnimeRS.Core.Models;
using AnimeRS.Data.dto;

public static class AnimeRSConverter
{
    public static AnimeLoverDTO ConvertToDto(AnimeLover animeLover)
    {
        return new AnimeLoverDTO
        {
            Id = animeLover.Id,
            Username = animeLover.Username,
            Role = animeLover.Role
            // Voeg andere relevante velden toe
        };
    }

    public static AnimeLover ConvertToDomain(AnimeLoverDTO animeLoverDTO)
    {
        return new AnimeLover
        {
            // Aannemende dat AnimeLover een constructor heeft die deze velden accepteert
            Username = animeLoverDTO.Username,
            Role = animeLoverDTO.Role,
            // Voeg andere velden toe indien nodig
        };
    }

    public static AnimeDTO ConvertToAnimeDto(Anime anime)
    {
        if (anime == null) return null;

        return new AnimeDTO
        {
            Id = anime.Id,
            Title = anime.Title,
            Description = anime.Description,
            Genre = anime.Genre,
            Episodes = anime.Episodes,
            Status = anime.Status,
            ReleaseDate = anime.ReleaseDate
            // Voeg andere relevante velden toe
        };
    }

    public static AnimeLoverDTO ConvertToAnimeLoverDto(AnimeLover animeLover)
    {
        if (animeLover == null) return null;

        return new AnimeLoverDTO
        {
            Id = animeLover.Id,
            Username = animeLover.Username,
            Role = animeLover.Role,
            Auth0UserId = animeLover.Auth0UserId
            // Voeg andere relevante velden toe
        };
    }

    public static AnimeLover ConvertToAnimeLover(AnimeLoverDTO animeLoverDTO)
    {
        if (animeLoverDTO == null) return null;

        return new AnimeLover
        {
            Id = animeLoverDTO.Id,
            Username = animeLoverDTO.Username,
            Role = animeLoverDTO.Role,
            Auth0UserId = animeLoverDTO.Auth0UserId
            // Voeg andere velden toe indien nodig
        };
    }

    public static Anime ConvertToAnime(AnimeDTO animeDTO)
    {
        if (animeDTO == null) return null;

        return new Anime
        {
            Title = animeDTO.Title,
            Description = animeDTO.Description,
            Genre = animeDTO.Genre,
            Episodes = animeDTO.Episodes,
            Status = animeDTO.Status,
            ReleaseDate = animeDTO.ReleaseDate
            // Voeg andere velden toe indien nodig
        };
    }

    public static FavoriteAnimeDTO ConvertToFavoriteAnimeDto(FavoriteAnime favoriteAnime)
    {
        if (favoriteAnime == null) return null;

        return new FavoriteAnimeDTO
        {
            AnimeLoverId = favoriteAnime.AnimeLoverId,
            AnimeId = favoriteAnime.AnimeId
            // Voeg andere relevante velden toe
        };
    }

    public static FavoriteAnime ConvertToFavoriteAnime(FavoriteAnimeDTO favoriteAnimeDTO)
    {
        if (favoriteAnimeDTO == null) return null;

        return new FavoriteAnime
        {
            AnimeLoverId = favoriteAnimeDTO.AnimeLoverId,
            AnimeId = favoriteAnimeDTO.AnimeId
            // Voeg andere velden toe indien nodig
        };
    }

    public static ReviewDTO ConvertToReviewDto(Review review)
    {
        if (review == null) return null;

        return new ReviewDTO
        {
            Id = review.Id,
            AnimeId = review.AnimeId,
            AnimeLoverId = review.AnimeLoverId,
            Comment = review.Comment,
            Rating = review.Rating,
            DatePosted = review.DatePosted
            // Voeg andere relevante velden toe
        };
    }

    public static Review ConvertToReview(ReviewDTO reviewDTO)
    {
        if (reviewDTO == null) return null;

        return new Review
        {
            AnimeId = reviewDTO.AnimeId,
            AnimeLoverId = reviewDTO.AnimeLoverId,
            Comment = reviewDTO.Comment,
            Rating = reviewDTO.Rating,
            DatePosted = reviewDTO.DatePosted
            // Voeg andere velden toe indien nodig
        };
    }
}
