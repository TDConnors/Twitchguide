# BITSMITHS!

This is the repository for the Bitsmiths team in Dr. Morse's CS Senior Project class.
Class work and projects will be stored here. 

* * *

## Our Project
The Twitch Channel Guide (TCG) is a web app that helps Twitch users better navigate Twitch. Twitch is a broadcasting service specifically targeted at video game players. Users can stream their game play from a personal computer, video game console or handheld device. Users will be able to log in to the TCG with their Twitch account. On their TCG account users can set their own streaming schedule. A user will be able to see when a streamer is going to stream as long as the user has followed the streamer. Users will also  be able to change the time scope of their guide. Unlike Twitch's current navigation, the TCG will navigate Twitch using a schedule generated for each user. The Twitch Channel Guides purpose is to simplify and enhance each user's streaming experience.

### Project Status
Base construction beginning soon!

Deployed site: [TwitchChannelGuide.Azurewebsites.net](TwitchChannelGuide.Azurewebsites.net)

Log in using a Twitch account. Change your schedule on your profile page and see others using the search bar.

### Building
Please build our project using Visual Studio 2015

### Guidelines
* Use the table name in the Primary key name, i.e ProductID
* All public methods include XML comments
* Curly Braces on their own lines
* All work is to be done on a new branch per pull request, no working off of the Develop branch.
* Indenting
* Good commit messages
* No auto-generated build files
* Don't commit code that doesn't compile.

### Construction Process

Create a database server.
After that, use our sql script located in the App_Data folder to create the tables

Add your connection string to the web.config file that you get from your database server, replacing the other links.


	##Getting Twitch Access
		First, create a [Twitch](https://www.twitch.tv) account.
		After that, register the app [here](https://www.twitch.tv/kraken/oauth2/clients/new)
		Take the new client_id they give you and Find and replace 'kx6k6d64t0ok27s5sfyo1w5n1q3dn6' in the project with that client id.

When all that is done just publish the website to a server.

If you have any trouble with twitch you can look at their page on authentication [here](https://dev.twitch.tv/docs/v5/guides/authentication/)

	
		
### Tools Used
* Microsoft Visual Studio 2015
* Twitch API
* Git
* Azure
* Visual Studio Team Services
* Nuget items:
    * Entity
    * Ninject
    * Moq
    
### Gource Video


<iframe id="streamer"

           src="https://drive.google.com/file/d/0BztzhKdfyjQMSzRlRGFkT2c4WXc/preview"

           height="420"

           width="560"

           frameborder="0"

           scrolling="no"

           allowfullscreen="true"></iframe>

* * *

## Team Members
* Repo Admin/Developer Brandon Hunt Bhunt12@wou.edu
* Developer Kyle Walsh Kwalsh15@wou.edu
* Developer Tyler Connors Tconnors14@wou.edu