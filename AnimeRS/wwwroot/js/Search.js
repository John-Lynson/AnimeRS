document.getElementById('searchButton').addEventListener('click', function () {
    var name = document.getElementById('searchName').value;
    var genre = document.getElementById('searchGenre').value;

    var xhr = new XMLHttpRequest();
    xhr.open('GET', '/api/anime/search?name=' + encodeURIComponent(name) + '&genre=' + encodeURIComponent(genre), true);
    xhr.onload = function () {
        if (this.status == 200) {
            var animes = JSON.parse(this.responseText);
            displayResults(animes);
        } else {
            console.error('Fout bij het ophalen van data:', this.statusText);
        }
    };
    xhr.send();
});

function displayResults(animes) {
    var resultsContainer = document.getElementById('searchResults');
    resultsContainer.innerHTML = '';

    animes.forEach(function (anime) {
        var div = document.createElement('div');
        div.className = 'search-item';
        div.innerHTML = '<h3>' + anime.title + '</h3><p>' + anime.description + '</p>';
        resultsContainer.appendChild(div);
    });
}
