$(function () {
    $.ajax({
        type: 'GET',
        url: 'https://api.twitch.tv/kraken/user?',
        headers: { 'Client-ID': 'kx6k6d64t0ok27s5sfyo1w5n1q3dn6'},
        data: {get_param: 'value' },
        dataType: 'json',
        success: function (data) {
            var items = [];
            $.each(data, function (index, element) {
                $('body').append($('<div>', {
                    text: element.name
                }));
            });
        }
    });
});