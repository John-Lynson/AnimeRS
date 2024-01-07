using Xunit;
using AnimeRS.Core.Models;
using AnimeRS.Data.dto;
using System;

namespace AnimeRS.UnitTesten
{
    public class AnimeRSConverterTests
    {
        [Fact]
        public void ConvertToDomain_ShouldConvertAnimeLoverDTOToAnimeLover()
        {
            // Arrange
            var animeLoverDTO = new AnimeLoverDTO
            {
                Id = 1,
                Username = "TestUser",
                Role = "UserRole",
                Auth0UserId = "Auth0Id"
            };

            // Act
            var result = AnimeRSConverter.ConvertToDomain(animeLoverDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(animeLoverDTO.Id, result.Id);
            Assert.Equal(animeLoverDTO.Username, result.Username);
            Assert.Equal(animeLoverDTO.Role, result.Role);
            Assert.Equal(animeLoverDTO.Auth0UserId, result.Auth0UserId);
        }

        [Fact]
        public void ConvertToDto_ShouldConvertAnimeLoverToAnimeLoverDTO()
        {
            // Arrange
            var animeLover = new AnimeLover
            {
                Id = 1,
                Username = "TestUser",
                Role = "UserRole",
                Auth0UserId = "Auth0Id"
            };

            // Act
            var result = AnimeRSConverter.ConvertToDto(animeLover);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(animeLover.Id, result.Id);
            Assert.Equal(animeLover.Username, result.Username);
            Assert.Equal(animeLover.Role, result.Role);
            Assert.Equal(animeLover.Auth0UserId, result.Auth0UserId);
        }

        [Fact]
        public void ConvertToDomain_ShouldConvertAnimeDTOToAnime()
        {
            var animeDTO = new AnimeDTO
            {
                Id = 1,
                Title = "Test Anime",
                Description = "Description",
                // Vul andere relevante velden in
            };

            var result = AnimeRSConverter.ConvertToDomain(animeDTO);

            Assert.NotNull(result);
            Assert.Equal(animeDTO.Id, result.Id);
            Assert.Equal(animeDTO.Title, result.Title);
            // Assert voor andere relevante velden
        }

        [Fact]
        public void ConvertToDto_ShouldConvertAnimeToAnimeDTO()
        {
            var anime = new Anime
            {
                Id = 1,
                Title = "Test Anime",
                Description = "Description",
                // Vul andere relevante velden in
            };

            var result = AnimeRSConverter.ConvertToDto(anime);

            Assert.NotNull(result);
            Assert.Equal(anime.Id, result.Id);
            Assert.Equal(anime.Title, result.Title);
            // Assert voor andere relevante velden
        }

        [Fact]
        public void ConvertToDomain_ShouldConvertFavoriteAnimeDTOToFavoriteAnime()
        {
            var favoriteAnimeDTO = new FavoriteAnimeDTO
            {
                AnimeLoverId = 1,
                AnimeId = 2
            };

            var result = AnimeRSConverter.ConvertToDomain(favoriteAnimeDTO);

            Assert.NotNull(result);
            Assert.Equal(favoriteAnimeDTO.AnimeLoverId, result.AnimeLoverId);
            Assert.Equal(favoriteAnimeDTO.AnimeId, result.AnimeId);
        }

        [Fact]
        public void ConvertToDto_ShouldConvertFavoriteAnimeToFavoriteAnimeDTO()
        {
            var favoriteAnime = new FavoriteAnime
            {
                AnimeLoverId = 1,
                AnimeId = 2
            };

            var result = AnimeRSConverter.ConvertToDto(favoriteAnime);

            Assert.NotNull(result);
            Assert.Equal(favoriteAnime.AnimeLoverId, result.AnimeLoverId);
            Assert.Equal(favoriteAnime.AnimeId, result.AnimeId);
        }

        [Fact]
        public void ConvertToDomain_ShouldConvertReviewDTOToReview()
        {
            var reviewDTO = new ReviewDTO
            {
                Id = 1,
                AnimeId = 2,
                AnimeLoverId = 3,
                Comment = "Great!",
                Rating = 5,
                DatePosted = DateTime.Now
            };

            var result = AnimeRSConverter.ConvertToDomain(reviewDTO);

            Assert.NotNull(result);
            Assert.Equal(reviewDTO.Id, result.Id);
            Assert.Equal(reviewDTO.AnimeId, result.AnimeId);
            Assert.Equal(reviewDTO.AnimeLoverId, result.AnimeLoverId);
            Assert.Equal(reviewDTO.Comment, result.Comment);
            Assert.Equal(reviewDTO.Rating, result.Rating);
            Assert.Equal(reviewDTO.DatePosted, result.DatePosted);
        }

        [Fact]
        public void ConvertToDto_ShouldConvertReviewToReviewDTO()
        {
            var review = new Review
            {
                Id = 1,
                AnimeId = 2,
                AnimeLoverId = 3,
                Comment = "Great!",
                Rating = 5,
                DatePosted = DateTime.Now
            };

            var result = AnimeRSConverter.ConvertToReviewDto(review);

            Assert.NotNull(result);
            Assert.Equal(review.Id, result.Id);
            Assert.Equal(review.AnimeId, result.AnimeId);
            Assert.Equal(review.AnimeLoverId, result.AnimeLoverId);
            Assert.Equal(review.Comment, result.Comment);
            Assert.Equal(review.Rating, result.Rating);
            Assert.Equal(review.DatePosted, result.DatePosted);
        }
    }
}
