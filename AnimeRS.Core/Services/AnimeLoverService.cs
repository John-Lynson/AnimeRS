using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeRS.Data.Interfaces;
using AnimeRS.Core.Models;

public class AnimeLoverService
{
    private readonly IAnimeLoverRepository _animeLoverRepository;

    public AnimeLoverService(IAnimeLoverRepository animeLoverRepository)
    {
        _animeLoverRepository = animeLoverRepository;
    }

    // Methoden voor het ophalen, toevoegen, bijwerken en verwijderen van anime liefhebbers
}