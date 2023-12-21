document.addEventListener('DOMContentLoaded', function () {
    loadAllAnimes();
});

document.getElementById('searchButton').addEventListener('click', function () {
    var name = document.getElementById('searchName').value;
    var genre = document.getElementById('searchGenre').value;
    searchAnimes(name, genre);
});

function loadAllAnimes() {
    fetch('/api/anime')
        .then(response => response.json())
        .then(animes => displayAnimes(animes, true))
        .catch(error => console.error('Fout bij het laden van animes:', error));
}

function searchAnimes(name, genre) {
    fetch('/api/anime/search?name=' + encodeURIComponent(name) + '&genre=' + encodeURIComponent(genre))
        .then(response => response.json())
        .then(animes => displayAnimes(animes, false))
        .catch(error => console.error('Fout bij het zoeken van animes:', error));
}

function displayAnimes(animes, isInitialLoad) {
    var listContainer = document.getElementById('animeList');
    if (isInitialLoad) {
        listContainer.innerHTML = '';
    }

    animes.sort((a, b) => a.title.localeCompare(b.title)).forEach(function (anime) {
        var li = document.createElement('li');
        li.innerHTML = '<a href="/anime/' + anime.id + '">' + anime.title + ' (' + new Date(anime.releaseDate).toLocaleDateString() + ')</a>';
        listContainer.appendChild(li);
    });
}
