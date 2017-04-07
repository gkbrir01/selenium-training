using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace csharp_example
{
    internal class CartPage : Page
    {
        public CartPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        //[FindsBy(How = How.CssSelector, Using = "[name=remove_cart_item]")]
        //public IWebElement duckRemove;
        [FindsBy(How = How.CssSelector, Using = ".fa.fa-home")]
        internal IWebElement toMainPage;
        //[FindsBy(How = How.CssSelector, Using = ".dataTable.rounded-corners")]
        //internal IWebElement tableCart;
        
        
        internal void GoToMainPage()
        {
            toMainPage.Click();
        }

        internal void RemoveDuckAndWait()
        {
            IWebElement duckRemove = driver.FindElement(By.CssSelector("[name=remove_cart_item]"));
            duckRemove.Click();
            //Wait that removed the duck disappeared
            wait.Until(ExpectedConditions.StalenessOf(duckRemove));
            Console.WriteLine("Remove Duck");
        }
               
        internal void LastRecordDisappearInTableWait(int nrDuckInCart)
        {
            IWebElement lastRow = driver.FindElement(By.CssSelector(".dataTable.rounded-corners tr:nth-child(" + (nrDuckInCart+1) + ") td.item"));
            //Wait as the last line of the product will disappear from the table
            wait.Until(ExpectedConditions.StalenessOf(lastRow));
        }
    }
}
