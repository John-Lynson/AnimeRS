using Xunit;
using Moq;
using AnimeRS.Core.Services;
using AnimeRS.Core.Models;
using AnimeRS.Data.Interfaces;
using AnimeRS.Data.dto;
using System.Collections.Generic;

namespace AnimeRS.UnitTesten.Services
{
    public class AnimeServiceTests
    {
        private readonly Mock<IAnimeRepository> mockAnimeRepository;
        private readonly Mock<IReviewRepository> mockReviewRepository;
        private readonly AnimeService animeService;

        public AnimeServiceTests()
        {
            mockAnimeRepository = new Mock<IAnimeRepository>();
            mockReviewRepository = new Mock<IReviewRepository>();

            var dummyReviews = new List<ReviewDTO> {};
            mockReviewRepository.Setup(repo => repo.GetAllReviews()).Returns(dummyReviews);

            var dummyAnimes = new List<AnimeDTO> {};
            mockAnimeRepository.Setup(repo => repo.GetAllAnimes()).Returns(dummyAnimes);

            // Creëer een instance van AnimeService met de mocks
            animeService = new AnimeService(mockAnimeRepository.Object, mockReviewRepository.Object);
        }

        [Fact]
        public void AddAnime_ShouldCallAddAnime_WhenCalled()
        {
            // Arrange
            var anime = new Anime {};

            // Act
            animeService.AddAnime(anime);

            // Assert
            mockAnimeRepository.Verify(repo => repo.AddAnime(It.IsAny<AnimeDTO>()), Times.Once);
        }

        [Fact]
        public void GetAnimeById_ShouldReturnAnime_WhenAnimeExists()
        {
            // Arrange
            var animeId = 1;
            var dummyAnimeDTO = new AnimeDTO { Id = animeId };
            mockAnimeRepository.Setup(repo => repo.GetAnimeById(animeId)).Returns(dummyAnimeDTO);

            // Act
            var result = animeService.GetAnimeById(animeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(animeId, result.Id);
        }

        [Fact]
        public void UpdateAnime_ShouldCallUpdateAnime_WhenCalled()
        {
            // Arrange
            var anime = new Anime {};

            // Act
            animeService.UpdateAnime(anime);

            // Assert
            mockAnimeRepository.Verify(repo => repo.UpdateAnime(It.IsAny<AnimeDTO>()), Times.Once);
        }

        [Fact]
        public void SearchAnimes_ShouldReturnAnimes_WhenQueryMatches()
        {
            var query = "some query";
            var matchingAnimes = new List<AnimeDTO>
    {
        new AnimeDTO { Id = 1, Title = "Anime Title 1", Genre = "Genre", /* ... andere eigenschappen ... */ },
        new AnimeDTO { Id = 2, Title = "Another Anime Title", Genre = "Different Genre", /* ... andere eigenschappen ... */ }

    };
            mockAnimeRepository.Setup(repo => repo.SearchAnimes(query)).Returns(matchingAnimes);

            var result = animeService.SearchAnimes(query);

            Assert.NotNull(result);
            Assert.Equal(matchingAnimes.Count, result.Count());
        }


        [Fact]
        public void GetTopAnimesByTotalRating_ShouldReturnTopRatedAnimes()
        {
            // Arrange
            var topCount = 3;

            // Dummy data voor reviews
            var dummyReviews = new List<ReviewDTO>
    {
        new ReviewDTO { AnimeId = 1, Rating = 5 },
        new ReviewDTO { AnimeId = 2, Rating = 4 },
        new ReviewDTO { AnimeId = 1, Rating = 4 },
        new ReviewDTO { AnimeId = 3, Rating = 5 },
        new ReviewDTO { AnimeId = 2, Rating = 5 },
        // Voeg meer dummy reviews toe indien nodig
    };
            mockReviewRepository.Setup(repo => repo.GetAllReviews()).Returns(dummyReviews);

            // Dummy data voor animes
            var dummyAnimes = new List<AnimeDTO>
    {
        new AnimeDTO { Id = 1, /* andere eigenschappen */ },
        new AnimeDTO { Id = 2, /* andere eigenschappen */ },
        new AnimeDTO { Id = 3, /* andere eigenschappen */ }
        
    };
            mockAnimeRepository.Setup(repo => repo.GetAnimeById(It.IsAny<int>())).Returns<int>(id => dummyAnimes.FirstOrDefault(a => a.Id == id));

            // Act
            var result = animeService.GetTopAnimesByTotalRating(topCount);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(topCount, result.Count()); 
                             
        }


        [Fact]
        public void DeleteAnime_ShouldCallDeleteAnime_WhenCalled()
        {
            // Arrange
            var animeId = 1;

            // Act
            animeService.DeleteAnime(animeId);

            // Assert
            mockAnimeRepository.Verify(repo => repo.DeleteAnime(animeId), Times.Once);
        }



        [Fact]
        public void GetAllAnimes_ShouldReturnAllAnimes_WhenAnimesExist()
        {
            // Act
            var result = animeService.GetAllAnimes();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockAnimeRepository.Object.GetAllAnimes().Count(), result.Count());
        }
    }
}
