using Xunit;
using Moq;
using AnimeRS.Core.Interfaces;
using AnimeRS.Core.Models;
using AnimeRS.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Collections.Generic;
using AnimeRS.Core.Services;

namespace AnimeRS.UnitTesten.Controllers
{
    public class ReviewControllerTests
    {
        private readonly Mock<IReviewService> mockReviewService;
        private readonly Mock<IAnimeLoverService> mockAnimeLoverService;
        private readonly ReviewController controller;
        private readonly Mock<ClaimsPrincipal> mockUser;

        public ReviewControllerTests()
        {
            mockReviewService = new Mock<IReviewService>();
            mockAnimeLoverService = new Mock<IAnimeLoverService>();
            controller = new ReviewController(mockReviewService.Object, mockAnimeLoverService.Object);
            mockUser = new Mock<ClaimsPrincipal>();

            // Setup de mock gebruiker en claims
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, "MockUserId") };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = principal }
            };
        }

        [Fact]
        public void CreateReview_ReturnsBadRequest_WhenModelInvalid()
        {
            // Arrange
            var review = new Review { /* Eigenschappen */ };
            controller.ModelState.AddModelError("Error", "Modelstate error");

            // Act
            var result = controller.CreateReview(review);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void CreateReview_ReturnsOk_WhenReviewCreated()
        {
            // Arrange
            var review = new Review { /* Eigenschappen */ };
            var mockAnimeLover = new AnimeLover { Id = 1, Auth0UserId = "MockUserId" };
            mockAnimeLoverService.Setup(service => service.GetByAuth0UserId("MockUserId")).Returns(mockAnimeLover);
            mockReviewService.Setup(service => service.AddReview(It.IsAny<Review>()));

            // Act
            var result = controller.CreateReview(review);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void EditReview_ReturnsNotFound_WhenReviewDoesNotExist()
        {
            // Arrange
            int reviewId = 1;
            mockReviewService.Setup(service => service.GetReviewById(reviewId)).Returns((Review)null);

            // Act
            var result = controller.EditReview(reviewId, "New Comment");

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
