//to check on Roles Page
function confirm(text, id, url)
{
    event.preventDefault();

    $.confirm({
        title: 'Confirm!',
        content: text,
        buttons: {
            confirm: function () {
                $.alert('Confirmed!');
                setTimeout(function(){location.href = url + id}, 1500)
            },
            cancel: function () {
                $.alert('Canceled!');
                event.preventDefault();
            },
        }
    });

}