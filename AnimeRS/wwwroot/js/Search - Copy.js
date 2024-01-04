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
        var title = $('#searchTitle').val();
        var genre = $('#searchGenre').val();
        searchAnimes(title, genre);
    });

    // Autocomplete zoekfunctionaliteit
    $('#searchBar').on('keyup', function () {
        var query = $(this).val();
        if (query.length > 1) {
            $.ajax({
                url: '/api/anime/autocomplete?query=' + encodeURIComponent(query),
                method: 'GET',
                success: function (data) {
                    var suggestions = $('#suggestions');
                    suggestions.empty();
                    data.forEach(function (anime) {
                        suggestions.append('<li>' + anime.title + '</li>');
                    });
                    suggestions.show();
                },
                error: function (error) {
                    console.error('Fout bij het zoeken:', error);
                }
            });
        } else {
            $('#suggestions').hide();
        }
    });

    // Verberg de suggesties wanneer er ergens anders wordt geklikt
    $(document).on('click', function (event) {
        if (!$(event.target).closest('#searchBar').length) {
            $('#suggestions').hide();
        }
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

    // Groepeer animes op basis van de eerste letter van hun titel
    var groupedAnimes = animes.reduce((acc, anime) => {
        let firstLetter = anime.title.charAt(0).toUpperCase();
        if (!acc[firstLetter]) {
            acc[firstLetter] = [];
        }
        acc[firstLetter].push(anime);
        return acc;
    }, {});

    // Sorteer en toon de gegroepeerde animes
    Object.keys(groupedAnimes).sort().forEach(letter => {
        var section = $('<section>', { class: 'anime-letter-section' });
        section.append($('<h4>').text(letter));
        var ul = $('<ul>');

        groupedAnimes[letter].forEach(anime => {
            var listItem = $('<li>');
            listItem.html('<a href="/anime/' + anime.id + '">' + anime.title + ' (' + new Date(anime.releaseDate).toLocaleDateString() + ')</a>');
            ul.append(listItem);
        });

        section.append(ul);
        listContainer.append(section);
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
