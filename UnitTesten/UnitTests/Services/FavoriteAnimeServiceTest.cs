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
    public class FavoriteAnimeServiceTests
    {
        private readonly Mock<IFavoriteAnimeRepository> mockFavoriteAnimeRepository;
        private readonly Mock<IAnimeRepository> mockAnimeRepository;
        private readonly FavoriteAnimeService favoriteAnimeService;

        public FavoriteAnimeServiceTests()
        {
            mockFavoriteAnimeRepository = new Mock<IFavoriteAnimeRepository>();
            mockAnimeRepository = new Mock<IAnimeRepository>();
            favoriteAnimeService = new FavoriteAnimeService(mockFavoriteAnimeRepository.Object, mockAnimeRepository.Object);

            // Mock data setup kan hier
        }

        [Fact]
        public void GetFavoriteAnimesByAnimeLoverId_ShouldReturnFavoriteAnimes()
        {
            // Arrange
            int animeLoverId = 1;
            var dummyFavoriteAnimeDTOs = new List<FavoriteAnimeDTO>
    {
        new FavoriteAnimeDTO { AnimeLoverId = animeLoverId, AnimeId = 100 },
        new FavoriteAnimeDTO { AnimeLoverId = animeLoverId, AnimeId = 101 }
    };
            var dummyAnimeDTOs = new List<AnimeDTO>
    {
        new AnimeDTO { Id = 100, Title = "Anime1" },
        new AnimeDTO { Id = 101, Title = "Anime2" }
    };
            mockFavoriteAnimeRepository.Setup(repo => repo.GetFavoriteAnimesByAnimeLoverId(animeLoverId)).Returns(dummyFavoriteAnimeDTOs);
            mockAnimeRepository.Setup(repo => repo.GetAnimesByIds(It.IsAny<IEnumerable<int>>())).Returns(dummyAnimeDTOs);

            // Act
            var result = favoriteAnimeService.GetFavoriteAnimesByAnimeLoverId(animeLoverId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void AddFavoriteAnime_ShouldAddFavoriteAnime()
        {
            // Arrange
            int animeLoverId = 1;
            int animeId = 100;
            mockFavoriteAnimeRepository.Setup(repo => repo.AddFavoriteAnime(It.IsAny<FavoriteAnimeDTO>())).Returns(true);

            // Act
            var result = favoriteAnimeService.AddFavoriteAnime(animeLoverId, animeId);

            // Assert
            Assert.True(result);
            mockFavoriteAnimeRepository.Verify(repo => repo.AddFavoriteAnime(It.Is<FavoriteAnimeDTO>(fa => fa.AnimeLoverId == animeLoverId && fa.AnimeId == animeId)), Times.Once);
        }

        [Fact]
        public void RemoveFavoriteAnime_ShouldRemoveFavoriteAnime()
        {
            // Arrange
            int animeLoverId = 1;
            int animeId = 100;
            var dummyFavoriteAnimeDTO = new FavoriteAnimeDTO { AnimeLoverId = animeLoverId, AnimeId = animeId };
            mockFavoriteAnimeRepository.Setup(repo => repo.GetFavoriteAnimesByAnimeLoverId(animeLoverId)).Returns(new List<FavoriteAnimeDTO> { dummyFavoriteAnimeDTO });
            mockFavoriteAnimeRepository.Setup(repo => repo.RemoveFavoriteAnime(It.IsAny<int>())).Returns(true);

            // Act
            var result = favoriteAnimeService.RemoveFavoriteAnime(animeLoverId, animeId);

            // Assert
            Assert.True(result);
            mockFavoriteAnimeRepository.Verify(repo => repo.RemoveFavoriteAnime(animeLoverId), Times.Once);
        }

        [Fact]
        public void ToggleFavoriteAnime_ShouldAddFavoriteAnime_WhenNotFavoriteYet()
        {
            // Arrange
            int animeLoverId = 1;
            int animeId = 100;
            var existingFavoriteAnimes = new List<FavoriteAnimeDTO>(); // Geen favoriete animes voor deze gebruiker
            mockFavoriteAnimeRepository.Setup(repo => repo.GetFavoriteAnimesByAnimeLoverId(animeLoverId)).Returns(existingFavoriteAnimes);
            mockFavoriteAnimeRepository.Setup(repo => repo.AddFavoriteAnime(It.IsAny<FavoriteAnimeDTO>())).Returns(true);

            // Act
            favoriteAnimeService.ToggleFavoriteAnime(animeLoverId, animeId);

            // Assert
            mockFavoriteAnimeRepository.Verify(repo => repo.AddFavoriteAnime(It.Is<FavoriteAnimeDTO>(fa => fa.AnimeLoverId == animeLoverId && fa.AnimeId == animeId)), Times.Once);
            mockFavoriteAnimeRepository.Verify(repo => repo.RemoveFavoriteAnime(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void ToggleFavoriteAnime_ShouldRemoveFavoriteAnime_WhenAlreadyFavorite()
        {
            // Arrange
            int animeLoverId = 1;
            int animeId = 100;
            var existingFavoriteAnimes = new List<FavoriteAnimeDTO> { new FavoriteAnimeDTO { AnimeLoverId = animeLoverId, AnimeId = animeId } };
            mockFavoriteAnimeRepository.Setup(repo => repo.GetFavoriteAnimesByAnimeLoverId(animeLoverId)).Returns(existingFavoriteAnimes);
            mockFavoriteAnimeRepository.Setup(repo => repo.RemoveFavoriteAnime(It.IsAny<int>())).Returns(true);

            // Act
            favoriteAnimeService.ToggleFavoriteAnime(animeLoverId, animeId);

            // Assert
            mockFavoriteAnimeRepository.Verify(repo => repo.AddFavoriteAnime(It.IsAny<FavoriteAnimeDTO>()), Times.Never);
            mockFavoriteAnimeRepository.Verify(repo => repo.RemoveFavoriteAnime(animeLoverId), Times.Once);
        }
    }
}
