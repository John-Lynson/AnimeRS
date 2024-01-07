using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AnimeRS.Core.Services;
using AnimeRS.Core.ViewModels;
using System.Security.Claims;
using AnimeRS.Core.Interfaces;

namespace AnimeRS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoriteAnimeController : ControllerBase
    {
        private readonly IFavoriteAnimeService _favoriteAnimeService;
        private readonly IAnimeLoverService _animeLoverService;

        public FavoriteAnimeController(IFavoriteAnimeService favoriteAnimeService, IAnimeLoverService animeLoverService)
        {
            _favoriteAnimeService = favoriteAnimeService;
            _animeLoverService = animeLoverService;
        }

        [HttpPost("toggle/{animeId}")]
        public IActionResult ToggleFavorite(int animeId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var animeLover = _animeLoverService.GetByAuth0UserId(userId);
            if (animeLover == null)
            {
                return NotFound("Gebruiker niet gevonden.");
            }

            try
            {
                // Wissel de favoriete status en haal de nieuwe status op
                _favoriteAnimeService.ToggleFavoriteAnime(animeLover.Id, animeId);
                var isNowFavorite = _favoriteAnimeService.GetFavoriteAnimesByAnimeLoverId(animeLover.Id)
                                       .Any(fa => fa.AnimeId == animeId);
                return Ok(new { isFavorite = isNowFavorite });
            }
            catch (Exception ex)
            {
                // Log de uitzondering
                return StatusCode(500, "Er is een fout opgetreden bij het wijzigen van de favoriete status.");
            }
        }
    }
}
