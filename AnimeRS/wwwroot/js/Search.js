$(document).ready(function () {
    loadAllAnimes();

    $('#searchButton').click(function () {
        var name = $('#searchName').val();
        var genre = $('#searchGenre').val();
        searchAnimes(name, genre);
    });
});

function loadAllAnimes() {
    $.ajax({
        url: '/api/anime',
        method: 'GET',
        success: function (animes) {
            displayAnimes(animes, true);
        },
        error: function (xhr, status, error) {
            console.error('Fout bij het laden van animes:', error);
        }
    });
}

function searchAnimes(name, genre) {
    $.ajax({
        url: '/api/anime/search?name=' + encodeURIComponent(name) + '&genre=' + encodeURIComponent(genre),
        method: 'GET',
        success: function (animes) {
            displayAnimes(animes, false);
        },
        error: function (xhr, status, error) {
            console.error('Fout bij het zoeken van animes:', error);
        }
    });
}

function displayAnimes(animes, isInitialLoad) {
    var listContainer = $('#animeList');
    if (isInitialLoad) {
        listContainer.empty();
    }

    animes.sort((a, b) => a.title.localeCompare(b.title)).forEach(function (anime) {
        var listItem = $('<li>');
        listItem.html('<a href="/anime/' + anime.id + '">' + anime.title + ' (' + new Date(anime.releaseDate).toLocaleDateString() + ')</a>');
        listContainer.append(listItem);
    });
}
