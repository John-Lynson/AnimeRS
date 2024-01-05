$(document).ready(function () {
    $('#reviewForm').submit(function (event) {
        event.preventDefault();

        var reviewData = {
            AnimeId: $('#animeId').val(),
            Comment: $('#reviewComment').val(),
            Rating: parseInt($('#reviewRating').val(), 10)
        };

        $.ajax({
            url: '/api/review',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(reviewData),
            success: function () {
                alert("Review succesvol geplaatst.");
                // Voeg hier code toe om de pagina bij te werken
            },
            error: function (error) {
                console.error('Fout bij het plaatsen van review:', error);
                alert("Er is een fout opgetreden bij het plaatsen van de review.");
            }
        });
    });
});
