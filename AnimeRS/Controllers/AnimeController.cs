using Microsoft.AspNetCore.Mvc;
using AnimeRS.Core.Interfaces;
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

    [HttpPost]
    public IActionResult CreateAnime([FromBody] Anime anime)
    {
        _animeRepository.AddAnime(anime);
        return Ok(anime);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAnime(int id, [FromBody] Anime anime)
    {
        var existingAnime = _animeRepository.GetAnimeById(id);
        if (existingAnime == null)
        {
            return NotFound();
        }

        _animeRepository.UpdateAnime(anime);
        return Ok(anime);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAnime(int id)
    {
        var anime = _animeRepository.GetAnimeById(id);
        if (anime == null)
        {
            return NotFound();
        }

        _animeRepository.DeleteAnime(id);
        return Ok();
    }



}
