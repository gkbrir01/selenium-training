using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_example.pages
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

        internal void ChoiceDuck(int i)
        {
            latestDucks[i].Click();
        }

    }
}
