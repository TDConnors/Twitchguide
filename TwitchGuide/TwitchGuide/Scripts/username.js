/*gets the username from the json file retrieved from twitch*/
$ (function () 
	{
	var xmlhttp = new XMLHttpRequest(),
    url = 'https://api.twitch.tv/kraken/user?'
    xmlhttp.onreadystatechange = function()
	{
        if(this.readyState == 4 && this.status == 200)
		{
            var myArr = JSON.parse(this.responseText);
            document.getElementById('userstats').innerHTML += myArr.display_name;
		}
	});		