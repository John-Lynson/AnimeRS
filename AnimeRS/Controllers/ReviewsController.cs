﻿using AnimeRS.Core.Services;
using Microsoft.AspNetCore.Mvc;
using AnimeRS.Core.Models;

namespace AnimeRS.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public IActionResult Index()
        {
            var reviewDTOs = _reviewService.GetAllReviews();
            var reviews = reviewDTOs.Select(AnimeRSConverter.ConvertToDomain).ToList();
            return View(reviews);
        }

        // ... andere actiemethoden voor het plaatsen, bekijken, bewerken en verwijderen van reviews
    }
}
