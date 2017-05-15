Feature: As a User, I want to see each favorite's username to link to their profile page

Scenerio: the link successfully works
Given the user is logged in as Team Bitsmith and on the homepage
When the user clicks on TConnorsBitsmith they are taken to TConnorsBitsmith's profile

Scenerio: the user is not logged in
Given the user is not logged in and on the home page
the favorites section will be empty
