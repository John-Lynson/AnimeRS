$(document).ready(function () {
    loadAllAnimes();

    // Gecombineerde zoekfunctionaliteit
    $('#searchQuery').on('keyup', debounce(function () {
        var query = $('#searchQuery').val();
        if (query) {
            searchAnimes(query);
        } else {
            loadAllAnimes(); // Laad alle animes als de zoekbalk leeg is
        }
    }, 500)); // 500 ms debounce tijd

    $('#searchButton').click(function () {
        var query = $('#searchQuery').val();
        if (query) {
            searchAnimes(query);
        } else {
            loadAllAnimes(); // Laad alle animes als de zoekbalk leeg is
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

function searchAnimes(query) {
    $.ajax({
        url: '/api/anime/search?query=' + encodeURIComponent(query),
        method: 'GET',
        success: function (animes) {
            if (animes.length > 0) {
                displayAnimes(animes);
            }
            // Als er geen resultaten zijn, wordt de lijst niet bijgewerkt
        },
        error: function (xhr, status, error) {
            console.error('Fout bij het zoeken van animes:', error);
        }
    });
}

function displayAnimes(animes) {
    var listContainer = $('#animeList');
    listContainer.empty();

    var groupedAnimes = groupAnimesByFirstLetter(animes);

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

function groupAnimesByFirstLetter(animes) {
    return animes.reduce((acc, anime) => {
        let firstLetter = anime.title.charAt(0).toUpperCase();
        if (!acc[firstLetter]) {
            acc[firstLetter] = [];
        }
        acc[firstLetter].push(anime);
        return acc;
    }, {});
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
