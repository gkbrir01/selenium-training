using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Threading;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;


namespace csharp_example.tests
{
    [TestFixture]
    public class DucksAddRemove : TestBase
    {
        private IWebDriver driver;
        public const string nazwaPrzegladarki = "Chrome";
        int i = 0;
        
        [SetUp]
        public void start()
         
        {
            if (nazwaPrzegladarki == "Firefox")
            {
                FirefoxOptions options = new FirefoxOptions();
                driver = new FirefoxDriver();
            }
                       
            if (nazwaPrzegladarki == "IE")
            {
                driver = new InternetExplorerDriver();
            }

            if (nazwaPrzegladarki == "Chrome")
            {
                ChromeOptions optionsChrome = new ChromeOptions();
                optionsChrome.AddArguments("start-maximized");
                driver = new ChromeDriver(optionsChrome);
            }
            
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            
        }
        

        bool AreElementsPresent(IWebDriver driver, By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }

        int numberDucks = 3;

        [Test]
        public void AddRemoveDucks()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Navigate().GoToUrl("http://localhost/litecart");
            Thread.Sleep(1000);

            for (int i = 1; i <= numberDucks; i++)
            {
                //Choice Duck
                driver.FindElement(By.CssSelector("#box-latest-products li:nth-child(" + i + ")>a.link")).Click();
                Thread.Sleep(1000);
                //Checking if the duck has a field Size
                bool size = AreElementsPresent(driver, By.CssSelector("[name='options[Size]']"));
                if (size)
                {
                    new SelectElement(driver.FindElement(By.CssSelector("[name='options[Size]']"))).SelectByValue("Medium");
                }

                //the Number of ducks in a cart
                IWebElement amountGoods = driver.FindElement(By.CssSelector("#cart-wrapper .quantity"));

                //Add Duck to cart
                driver.FindElement(By.Name("add_cart_product")).Click();
                Thread.Sleep(1000);

                //Waiting for the new number of products in a cart
                wait.Until(ExpectedConditions.TextToBePresentInElement(amountGoods, "" + i + ""));
                amountGoods = driver.FindElement(By.CssSelector("#cart-wrapper .quantity"));
                Console.WriteLine("Number of ducks in a cart: " + amountGoods.Text);
                //Checking the number of products in a cart with a number of added
                NUnit.Framework.Assert.IsTrue(amountGoods.Text.Equals("" + i + ""));

                //Go to Main Page
                driver.FindElement(By.CssSelector(".fa.fa-home")).Click();
                Thread.Sleep(1000);
            }

            //Opening cart
            driver.FindElement(By.CssSelector("#cart .link")).Click();
            Thread.Sleep(2000);

            IWebElement tableCart = driver.FindElement(By.CssSelector(".dataTable.rounded-corners"));
            IList<IWebElement> rowsCart = tableCart.FindElements(By.CssSelector("tr:not(.header)"));

            //Remove Ducks
            for (int i = numberDucks; i > 0; i--)
            {
                IWebElement duckRemove = driver.FindElement(By.CssSelector("[name=remove_cart_item]"));
                duckRemove.Click();
                //Wait that removed the duck disappeared
                wait.Until(ExpectedConditions.StalenessOf(duckRemove));
                Console.WriteLine("Remove " + i + " Duck");

                IWebElement lastRow = driver.FindElement(By.CssSelector(".dataTable.rounded-corners tr:nth-child(" + (i + 1) + ") td.item"));
                //Wait as the last line of the product will disappear from the table
                wait.Until(ExpectedConditions.StalenessOf(lastRow));
            }

            //Wait until the table disappears
            wait.Until(ExpectedConditions.StalenessOf(tableCart));

            //Go to Main Page
            driver.FindElement(By.CssSelector(".fa.fa-home")).Click();
            Thread.Sleep(2000);
            //Thread.Sleep(10000);
        }
        
        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }

}
