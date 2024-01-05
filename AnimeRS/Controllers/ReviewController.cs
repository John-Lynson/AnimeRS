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

        [HttpPost("edit/{id}")]
        [Authorize]
        public IActionResult EditReview(int id, [FromBody] string newComment)
        {
            Console.WriteLine($"EditReview aangeroepen met id: {id} en newComment: {newComment}");

            if (string.IsNullOrWhiteSpace(newComment))
            {
                return BadRequest("Commentaar mag niet leeg zijn.");
            }

            var review = _reviewService.GetReviewById(id);
            if (review == null)
            {
                return NotFound("Review niet gevonden.");
            }

            // Log de huidige staat van de review en de nieuwe commentaar
            Console.WriteLine($"Review ID: {review.Id}, Huidige Commentaar: {review.Comment}, Nieuwe Commentaar: {newComment}");

            review.Comment = newComment;

            // Log de review details voordat deze wordt bijgewerkt
            Console.WriteLine($"Bijwerken review: {review.Id}, Nieuwe Commentaar: {review.Comment}");

            _reviewService.UpdateReview(review);

            return Ok("Review succesvol bijgewerkt.");
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
