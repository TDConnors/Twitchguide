Feature: Advanced search feature that returns partial matches or a custom error page if no matches could be found.

Scenario: No matches found
Given the text "zzz" in the search box
When the user presses search
Then the page returns no matches found custom page

Scenario: one or more matches found
Given the text "smith" in the search box
When the user presses search
Then a page with a list of a few matching users is returned.