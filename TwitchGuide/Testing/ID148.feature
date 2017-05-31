Feature: As a user, I want to see the avatar of my favorites next to their name on the homepage

Scenario: the avatar is there next to the name
	Given that the user is logged in as Team Bitsmith
		And on the homepage
	When the user scrolls down to the favorites section they will see an avatar next to the followed users