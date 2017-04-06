using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_example.pages
{
    internal class CartPage : Page
    {
        public CartPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        
        [FindsBy(How = How.CssSelector, Using = "#cart .link")]
        internal IWebElement cartLink;
        [FindsBy(How = How.CssSelector, Using = "[name=remove_cart_item]")]
        internal IWebElement duckRemove;
        [FindsBy(How = How.CssSelector, Using = ".fa.fa-home")]
        internal IWebElement toMainPage;
        [FindsBy(How = How.CssSelector, Using = ".dataTable.rounded-corners")]
        internal IWebElement tableCart;
        [FindsBy(How = How.CssSelector, Using = ".dataTable.rounded-corners tr>td.item")]
        internal IList<IWebElement> rowsTableCart;

        internal void OpenCart()
        {
            cartLink.Click();
        }

        internal void goToMainPage()
        {
            toMainPage.Click();
        }

        internal void RemoveDuck()
        {
            duckRemove.Click();
        }


    }
}
