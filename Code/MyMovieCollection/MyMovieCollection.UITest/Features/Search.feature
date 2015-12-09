Feature: Search
	In order to find information about movies
	As a movie enthusiast
	I want to find movies in the app

@mytag
Scenario: I can search for spectre
	Given I open the app
	And I search for "Spectre" 
	Then I should see "Spectre" in the results
