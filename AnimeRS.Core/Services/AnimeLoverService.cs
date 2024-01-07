using AnimeRS.Data.Interfaces;
using AnimeRS.Data.dto;
using AnimeRS.Core.Models;
using System.Collections.Generic;
using System.Linq;
using AnimeRS.Core.Interfaces;

public class AnimeLoverService : IAnimeLoverService
{
    private readonly IAnimeLoverRepository _animeLoverRepository;

    public AnimeLoverService(IAnimeLoverRepository animeLoverRepository)
    {
        _animeLoverRepository = animeLoverRepository;
    }

    public IEnumerable<AnimeLover> GetAllAnimeLovers()
    {
        var animeLoverDTOs = _animeLoverRepository.GetAllAnimeLovers();
        return animeLoverDTOs.Select(AnimeRSConverter.ConvertToDomain).ToList();
    }

    public AnimeLover GetAnimeLoverById(int id)
    {
        var animeLoverDTO = _animeLoverRepository.GetAnimeLoverById(id);
        return AnimeRSConverter.ConvertToDomain(animeLoverDTO);
    }

    public AnimeLover GetAnimeLoverByUsername(string username)
    {
        var animeLoverDTO = _animeLoverRepository.GetAnimeLoverByUsername(username);
        return AnimeRSConverter.ConvertToDomain(animeLoverDTO);
    }

    public AnimeLover GetByAuth0UserId(string auth0UserId)
    {
        var animeLoverDTO = _animeLoverRepository.GetByAuth0UserId(auth0UserId);
        return AnimeRSConverter.ConvertToDomain(animeLoverDTO);
    }

    public void AddAnimeLover(AnimeLover animeLover)
    {
        var animeLoverDTO = AnimeRSConverter.ConvertToDto(animeLover);
        _animeLoverRepository.AddAnimeLover(animeLoverDTO);
    }

    public void UpdateAnimeLover(AnimeLover animeLover)
    {
        var animeLoverDTO = AnimeRSConverter.ConvertToDto(animeLover);
        _animeLoverRepository.UpdateAnimeLover(animeLoverDTO);
    }

    public void DeleteAnimeLover(int id)
    {
        _animeLoverRepository.DeleteAnimeLover(id);
    }
}
