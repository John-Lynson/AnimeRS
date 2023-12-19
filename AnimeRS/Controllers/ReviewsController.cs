using AnimeRS.Data.Repositories;
using AnimeRS.Data.Interfaces;
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