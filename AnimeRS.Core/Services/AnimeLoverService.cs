using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeRS.Core.Interfaces;
using AnimeRS.Core.Models;

public class AnimeLoverService
{
    private readonly IAnimeLoverRepository _animeLoverRepository;

    public AnimeLoverService(IAnimeLoverRepository animeLoverRepository)
    {
        _animeLoverRepository = animeLoverRepository;
    }

    public IEnumerable<AnimeLover> GetAllAnimeLovers()
    {
        return _animeLoverRepository.GetAllAnimeLovers();
    }

    public AnimeLover GetAnimeLoverById(int id)
    {
        return _animeLoverRepository.GetAnimeLoverById(id);
    }

    public AnimeLover GetByAuth0UserId(string auth0UserId)
    {
        return _animeLoverRepository.GetByAuth0UserId(auth0UserId);
    }

    public void Create(AnimeLover animeLover)
    {
        _animeLoverRepository.AddAnimeLover(animeLover);
    }

    // Voeg hier andere methoden toe zoals Create, Update, Delete, etc.
}
