Feature: As a user, I want to see the avatar of my favorites next to their name on the homepage

Scenario: the username is correct
Given the user is logged in as Team Bitsmith
And on the homepage
When the user looks in the top right corner they will see "bitsmiths"

Scenerio: the user is not logged in
Given the user is not logged in
and the user is on the homepage
When the user looks in the top right corner they will see "Sign in"
