using Microsoft.AspNetCore.Mvc;
using AnimeRS.Core.Services;
using AnimeRS.Data.dto;

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
    public IActionResult CreateAnime([FromBody] AnimeDTO animeDTO)
    {
        _animeService.AddAnime(animeDTO);
        return Ok(animeDTO);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAnime(int id, [FromBody] AnimeDTO animeDTO)
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

        _animeService.UpdateAnime(animeDTO);
        return Ok(animeDTO);
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
