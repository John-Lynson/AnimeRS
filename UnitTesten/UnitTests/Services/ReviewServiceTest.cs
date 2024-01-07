using Xunit;
using Moq;
using AnimeRS.Core.Services;
using AnimeRS.Core.Models;
using AnimeRS.Data.Interfaces;
using AnimeRS.Data.dto;
using System.Collections.Generic;
using System.Linq;

namespace AnimeRS.UnitTesten.Services
{
    public class ReviewServiceTests
    {
        private readonly Mock<IReviewRepository> mockReviewRepository;
        private readonly Mock<AnimeService> mockAnimeService;

        private readonly ReviewService reviewService;

        public ReviewServiceTests()
        {
            mockReviewRepository = new Mock<IReviewRepository>();
            mockAnimeService = new Mock<AnimeService>();

            reviewService = new ReviewService(mockReviewRepository.Object, mockAnimeService.Object);
        }

        [Fact]
        public void GetAllReviews_ShouldReturnAllReviews()
        {
            // Arrange
            var dummyReviews = new List<ReviewDTO> { /* Voeg dummy review data toe */ };
            mockReviewRepository.Setup(repo => repo.GetAllReviews()).Returns(dummyReviews);

            // Act
            var result = reviewService.GetAllReviews();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dummyReviews.Count, result.Count());
        }

        [Fact]
        public void GetReviewById_ShouldReturnReview_WhenReviewExists()
        {
            // Arrange
            var reviewId = 1;
            var dummyReview = new ReviewDTO { Id = reviewId };
            mockReviewRepository.Setup(repo => repo.GetReviewById(reviewId)).Returns(dummyReview);

            // Act
            var result = reviewService.GetReviewById(reviewId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(reviewId, result.Id);
        }

        [Fact]
        public void AddReview_ShouldInvokeAddReview_WhenCalled()
        {
            // Arrange
            var review = new Review { /* initialiseer Review object */ };

            // Act
            reviewService.AddReview(review);

            // Assert
            mockReviewRepository.Verify(repo => repo.AddReview(It.IsAny<ReviewDTO>()), Times.Once);
        }

        [Fact]
        public void UpdateReview_ShouldInvokeUpdateReview_WhenCalled()
        {
            // Arrange
            var review = new Review { /* initialiseer Review object */ };

            // Act
            reviewService.UpdateReview(review);

            // Assert
            mockReviewRepository.Verify(repo => repo.UpdateReview(It.IsAny<ReviewDTO>()), Times.Once);
        }

        [Fact]
        public void GetReviewsByAnimeId_ShouldReturnReviewsForAnime_WhenReviewsExist()
        {
            // Arrange
            var animeId = 1;
            var dummyReviews = new List<ReviewDTO> { /* Voeg dummy review data toe voor deze animeId */ };
            mockReviewRepository.Setup(repo => repo.GetReviewsByAnimeId(animeId)).Returns(dummyReviews);

            // Act
            var result = reviewService.GetReviewsByAnimeId(animeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dummyReviews.Count, result.Count());
        }

        [Fact]
        public void GetAverageScore_ShouldReturnAverageScore_WhenReviewsExist()
        {
            // Arrange
            var animeId = 1;
            var dummyReviews = new List<ReviewDTO> { /* Voeg dummy review data toe met ratings */ };
            mockReviewRepository.Setup(repo => repo.GetReviewsByAnimeId(animeId)).Returns(dummyReviews);

            // Act
            var averageScore = reviewService.GetAverageScore(animeId);

            // Bereken verwachte gemiddelde score
            var expectedAverage = dummyReviews.Any() ? dummyReviews.Average(r => r.Rating) : 0;

            // Assert
            Assert.Equal(expectedAverage, averageScore);
        }

        [Fact]
        public void GetReviewsByUserId_ShouldReturnReviewsForUser_WhenReviewsExist()
        {
            // Arrange
            var userId = 1;
            var dummyReviews = new List<ReviewDTO> { /* Voeg dummy review data toe voor deze userId */ };
            mockReviewRepository.Setup(repo => repo.GetReviewsByAnimeLoverId(userId)).Returns(dummyReviews);

            // Act
            var result = reviewService.GetReviewsByUserId(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dummyReviews.Count, result.Count());
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
    }
}
