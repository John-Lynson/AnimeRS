using Microsoft.AspNetCore.Mvc;
using AnimeRS.Data.Interfaces;
using AnimeRS.Core.Models;

[Route("api/[controller]")]
[ApiController]
public class AnimeController : ControllerBase
{
    private readonly IAnimeRepository _animeRepository;

    public AnimeController(IAnimeRepository animeRepository)
    {
        _animeRepository = animeRepository;
    }

    [HttpGet]
    public IActionResult GetAllAnimes()
    {
        var animes = _animeRepository.GetAllAnimes();
        return Ok(animes);
    }

    [HttpGet("{id}")]
    public IActionResult GetAnimeById(int id)
    {
        var anime = _animeRepository.GetAnimeById(id);
        if (anime == null)
        {
            return NotFound();
        }
        return Ok(anime);
    }


    [HttpPost]
    public IActionResult CreateAnime([FromBody] Anime anime)
    {
        _animeRepository.AddAnime(anime);
        return Ok(anime);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAnime(int id, [FromBody] Anime anime)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != anime.Id)
        {
            return BadRequest("ID mismatch");
        }

        var existingAnime = _animeRepository.GetAnimeById(id);
        if (existingAnime == null)
        {
            return NotFound();
        }

        try
        {
            _animeRepository.UpdateAnime(anime);
            return Ok(anime);
        }
        catch (Exception ex)
        {
            // Log de uitzondering
            return StatusCode(500, "Internal server error");
        }
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteAnime(int id)
    {
        var anime = _animeRepository.GetAnimeById(id);
        if (anime == null)
        {
            return NotFound();
        }

        try
        {
            _animeRepository.DeleteAnime(id);
            return Ok();
        }
        catch (Exception ex)
        {
            // Log de uitzondering
            return StatusCode(500, "Internal server error");
        }
    }
}
