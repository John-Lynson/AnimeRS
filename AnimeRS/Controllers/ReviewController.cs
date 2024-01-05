using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AnimeRS.Core.Services;
using AnimeRS.Core.Models;
using System.Security.Claims;

namespace AnimeRS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;
        private readonly AnimeLoverService _animeLoverService;

        public ReviewController(ReviewService reviewService, AnimeLoverService animeLoverService)
        {
            _reviewService = reviewService;
            _animeLoverService = animeLoverService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateReview([FromBody] Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var auth0UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (auth0UserId == null)
            {
                return BadRequest("Auth0 gebruikers-ID niet gevonden.");
            }

            var animeLover = _animeLoverService.GetByAuth0UserId(auth0UserId);
            if (animeLover == null)
            {
                return BadRequest("Interne gebruikers-ID niet gevonden.");
            }

            review.AnimeLoverId = animeLover.Id; // Wijs de Id van AnimeLover toe aan AnimeLoverId van de review

            Console.WriteLine($"AnimeLoverId: {animeLover.Id}");

            _reviewService.AddReview(review);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized(); // Gebruiker is niet geauthenticeerd of de ID is niet gevonden
            }

            var review = _reviewService.GetReviewById(id);
            if (review != null && review.AnimeLoverId.ToString() == userIdClaim)
            {
                _reviewService.DeleteReview(id);
                // Redirect naar de juiste pagina of stuur een succesvolle response
                return Ok(); // Of een andere passende response
            }

            return Unauthorized(); // Of een andere passende response als de review niet bestaat of de gebruiker niet de eigenaar is
        }
    }
}
