$(document).ready(function () {
    // Handler voor het indienen van een nieuwe review
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

    // Functie om de bewerkingsmodus voor een review in te schakelen
    window.editReview = function (reviewId) {
        var reviewText = document.querySelector(`.review-text[data-review-id="${reviewId}"]`);
        var editReviewText = document.querySelector(`.edit-review-text[data-review-id="${reviewId}"]`);
        var editButton = document.querySelector(`.edit-review-btn[data-review-id="${reviewId}"]`);

        if (editButton) {
            reviewText.style.display = 'none';
            editReviewText.style.display = 'block';
            editReviewText.removeAttribute('hidden');

            editButton.textContent = 'Opslaan';
            editButton.onclick = function () { saveReview(reviewId); };
        } else {
            console.error('Bewerkingsknop niet gevonden voor review ID:', reviewId);
        }
    };

    // Functie om een bewerkte review op te slaan
    window.saveReview = function (reviewId) {
        var editReviewTextElement = document.querySelector(`.edit-review-text[data-review-id="${reviewId}"]`);
        var editReviewText = editReviewTextElement.value;

        $.ajax({
            url: `/api/review/edit/${reviewId}`,
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(editReviewText),
            success: function () {
                var reviewTextElement = document.querySelector(`.review-text[data-review-id="${reviewId}"]`);
                reviewTextElement.textContent = editReviewText;

                editReviewTextElement.style.display = 'none';
                reviewTextElement.style.display = 'block';

                var editButton = document.querySelector(`.edit-review-btn[data-review-id="${reviewId}"]`);
                editButton.textContent = 'Bewerken';
                editButton.onclick = function () { editReview(reviewId); };
            },
            error: function (error) {
                console.error('Fout bij het bijwerken van review:', error);
            }
        });
    };

    // Functie om een review te verwijderen
    window.deleteReview = function (reviewId) {
        if (!confirm('Weet u zeker dat u deze review wilt verwijderen?')) {
            return;
        }

        $.ajax({
            url: `/api/review/${reviewId}`,
            method: 'DELETE',
            success: function () {
                // Verwijder de review uit de DOM
                $(`.review-item[data-review-id="${reviewId}"]`).remove();
                alert("Review succesvol verwijderd.");
            },
            error: function (error) {
                console.error('Fout bij het verwijderen van review:', error);
                alert("Er is een fout opgetreden bij het verwijderen van de review.");
            }
        });
    };
});
