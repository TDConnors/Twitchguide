// do an ajax call to get some numbers
$(document).ready(function () {
            var ID = document.getElementById("twitchId").getAttribute('src');
            var source = "https://api.twitch.tv/kraken/users/"+ ID;
            console.log(source);
            // get data in JSON format from our controller    
            $.ajax({
                type: "GET",
                dataType: "json",
                url: source,
                success: displayData
            });
    });

    function displayData(Data)
    {
    }