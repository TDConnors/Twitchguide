$(function() {
    $.ajax({
        type: 'GET',
        url: 'https://api.twitch.tv/kraken/user?',
    headers: {
        'Client-ID': 'kx6k6d64t0ok27s5sfyo1w5n1q3dn6'
    },
    success: function(data) {
        document.getElementById("userstats").innerHTML=data;
    }
});
})