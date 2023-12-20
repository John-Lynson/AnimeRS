using Microsoft.AspNetCore.Mvc;
using AnimeRS.Core.Services;
using AnimeRS.Data.dto;
using AnimeRS.Core.Models;

[Route("api/[controller]")]
[ApiController]
public class AnimeController : ControllerBase
{
    private readonly AnimeService _animeService;

    public AnimeController(AnimeService animeService)
    {
        _animeService = animeService;
    }

    [HttpGet]
    public IActionResult GetAllAnimes()
    {
        var animeDTOs = _animeService.GetAllAnimes();
        var animes = animeDTOs.Select(AnimeRSConverter.ConvertToDomain).ToList();
        return Ok(animes);
    }

    [HttpGet("{id}")]
    public IActionResult GetAnimeById(int id)
    {
        var animeDTO = _animeService.GetAnimeById(id);
        if (animeDTO == null)
        {
            return NotFound();
        }
        var anime = AnimeRSConverter.ConvertToDomain(animeDTO);
        return Ok(anime);
    }

    [HttpPost]
    public IActionResult CreateAnime([FromBody] Anime anime)
    {
        var animeDTO = AnimeRSConverter.ConvertToDto(anime);
        _animeService.AddAnime(animeDTO);
        return Ok(anime); // Of return CreatedAtAction als je de locatie van de nieuwe resource wilt meegeven
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAnime(int id, [FromBody] Anime anime)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingAnimeDTO = _animeService.GetAnimeById(id);
        if (existingAnimeDTO == null)
        {
            return NotFound();
        }

        var updatedAnimeDTO = AnimeRSConverter.ConvertToDto(anime);
        _animeService.UpdateAnime(updatedAnimeDTO);
        return Ok(anime);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAnime(int id)
    {
        var animeDTO = _animeService.GetAnimeById(id);
        if (animeDTO == null)
        {
            return NotFound();
        }

        _animeService.DeleteAnime(id);
        return Ok();
    }
}
