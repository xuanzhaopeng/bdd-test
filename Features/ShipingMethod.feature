Feature: CK - Shiping methods

    Scenario Outline: I can change shipping methods when shippiing basket is not empty
        Given there is 1 user
        Given the user T1 is me
        Given I am on the <CountryCode> home page with correct user cookies
        Given I have <ProductCountInBasket> product in shopping basket
        Given I click Check out button in home page
        Given I click Guest Login button in check out page
        When I set shiping method to <ShipingMethod1> in check out page
        And I fill up client info in check out page
        And I click payment button in check out page
        And I click the back button in my browser
        And I set shiping method to <ShipingMethod2> in check out page
        Then I should see delivery address fileds are visible in checkout page

    Examples:
    | CountryCode | ProductCountInBasket | ShipingMethod1    | ShipingMethod2 |
    | NL          | 1                    | Nearest UPS Point | Standard       |
