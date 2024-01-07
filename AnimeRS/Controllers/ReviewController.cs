using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AnimeRS.Core.Services;
using AnimeRS.Core.Models;
using System.Security.Claims;
using AnimeRS.Core.Interfaces;

namespace AnimeRS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IAnimeLoverService _animeLoverService;

        public ReviewController(IReviewService reviewService, IAnimeLoverService animeLoverService)
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
            Console.WriteLine($"Delete methode aangeroepen voor review ID: {id}");

            var auth0UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine($"Gebruikers-ID claim: {auth0UserId}");

            if (auth0UserId == null)
            {
                return Unauthorized(); // Gebruiker is niet geauthenticeerd of de ID is niet gevonden
            }

            var review = _reviewService.GetReviewById(id);
            if (review == null)
            {
                return NotFound(); // Review niet gevonden
            }

            var animeLover = _animeLoverService.GetByAuth0UserId(auth0UserId);
            if (animeLover == null)
            {
                return Unauthorized(); // Gebruiker is niet gevonden in het systeem
            }

            if (review.AnimeLoverId != animeLover.Id)
            {
                return Unauthorized(); // Gebruiker is niet gemachtigd om deze review te verwijderen
            }

            _reviewService.DeleteReview(id);
            return Ok(); // Succesvolle response
        }
    }
}
