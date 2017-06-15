Feature: As a User, I want to search for user profiles from any page on the site

Scenario: Successful search for user
	Given the username "Team Bitsmith" in the search box
	When the user clicks "search"
	Then the page redirects to that person's profile page

Scenario: unsucessful search for user
	Given the username "xyz" in the search box
	When the user clicks "search"
	Then the page redirects to an error page