using AnimeRS.Data.Interfaces;
using AnimeRS.Data.dto;
using System.Collections.Generic;

public class AnimeLoverService
{
    private readonly IAnimeLoverRepository _animeLoverRepository;

    public AnimeLoverService(IAnimeLoverRepository animeLoverRepository)
    {
        _animeLoverRepository = animeLoverRepository;
    }

    public IEnumerable<AnimeLoverDTO> GetAllAnimeLovers()
    {
        return _animeLoverRepository.GetAllAnimeLovers();
    }

    public AnimeLoverDTO GetAnimeLoverById(int id)
    {
        return _animeLoverRepository.GetAnimeLoverById(id);
    }

    public AnimeLoverDTO GetAnimeLoverByUsername(string username)
    {
        return _animeLoverRepository.GetAnimeLoverByUsername(username);
    }

    public AnimeLoverDTO GetByAuth0UserId(string auth0UserId)
    {
        return _animeLoverRepository.GetByAuth0UserId(auth0UserId);
    }

    public void AddAnimeLover(AnimeLoverDTO animeLoverDTO)
    {
        _animeLoverRepository.AddAnimeLover(animeLoverDTO);
    }

    public void UpdateAnimeLover(AnimeLoverDTO animeLoverDTO)
    {
        _animeLoverRepository.UpdateAnimeLover(animeLoverDTO);
    }

    public void DeleteAnimeLover(int id)
    {
        _animeLoverRepository.DeleteAnimeLover(id);
    }
}
