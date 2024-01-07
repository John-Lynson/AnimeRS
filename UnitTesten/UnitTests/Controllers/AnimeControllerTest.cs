using Xunit;
using Moq;
using AnimeRS.Core.Interfaces;
using AnimeRS.Core.Models;
using Microsoft.AspNetCore.Mvc;
using AnimeRS.Web.Controllers;
using System.Collections.Generic;

namespace AnimeRS.UnitTesten.Controllers
{
    public class AnimeControllerTests
    {
        private readonly Mock<IAnimeService> mockAnimeService;
        private readonly AnimeController controller;

        public AnimeControllerTests()
        {
            mockAnimeService = new Mock<IAnimeService>();
            controller = new AnimeController(mockAnimeService.Object);
        }

        [Fact]
        public void GetAllAnimes_ReturnsAllAnimes()
        {
            // Arrange
            var dummyAnimes = new List<Anime>
            {
                new Anime { Id = 1, Title = "Naruto" },
                new Anime { Id = 2, Title = "Dragon Ball Z" }
            };
            mockAnimeService.Setup(service => service.GetAllAnimes()).Returns(dummyAnimes);

            // Act
            var result = controller.GetAllAnimes();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(dummyAnimes, okResult.Value);
        }

        [Fact]
        public void GetAnimeById_ReturnsNotFound_WhenAnimeDoesNotExist()
        {
            // Arrange
            mockAnimeService.Setup(service => service.GetAnimeById(It.IsAny<int>())).Returns((Anime)null);

            // Act
            var result = controller.GetAnimeById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetAnimeById_ReturnsAnime_WhenAnimeExists()
        {
            // Arrange
            var dummyAnime = new Anime { Id = 1, Title = "Naruto" };
            mockAnimeService.Setup(service => service.GetAnimeById(1)).Returns(dummyAnime);

            // Act
            var result = controller.GetAnimeById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(dummyAnime, okResult.Value);
        }

        [Fact]
        public void CreateAnime_ReturnsCreatedAnime()
        {
            // Arrange
            var animeToCreate = new Anime { Title = "One Piece" };
            mockAnimeService.Setup(service => service.AddAnime(animeToCreate));

            // Act
            var result = controller.CreateAnime(animeToCreate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(animeToCreate, okResult.Value);
        }

        [Fact]
        public void UpdateAnime_ReturnsNotFound_WhenAnimeDoesNotExist()
        {
            // Arrange
            mockAnimeService.Setup(service => service.GetAnimeById(It.IsAny<int>())).Returns((Anime)null);

            // Act
            var result = controller.UpdateAnime(1, new Anime());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void SearchAnimes_ReturnsFilteredAnimes()
        {
            // Arrange
            var dummyAnimes = new List<Anime>
            {
                new Anime { Id = 1, Title = "One Punch Man" }
            };
            mockAnimeService.Setup(service => service.SearchAnimes("One")).Returns(dummyAnimes);

            // Act
            var result = controller.SearchAnimes("One");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(dummyAnimes, okResult.Value);
        }

        [Fact]
        public void DeleteAnime_ReturnsNotFound_WhenAnimeDoesNotExist()
        {
            // Arrange
            mockAnimeService.Setup(service => service.GetAnimeById(It.IsAny<int>())).Returns((Anime)null);

            // Act
            var result = controller.DeleteAnime(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteAnime_ReturnsOk_WhenAnimeExists()
        {
            // Arrange
            var dummyAnime = new Anime { Id = 1, Title = "My Hero Academia" };
            mockAnimeService.Setup(service => service.GetAnimeById(1)).Returns(dummyAnime);
            mockAnimeService.Setup(service => service.DeleteAnime(1));

            // Act
            var result = controller.DeleteAnime(1);

            // Assert
            Assert.IsType<OkResult>(result);
            mockAnimeService.Verify(service => service.DeleteAnime(1), Times.Once);
        }
    }
}
