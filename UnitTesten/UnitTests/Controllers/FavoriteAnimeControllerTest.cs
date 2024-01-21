using Xunit;
using Moq;
using AnimeRS.Core.Services;
using AnimeRS.Data.Interfaces;
using AnimeRS.Core.Models;
using AnimeRS.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using System.Collections.Generic;
using AnimeRS.Core.Interfaces;
using AnimeRS.Core.ViewModels;

namespace AnimeRS.UnitTesten.Controllers
{
    public class FavoriteAnimeControllerTests
    {
        private readonly Mock<IFavoriteAnimeService> mockFavoriteAnimeService;
        private readonly Mock<IAnimeLoverService> mockAnimeLoverService;
        private readonly FavoriteAnimeController controller;
        private readonly Mock<ClaimsPrincipal> mockUser;

        public FavoriteAnimeControllerTests()
        {
            mockFavoriteAnimeService = new Mock<IFavoriteAnimeService>();
            mockAnimeLoverService = new Mock<IAnimeLoverService>();
            controller = new FavoriteAnimeController(mockFavoriteAnimeService.Object, mockAnimeLoverService.Object);
            mockUser = new Mock<ClaimsPrincipal>();

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, "MockUserId") };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = principal }
            };
        }

        [Fact]
        public void ToggleFavorite_ReturnsOk_WhenToggledSuccessfully()
        {
            // Arrange
            int animeId = 1;
            var dummyAnimeLover = new AnimeLover { Id = 1, Auth0UserId = "MockUserId" };
            mockAnimeLoverService.Setup(service => service.GetByAuth0UserId("MockUserId")).Returns(dummyAnimeLover);

            // Act
            var result = controller.ToggleFavorite(animeId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
