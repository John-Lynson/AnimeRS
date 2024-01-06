document.addEventListener('DOMContentLoaded', (event) => {
    document.querySelectorAll('.favorite-star').forEach(star => {
        star.addEventListener('click', function () {
            toggleFavorite(this.dataset.animeId);
        });
    });
});

function toggleFavorite(animeId) {
    console.log("Anime ID:", animeId); // Log om te controleren
    if (animeId === undefined) {
        console.error("Anime ID is undefined");
        return;
    }
    $.ajax({
        url: '/api/favoriteanime/toggle/' + animeId,
        method: 'POST',
        success: function (response) {
            console.log("Response received:", response);
            updateFavoriteStar(animeId, response.isFavorite);
        },


        error: function (error) {
            console.error('Fout bij het wijzigen van favoriete status:', error);
        }
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
