using Xunit;
using Moq;
using AnimeRS.Core.Services;
using AnimeRS.Core.Models;
using AnimeRS.Data.Interfaces;
using AnimeRS.Data.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using AnimeRS.Core.Interfaces;

namespace AnimeRS.UnitTesten.Services
{
    public class ReviewServiceTests
    {
        private readonly Mock<IReviewRepository> mockReviewRepository;
        private readonly Mock<IAnimeService> mockAnimeService;
        private readonly ReviewService reviewService;

        public ReviewServiceTests()
        {
            mockReviewRepository = new Mock<IReviewRepository>();
            mockAnimeService = new Mock<IAnimeService>();

            // Dummy data voor ReviewDTO
            var dummyReviews = new List<ReviewDTO>
            {
                new ReviewDTO
                {
                    Id = 1,
                    AnimeId = 101,
                    AnimeLoverId = 1001,
                    Comment = "Geweldige anime, aanrader!",
                    Rating = 5,
                    DatePosted = DateTime.UtcNow.AddDays(-10)
                },
                new ReviewDTO
                {
                    Id = 2,
                    AnimeId = 102,
                    AnimeLoverId = 1002,
                    Comment = "Niet slecht, maar kon beter.",
                    Rating = 3,
                    DatePosted = DateTime.UtcNow.AddDays(-5)
                }
                // Voeg meer reviews toe naar wens
            };

            mockReviewRepository.Setup(repo => repo.GetAllReviews()).Returns(dummyReviews);
            reviewService = new ReviewService(mockReviewRepository.Object, mockAnimeService.Object);
        }

        [Fact]
        public void GetAllReviews_ShouldReturnAllReviews()
        {
            // Act
            var result = reviewService.GetAllReviews();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count()); // Aangepast naar het aantal dummy reviews
        }

        [Fact]
        public void GetReviewById_ShouldReturnReview_WhenReviewExists()
        {
            // Arrange
            var reviewId = 1;
            var dummyReview = new ReviewDTO { Id = reviewId, AnimeId = 101, Comment = "Leuke anime", Rating = 4 };
            mockReviewRepository.Setup(repo => repo.GetReviewById(reviewId)).Returns(dummyReview);

            // Act
            var result = reviewService.GetReviewById(reviewId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(reviewId, result.Id);
        }

        [Fact]
        public void UpdateReview_ShouldInvokeUpdateReview_WhenCalled()
        {
            // Arrange
            var review = new Review { Id = 1, AnimeId = 101, Comment = "Bijgewerkte review", Rating = 5 };
            var reviewDTO = AnimeRSConverter.ConvertToReviewDto(review);
            mockReviewRepository.Setup(repo => repo.UpdateReview(It.IsAny<ReviewDTO>()));

            // Act
            reviewService.UpdateReview(review);

            // Assert
            mockReviewRepository.Verify(repo => repo.UpdateReview(It.Is<ReviewDTO>(dto =>
                dto.Id == reviewDTO.Id &&
                dto.Comment == reviewDTO.Comment)), Times.Once);
        }

        [Fact]
        public void GetReviewsByAnimeId_ShouldReturnReviewsForAnime_WhenReviewsExist()
        {
            // Arrange
            var animeId = 101;
            var dummyReviews = new List<ReviewDTO> { new ReviewDTO { AnimeId = animeId, Rating = 4 } };
            mockReviewRepository.Setup(repo => repo.GetReviewsByAnimeId(animeId)).Returns(dummyReviews);

            // Act
            var result = reviewService.GetReviewsByAnimeId(animeId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public void GetAverageScore_ShouldReturnAverageScore_WhenReviewsExist()
        {
            // Arrange
            var animeId = 101;
            var dummyReviews = new List<ReviewDTO>
    {
        new ReviewDTO { AnimeId = animeId, Rating = 3 },
        new ReviewDTO { AnimeId = animeId, Rating = 5 }
    };
            mockReviewRepository.Setup(repo => repo.GetReviewsByAnimeId(animeId)).Returns(dummyReviews);

            // Act
            var averageScore = reviewService.GetAverageScore(animeId);

            // Assert
            var expectedAverage = dummyReviews.Any() ? dummyReviews.Average(r => r.Rating) * 2 : 0;
            Assert.Equal(expectedAverage, averageScore);
        }

        [Fact]
        public void GetReviewsByUserId_ShouldReturnReviewsForUser_WhenReviewsExist()
        {
            // Arrange
            var userId = 1001;
            var dummyReviews = new List<ReviewDTO> { new ReviewDTO { AnimeLoverId = userId, AnimeId = 101, Rating = 4 } };
            var dummyAnime = new Anime { Id = 101, Title = "Anime Title" };
            mockReviewRepository.Setup(repo => repo.GetReviewsByAnimeLoverId(userId)).Returns(dummyReviews);
            mockAnimeService.Setup(service => service.GetAnimeById(101)).Returns(dummyAnime);

            // Act
            var result = reviewService.GetReviewsByUserId(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public void DeleteReview_ShouldInvokeDeleteReview_WhenCalled()
        {
            // Arrange
            var reviewId = 1;

            // Act
            reviewService.DeleteReview(reviewId);

            // Assert
            mockReviewRepository.Verify(repo => repo.DeleteReview(reviewId), Times.Once);
        }


        [Fact]
        public void AddReview_ShouldInvokeAddReview_WhenCalled()
        {
            // Arrange
            var review = new Review
            {
                AnimeId = 103,
                Comment = "Nieuwe review toevoegen.",
                Rating = 4
                // DatePosted zal intern worden ingesteld
            };
            var reviewDTO = new ReviewDTO
            {
                AnimeId = review.AnimeId,
                Comment = review.Comment,
                Rating = review.Rating,
                DatePosted = review.DatePosted
            };
            mockReviewRepository.Setup(repo => repo.AddReview(It.IsAny<ReviewDTO>()));

            // Act
            reviewService.AddReview(review);

            // Assert
            mockReviewRepository.Verify(repo => repo.AddReview(It.Is<ReviewDTO>(dto =>
                dto.AnimeId == reviewDTO.AnimeId &&
                dto.Comment == reviewDTO.Comment &&
                dto.Rating == reviewDTO.Rating)), Times.Once);
        }

        // Voeg hier extra tests toe voor andere methoden van ReviewService
    }
}
