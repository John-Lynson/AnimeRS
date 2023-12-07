$(document).ready(function () {
    loadAnimes();

    $('#animeForm').submit(function (e) {
        e.preventDefault();
        submitAnimeForm();
    });
});

function loadAnimes() {
    $.ajax({
        url: '/api/anime',
        method: 'GET',
        success: function (data) {
            var animeTable = $('#animeTable tbody');
            animeTable.empty();
            data.forEach(function (anime) {
                animeTable.append('<tr>' +
                    '<td>' + anime.title + '</td>' +
                    '<td>' + anime.description + '</td>' +
                    '<td>' + anime.genre + '</td>' +
                    '<td>' + anime.episodes + '</td>' +
                    '<td>' + anime.status + '</td>' +
                    '<td>' + new Date(anime.releaseDate).toLocaleDateString() + '</td>' +
                    '<td><button onclick="openModal(' + anime.id + ')">Bewerken</button>' +
                    '<button onclick="deleteAnime(' + anime.id + ')">Verwijderen</button></td>' +
                    '</tr>');
            });
        }
    });
}

function openModal(animeId) {
    if (animeId) {
        $.ajax({
            url: '/api/anime/' + animeId,
            method: 'GET',
            success: function (anime) {
                $('#animeId').val(anime.id);
                $('#animeTitle').val(anime.title);
                $('#animeDescription').val(anime.description);
                $('#animeGenre').val(anime.genre);
                $('#animeEpisodes').val(anime.episodes);
                $('#animeStatus').val(anime.status);
                $('#animeReleaseDate').val(new Date(anime.releaseDate).toISOString().split('T')[0]);
            }
        });
    } else {
        $('#animeForm')[0].reset();
        $('#animeId').val('');
    }
    $('#animeModal').show(); // Zorg ervoor dat je een modal hebt of pas dit aan
}

function submitAnimeForm() {
    var animeId = $('#animeId').val();
    var animeData = {
        title: $('#animeTitle').val(),
        description: $('#animeDescription').val(),
        genre: $('#animeGenre').val(),
        episodes: parseInt($('#animeEpisodes').val(), 10),
        status: $('#animeStatus').val(),
        releaseDate: $('#animeReleaseDate').val()
    };

    // Voeg de id alleen toe bij bewerken
    if (animeId) {
        animeData.id = animeId;
    }

    var url = animeId ? '/api/anime/' + animeId : '/api/anime';
    var method = animeId ? 'PUT' : 'POST';

    // Deactiveer de knop om dubbele inzendingen te voorkomen
    $('#animeFormButton').prop('disabled', true);

    $.ajax({
        url: url,
        method: method,
        contentType: 'application/json',
        data: JSON.stringify(animeData),
        success: function () {
            $('#animeModal').hide();
            loadAnimes();
        },
        error: function (xhr, status, error) {
            alert("Er is een fout opgetreden: " + error);
        },
        complete: function () {
            // Activeer de knop opnieuw na voltooiing van de aanvraag
            $('#animeFormButton').prop('disabled', false);
        }
    });
}

function deleteAnime(animeId) {
    if (confirm('Weet je zeker dat je deze anime wilt verwijderen?')) {
        $.ajax({
            url: '/api/anime/' + animeId,
            method: 'DELETE',
            success: function () {
                loadAnimes();
            },
            error: function (xhr, status, error) {
                alert("Er is een fout opgetreden: " + error);
            }
        });
    }
}
