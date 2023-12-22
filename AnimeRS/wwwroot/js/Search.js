$(document).ready(function () {
    loadAllAnimes();

    // Live zoeken implementeren
    $('#searchTitle, #searchGenre').on('keyup', debounce(function () {
        var title = $('#searchTitle').val();
        var genre = $('#searchGenre').val();
        searchAnimes(title, genre);
    }, 500)); // 500 ms debounce tijd

    // Oorspronkelijke zoekknop functionaliteit
    $('#searchButton').click(function () {
        var title = $('#searchTitle').val(); // Zorg dat dit overeenkomt met het ID van je invoerveld
        var genre = $('#searchGenre').val();
        searchAnimes(title, genre);
    });
});

function loadAllAnimes() {
    $.ajax({
        url: '/api/anime',
        method: 'GET',
        success: function (animes) {
            displayAnimes(animes);
        },
        error: function (xhr, status, error) {
            console.error('Fout bij het laden van animes:', error);
        }
    });
}

function searchAnimes(title, genre) {
    $.ajax({
        url: '/api/anime/search?title=' + encodeURIComponent(title) + '&genre=' + encodeURIComponent(genre),
        method: 'GET',
        success: function (animes) {
            displayAnimes(animes);
        },
        error: function (xhr, status, error) {
            console.error('Fout bij het zoeken van animes:', error);
        }
    });
}


function displayAnimes(animes) {
    var listContainer = $('#animeList');
    listContainer.empty();

    animes.sort((a, b) => a.title.localeCompare(b.title)).forEach(function (anime) {
        var listItem = $('<li>');
        listItem.html('<a href="/anime/' + anime.id + '">' + anime.title + ' (' + new Date(anime.releaseDate).toLocaleDateString() + ')</a>');
        listContainer.append(listItem);
    });
}

function debounce(func, delay) {
    var inDebounce;
    return function () {
        var context = this;
        var args = arguments;
        clearTimeout(inDebounce);
        inDebounce = setTimeout(function () {
            func.apply(context, args);
        }, delay);
    };
}
