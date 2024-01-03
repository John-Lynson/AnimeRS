using AnimeRS.Core.Models;
using AnimeRS.Data.dto;
using System.Collections.Generic;
using System.Linq;

public static class AnimeRSConverter
{
    // AnimeLover Conversies
    public static AnimeLover ConvertToDomain(AnimeLoverDTO dto)
    {
        return dto == null ? null : new AnimeLover
        {
            Id = dto.Id,
            Username = dto.Username,
            Role = dto.Role,
            Auth0UserId = dto.Auth0UserId
            // Voeg andere relevante velden toe
        };
    }

    public static AnimeLoverDTO ConvertToDto(AnimeLover domain)
    {
        return domain == null ? null : new AnimeLoverDTO
        {
            Id = domain.Id,
            Username = domain.Username,
            Role = domain.Role,
            Auth0UserId = domain.Auth0UserId
            // Voeg andere relevante velden toe
        };
    }

    // Anime Conversies
    public static Anime ConvertToDomain(AnimeDTO dto)
    {
        return dto == null ? null : new Anime
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            Genre = dto.Genre,
            Episodes = dto.Episodes,
            Status = dto.Status,
            ReleaseDate = dto.ReleaseDate,
            ImageURL = dto.ImageURL
            // Voeg andere relevante velden toe
        };
    }

    public static AnimeDTO ConvertToDto(Anime domain)
    {
        return domain == null ? null : new AnimeDTO
        {
            Id = domain.Id,
            Title = domain.Title,
            Description = domain.Description,
            Genre = domain.Genre,
            Episodes = domain.Episodes,
            Status = domain.Status,
            ReleaseDate = domain.ReleaseDate,
            ImageURL = domain.ImageURL
            // Voeg andere relevante velden toe
        };
    }

    // FavoriteAnime Conversies
    public static FavoriteAnime ConvertToDomain(FavoriteAnimeDTO dto)
    {
        return dto == null ? null : new FavoriteAnime
        {
            AnimeLoverId = dto.AnimeLoverId,
            AnimeId = dto.AnimeId
            // Voeg andere relevante velden toe
        };
    }

    public static FavoriteAnimeDTO ConvertToDto(FavoriteAnime domain)
    {
        return domain == null ? null : new FavoriteAnimeDTO
        {
            AnimeLoverId = domain.AnimeLoverId,
            AnimeId = domain.AnimeId
            // Voeg andere relevante velden toe
        };
    }

    // Review Conversies
    public static Review ConvertToDomain(ReviewDTO dto)
    {
        return dto == null ? null : new Review
        {
            Id = dto.Id,
            AnimeId = dto.AnimeId,
            AnimeLoverId = dto.AnimeLoverId,
            Comment = dto.Comment,
            Rating = dto.Rating,
            DatePosted = dto.DatePosted
            // Voeg andere relevante velden toe
        };
    }

    public static Anime ConvertToAutocompleteDomain(AnimeDTO dto)
    {
        return new Anime
        {
            Id = dto.Id,
            Title = dto.Title
        };
    }

    public static ReviewDTO ConvertToDto(Review domain)
    {
        return domain == null ? null : new ReviewDTO
        {
            Id = domain.Id,
            AnimeId = domain.AnimeId,
            AnimeLoverId = domain.AnimeLoverId,
            Comment = domain.Comment,
            Rating = domain.Rating,
            DatePosted = domain.DatePosted
            // Voeg andere relevante velden toe
        };
    }
}
