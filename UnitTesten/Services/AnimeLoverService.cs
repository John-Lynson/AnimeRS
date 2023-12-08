using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AnimeRS.Core.Models; // Vervang dit door de juiste namespace waar uw AnimeLover klasse zich bevindt
using AnimeRS.Core.Interfaces;

namespace UnitTesten.Services
{
    public class AnimeLoverServiceTests
    {
        [Fact]
        public void GetAllAnimeLovers_ShouldReturnAllAnimeLovers()
        {
            // Arrange
            var mockRepo = new Mock<IAnimeLoverRepository>();
            var animeLovers = new List<AnimeLover>
        {
            new AnimeLover("username1", "role1", "auth0UserId1"),
            new AnimeLover("username2", "role2", "auth0UserId2")
        };
            mockRepo.Setup(repo => repo.GetAllAnimeLovers()).Returns(animeLovers);
            var service = new AnimeLoverService(mockRepo.Object);

            // Act
            var result = service.GetAllAnimeLovers();

            // Assert
            Assert.Equal(animeLovers.Count, result.Count());
        }
    }
}