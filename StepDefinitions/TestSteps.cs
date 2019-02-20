using System;
using TechTalk.SpecFlow;

namespace TestSpecs {
    [Binding]
    class TestStpes
    {
        [Given(@"I am on the (.*) website")]
        public void GivenIAmOnTheNLWebsite(string countryCode)
        {
            setCountryCode(countryCode);
            Driver.Navigate().GoToUrl(baseURLCK);
        }

        [Given(@"All cookies are set")]
        public void GivenAllCookiesAreSet()
        {
            Driver.Manage().Cookies.DeleteCookieNamed("PVH_COOKIES_GDPR");
            Driver.Manage().Cookies.DeleteCookieNamed("PVH_COOKIES_GDPR_ANALYTICS");
            Driver.Manage().Cookies.DeleteCookieNamed("PVH_COOKIES_GDPR_SOCIALMEDIA");
            Driver.Manage().Cookies.DeleteCookieNamed("newsletterSeen");
            Driver.Manage().Cookies.AddCookie(new Cookie("PVH_COOKIES_GDPR", "Accept"));
            Driver.Manage().Cookies.AddCookie(new Cookie("PVH_COOKIES_GDPR_ANALYTICS", "Accept"));
            Driver.Manage().Cookies.AddCookie(new Cookie("PVH_COOKIES_GDPR_SOCIALMEDIA", "Accept"));
            Driver.Manage().Cookies.AddCookie(new Cookie("newsletterSeen", "true"));
            Driver.Navigate().GoToUrl(baseURLCK);
        }

        [Given(@"I have one item in my shopping basket")]
        public void GivenIHaveOneItemInMyShoppingBasket()
        {
            string ShoppingBasket1 = baseURLCK + "BasketRePopulate?ean=8719113837794&qty=1";
            Driver.Navigate().GoToUrl(ShoppingBasket1);
        }

        [Given(@"I proceed to the checkout page")]
        public void GivenIProceedToTheCheckoutPage()
        {
            Wait.Until(Driver => Driver.FindElement(By.ClassName("stickyPanel")).Displayed);
            IWebElement CheckOutButton = Driver.FindElement(By.ClassName("stickyPanel"));
            CheckOutButton.FindElement(By.ClassName("proceedToCheckout")).Click();
        }

        [Given(@"I continue as a CK guest")]
        public void GivenIContinueAsACKGuest()
        {
            Wait.Until(Driver => Driver.FindElement(By.ClassName("guestLogin")).Displayed);
            IWebElement GuestLogin = Driver.FindElement(By.ClassName("guestLogin"));
            GuestLogin.FindElement(By.Id("checkoutContinueGuestWebsphere")).Click();
        }

        [When(@"Nearest UPS point is selected")]
        public void WhenNearestUPSPointIsSelected()
        {
            Wait.Until(Driver => Driver.FindElement(By.ClassName("checkoutColumn")).Displayed);
            IWebElement RadioButton1 = Driver.FindElement(By.ClassName("deliveryAddress"));
            var UPSRadioButton = RadioButton1.FindElements(By.ClassName("methodName"));
            UPSRadioButton.ElementAt(0).Click();
            Wait.Until(Driver => Driver.FindElement(By.Id("pupContainer")).Displayed);
            Driver.SwitchTo().Frame("pupContainer");
            Wait.Until(Driver => Driver.FindElement(By.ClassName("iframeDialog")).Displayed);
            IWebElement bewerken = Driver.FindElement(By.Id("editLink"));
            bewerken.FindElement(By.Name("EditLink")).Click();
            IWebElement InvulVelden = Driver.FindElement(By.ClassName("iframeDialog"));
            InvulVelden.FindElement(By.Name("AddressLine1")).SendKeys("Danzigerkade 165");
            InvulVelden.FindElement(By.Name("City")).SendKeys("Amsterdam");
            InvulVelden.FindElement(By.Name("PostalCode")).SendKeys("1013 AP");
            InvulVelden.FindElement(By.Name("FindButton")).Click();
            Wait.Until(Driver => Driver.FindElement(By.ClassName("subcol5")).Displayed);
            IWebElement LocationSelector = Driver.FindElements(By.ClassName("subcol5")).ElementAt(0);
            LocationSelector.FindElement(By.Name("SelectLocation")).Click();
            Driver.SwitchTo().DefaultContent();
            System.Threading.Thread.Sleep(1000);
        }
         
        [When(@"I proceed to the payment page")]
        public void WhenIProceedToThePaymentPage()
        {
            Wait.Until(Driver => Driver.FindElement(By.Id("pickUpPointForm")).Displayed);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", Driver.FindElement(By.Id("pickUpPointForm")));
            IWebElement persDetails = Driver.FindElement(By.Id("pickUpPointForm"));
            IWebElement TitleSelector = persDetails.FindElement(By.Id("pickUpPoint_title_chosen"));
            TitleSelector.Click();
            IWebElement TitleSelected = TitleSelector.FindElement(By.ClassName("chosen-drop"));
            TitleSelected.FindElements(By.ClassName("active-result")).ElementAt(1).Click();
            //var dropdownpers = Driver.FindElements(By.ClassName("active-result")).Where(x => x.Displayed);
            //dropdownpers.ElementAt(1).Click();
            IWebElement Voornaam = Driver.FindElement(By.ClassName("pupFirstName"));
            Voornaam.FindElement(By.Name("firstName")).SendKeys("Theo");
            IWebElement Achternaam = Driver.FindElement(By.ClassName("pupLastName"));
            Achternaam.FindElement(By.Name("lastName")).SendKeys("Test");
            IWebElement Telefoon = Driver.FindElement(By.ClassName("pupPhone"));
            Telefoon.FindElement(By.Name("phone1")).SendKeys("0612345678");
            IWebElement Email = Driver.FindElement(By.ClassName("pupEmail"));
            Email.FindElement(By.Name("email1")).SendKeys("test@test.test");
            IWebElement Betalen = Driver.FindElement(By.ClassName("checkoutControls"));
            Betalen.FindElement(By.Id("continueToPayment")).Click();
        }

        [When(@"I click the back button in my browser")]
        public void WhenIClickTheBackButtonInMyBrowser()
        {
            Wait.Until(Driver => Driver.FindElement(By.ClassName("paymentOptionSelect")).Displayed);
            Driver.Navigate().Back();
        }
         
        [When(@"I change the delivery method to standard")]
        public void WhenIChangeTheDeliveryMethodToStandard()
        {
            Wait.Until(Driver => Driver.FindElement(By.ClassName("shippingSelection")).Displayed);
            IWebElement RadioButton2 = Driver.FindElement(By.ClassName("shippingSelection"));
            RadioButton2.FindElements(By.ClassName("methodName")).ElementAt(1).Click();
        }

        [Then(@"I should get the option to fill in my own address")]
        public void ThenIShouldGetTheOptionToFillInMyOwnAddress()
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);",
                 Driver.FindElement(By.Id("WC_AddressForm_div_personTitle")));
            try
            {
                IWebElement visible = Driver.FindElement(By.Id("WC_AddressForm_div_address2"));
                Assert.IsTrue(visible.Displayed);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }
    }
}