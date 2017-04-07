using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;

namespace csharp_example
{
    internal class MainPage : Page
    {
        public MainPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        internal void Open()
        {
            driver.Url = "http://localhost/litecart";
        }

        [FindsBy(How = How.CssSelector, Using = "#box-latest-products li>a.link")]
        IList<IWebElement> latestDucks;

        [FindsBy(How = How.CssSelector, Using = "#cart .link")]
        internal IWebElement cartLink;

        internal void ChoiceDuck(int i)
        {
            latestDucks[(i-1)].Click();
        }

        internal void GoToCart()
        {
            cartLink.Click();
        }

    }
}
