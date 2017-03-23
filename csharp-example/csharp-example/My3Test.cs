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
using System.Linq;
using System.Collections;

namespace csharp_example
{
    [TestFixture]
    public class My3Test
    {
        private IWebDriver driver;

        //private WebDriverWait wait;
        public const string nazwaPrzegladarki = "Chrome";
        

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
                driver = new ChromeDriver();
            }


            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
            driver.Manage().Window.Maximize();
        }


        [Test]
        public void Test7()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            
                
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}