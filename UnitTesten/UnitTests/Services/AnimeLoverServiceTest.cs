using Xunit;
using Moq;
using AnimeRS.Core.Models;
using AnimeRS.Data.Interfaces;
using AnimeRS.Data.dto;
using System;
using System.Collections.Generic;
using AnimeRS.Core.Services;
using System.Linq;

namespace AnimeRS.UnitTesten.Services
{
    public class AnimeLoverServiceTests
    {
        private readonly Mock<IAnimeLoverRepository> mockAnimeLoverRepository;
        private readonly AnimeLoverService animeLoverService;

        public AnimeLoverServiceTests()
        {
            mockAnimeLoverRepository = new Mock<IAnimeLoverRepository>();
            animeLoverService = new AnimeLoverService(mockAnimeLoverRepository.Object);

            // Hier kan je de mock setup plaatsen
        }

        [Fact]
        public void GetAllAnimeLovers_ShouldReturnAllAnimeLovers()
        {
            // Arrange
            // In AnimeLoverServiceTests constructor
            var dummyAnimeLovers = new List<AnimeLoverDTO>
              {
                new AnimeLoverDTO { Id = 1, Username = "Lover1", Auth0UserId = "auth0Id1", Role = "Role1" },
                 new AnimeLoverDTO { Id = 2, Username = "Lover2", Auth0UserId = "auth0Id2", Role = "Role2" }
               };
            mockAnimeLoverRepository.Setup(repo => repo.GetAllAnimeLovers()).Returns(dummyAnimeLovers);

            // Act
            var result = animeLoverService.GetAllAnimeLovers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dummyAnimeLovers.Count, result.Count());
        }

        [Fact]
        public void GetAnimeLoverById_ShouldReturnAnimeLover_WhenAnimeLoverExists()
        {
            // Arrange
            var animeLoverId = 1;
            var dummyAnimeLover = new AnimeLoverDTO { Id = 1, Username = "Lover1", Auth0UserId = "auth0Id1", Role = "Role1" };
            mockAnimeLoverRepository.Setup(repo => repo.GetAnimeLoverById(1)).Returns(dummyAnimeLover);


            // Act
            var result = animeLoverService.GetAnimeLoverById(animeLoverId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(animeLoverId, result.Id);
        }

        [Fact]
        public void GetAnimeLoverByUsername_ShouldReturnAnimeLover_WhenUsernameExists()
        {
            // Arrange
            var username = "Lover1";
            var dummyAnimeLover = new AnimeLoverDTO { Id = 1, Username = username, Auth0UserId = "auth0Id1", Role = "User" };
            mockAnimeLoverRepository.Setup(repo => repo.GetAnimeLoverByUsername(username)).Returns(dummyAnimeLover);

            // Act
            var result = animeLoverService.GetAnimeLoverByUsername(username);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(username, result.Username);
        }

        [Fact]
        public void GetByAuth0UserId_ShouldReturnAnimeLover_WhenAuth0UserIdExists()
        {
            // Arrange
            var auth0UserId = "auth0Id1";
            var dummyAnimeLoverDTO = new AnimeLoverDTO
            {
                Id = 1,
                Username = "Lover1",
                Role = "User",
                Auth0UserId = auth0UserId
            };
            mockAnimeLoverRepository.Setup(repo => repo.GetByAuth0UserId(auth0UserId)).Returns(dummyAnimeLoverDTO);


            // Act
            var result = animeLoverService.GetByAuth0UserId(auth0UserId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(auth0UserId, result.Auth0UserId);
        }

        [Fact]
        public void AddAnimeLover_ShouldInvokeAddAnimeLover_WhenCalled()
        {
            // Arrange
            var animeLover = new AnimeLover {};
            var animeLoverDTO = AnimeRSConverter.ConvertToDto(animeLover);
            mockAnimeLoverRepository.Setup(repo => repo.AddAnimeLover(It.IsAny<AnimeLoverDTO>())).Returns(true);

            // Act
            animeLoverService.AddAnimeLover(animeLover);

            // Assert
            mockAnimeLoverRepository.Verify(repo => repo.AddAnimeLover(It.Is<AnimeLoverDTO>(dto => dto.Username == animeLoverDTO.Username)), Times.Once);
        }

        [Fact]
        public void UpdateAnimeLover_ShouldInvokeUpdateAnimeLover_WhenCalled()
        {
            // Arrange
            var animeLover = new AnimeLover
            {
                Id = 1,
                Username = "Gebruiker1",
                // Vul andere relevante velden in...
            };
            var animeLoverDTO = AnimeRSConverter.ConvertToDto(animeLover);
            mockAnimeLoverRepository.Setup(repo => repo.UpdateAnimeLover(It.IsAny<AnimeLoverDTO>())).Returns(true);

            // Act
            animeLoverService.UpdateAnimeLover(animeLover);

            // Assert
            mockAnimeLoverRepository.Verify(repo => repo.UpdateAnimeLover(It.Is<AnimeLoverDTO>(dto => dto.Id == animeLoverDTO.Id && dto.Username == animeLoverDTO.Username)), Times.Once);
        }

        [Fact]
        public void DeleteAnimeLover_ShouldInvokeDeleteAnimeLover_WhenCalled()
        {
            // Arrange
            var animeLoverId = 1;
            mockAnimeLoverRepository.Setup(repo => repo.DeleteAnimeLover(animeLoverId)).Returns(true);

            // Act
            animeLoverService.DeleteAnimeLover(animeLoverId);

            // Assert
            mockAnimeLoverRepository.Verify(repo => repo.DeleteAnimeLover(animeLoverId), Times.Once);
        }
    }
}
