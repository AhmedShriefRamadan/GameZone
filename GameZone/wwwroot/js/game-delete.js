$(document).ready(function () {
    $(".delete-game").on('click', function () {
        var btn = $(this);

        $.ajax({
            url: `/Games/Delete/${btn.data('id')}`,
            method: 'DELETE',
            success: function () {
                alert('success');
            },
            error: function () {
                alert('error');
            }
        });
    });
});