document.addEventListener('DOMContentLoaded', () => {
    document.querySelectorAll('.favorite-star').forEach(star => {
        star.addEventListener('click', () => toggleFavorite(star.dataset.animeId));
    });
});

function toggleFavorite(animeId) {
    if (!animeId) {
        console.error("Anime ID is undefined");
        return;
    }

    $.ajax({
        url: `/api/favoriteanime/toggle/${animeId}`,
        method: 'POST',
        success: (response) => updateFavoriteStar(animeId, response.isFavorite),
        error: (error) => handleError(error)
    });
}

function updateFavoriteStar(animeId, isFavorite) {
    console.log("Updating star for Anime ID:", animeId, "Is Favorite:", isFavorite);
    var starElement = document.querySelector('.favorite-star[data-anime-id="' + animeId + '"]');
    if (starElement) {
        if (isFavorite) {
            starElement.innerHTML = '&#9733;'; // Gevulde ster
        } else {
            starElement.innerHTML = '&#9734;'; // Lege ster
        }
    }
}

function handleError(error) {
    console.error('Fout bij het wijzigen van favoriete status:', error);
}

