using Microsoft.AspNetCore.Mvc;
using AnimeRS.Core.Services;
using AnimeRS.Core.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class AnimeController : ControllerBase
{
    private readonly AnimeService _animeService;
    private readonly FavoriteAnimeService _favoriteAnimeService;

    public AnimeController(AnimeService animeService)
    {
        _animeService = animeService;
    }

    [HttpGet]
    public IActionResult GetAllAnimes()
    {
        var animes = _animeService.GetAllAnimes();
        return Ok(animes);
    }

    [HttpGet("{id}")]
    public IActionResult GetAnimeById(int id)
    {
        var anime = _animeService.GetAnimeById(id);
        if (anime == null)
        {
            return NotFound();
        }
        return Ok(anime);
    }

    [HttpPost]
    public IActionResult CreateAnime([FromBody] Anime anime)
    {
        _animeService.AddAnime(anime);
        return Ok(anime); // Of return CreatedAtAction voor de locatie van de nieuwe resource
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAnime(int id, [FromBody] Anime anime)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingAnime = _animeService.GetAnimeById(id);
        if (existingAnime == null)
        {
            return NotFound();
        }

        _animeService.UpdateAnime(anime);
        return Ok(anime);
    }

    [HttpGet("search")]
    public IActionResult SearchAnimes(string query)
    {
        var animes = _animeService.SearchAnimes(query);
        return Ok(animes);
    }



    [HttpDelete("{id}")]
    public IActionResult DeleteAnime(int id)
    {
        var anime = _animeService.GetAnimeById(id);
        if (anime == null)
        {
            return NotFound();
        }

        _animeService.DeleteAnime(id);
        return Ok();
    }
}
