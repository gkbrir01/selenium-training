using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Threading;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace csharp_example
{
    [TestFixture]
    public class My2Test
    {
        private IWebDriver driver;
        private WebDriverWait wait;
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

            if (nazwaPrzegladarki == "FirefoxESR")
            {
                FirefoxOptions options = new FirefoxOptions();
                options.BrowserExecutableLocation = @"c:\Program Files\Mozilla FirefoxESR\firefox.exe";
                driver = new FirefoxDriver(options);
            }

            if (nazwaPrzegladarki == "FirefoxN")
            {
                FirefoxOptions options = new FirefoxOptions();
                options.BrowserExecutableLocation = @"c:\Program Files\Nightly\firefox.exe";
                driver = new FirefoxDriver(options);
            }


            if (nazwaPrzegladarki == "IE")
            {
                driver = new InternetExplorerDriver();
            }

            if (nazwaPrzegladarki == "Chrome")
            {
                //ChromeOptions optionsChrome = new ChromeOptions();
                //Console.WriteLine(optionsChrome);
                driver = new ChromeDriver();
            }


            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }


        //WORK 7
        [Test]
        public void Test7()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("My Store"));

            //Number of main menu items
            int mEnd = driver.FindElements(By.CssSelector("#app-")).Count;
           
            for (int m = 1; m <= mEnd; m++)
            {

                IWebElement menu = driver.FindElement(By.CssSelector("#app-:nth-child(" + m + ")"));
                menu.Click();

                //The number of nested points in the main menu item                
                int sEnd = driver.FindElements(By.CssSelector("#app-:nth-child(" + m + ") li")).Count;
                                
                for (int s = 1; s <= sEnd; s++)
                {
                    IWebElement submenu = driver.FindElement(By.CssSelector("#app-:nth-child(" + m + ") li:nth-child(" + s + ")"));
                    String submenuTXT = submenu.Text;
                    submenu.Click();

                    //comparing the header with the menu item
                    IWebElement title = driver.FindElement(By.CssSelector("h1"));
                    String titleTXT = title.Text;
                    if (submenuTXT != titleTXT)
                    {
                        Console.WriteLine("submenu "+ submenuTXT+" and title "+ titleTXT+" are not the same");
                    }
                }
            }
        }




        //WORK 8
        [Test]
        public void Test8()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart");
            wait.Until(ExpectedConditions.TitleIs("Online Store | My Store"));
            //Thread.Sleep(1000);

            IList<IWebElement> elements = driver.FindElements(By.CssSelector("a.link[title$=Duck]"));
            
            foreach (IWebElement element in elements)
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Duck " + ++i);
                Console.WriteLine(element.Location);
                //Console.WriteLine(element.Text);
                
                IList<IWebElement> sticks = element.FindElements(By.CssSelector("div.sticker"));

                if(sticks.Count > 1)
                {
                    Console.WriteLine("Duck has more than 1 stick !!!!!");
                }

                Console.WriteLine("The number od stickers: "+ sticks.Count);

                foreach (IWebElement stick in sticks)
                {
                
                    Console.WriteLine("Kind of sticker: " + stick.Text);
                }
                
              }
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}