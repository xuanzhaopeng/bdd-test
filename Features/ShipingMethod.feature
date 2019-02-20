Feature: Shiping methods
    Scenario: I want to change chosen shipping methods
        Given I am on the NL website
        And All cookies are set
        And I have one item in my shopping basket
        And I proceed to the checkout page
        And I continue as a CK guest
        When Nearest UPS point is selected
        And I proceed to the payment page
        And I click the back button in my browser
        And I change the delivery method to standard
        Then I should get the option to fill in my own address
