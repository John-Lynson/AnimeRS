using AnimeRS.Data.Repositories;
using AnimeRS.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnimeRS.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        // ... actiemethoden voor het plaatsen, bekijken, bewerken en verwijderen van reviews
    }
}