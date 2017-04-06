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
using OpenQA.Selenium.Remote;

namespace csharp_example
{
    [TestFixture]
    public class My3Test
    {
        private IWebDriver driver;

        //private WebDriverWait wait;
        public const string nazwaPrzegladarki = "Remote";
        

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

            if (nazwaPrzegladarki == "Remote")
            {
                //ChromeOptions optionsChrome = new ChromeOptions();
                //driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), DesiredCapabilities.Chrome());
                //IWebDriver driver;
                //https://www.browserstack.com
                //DesiredCapabilities capability = DesiredCapabilities.Chrome();
                DesiredCapabilities capability = DesiredCapabilities.Edge();
                capability.SetCapability("browserstack.user", "grzegorzkozowski1");
                capability.SetCapability("browserstack.key", "4jBobsJ8vpz18qPs4wzs");
                capability.SetCapability("build", "First build");
                capability.SetCapability("browserstack.debug", "true");
                capability.SetCapability("platform", "WINDOWS");
                //Platform can be one of MAC, WIN8, XP, WINDOWS, and ANY
                driver = new RemoteWebDriver(
                  new Uri("http://hub-cloud.browserstack.com/wd/hub/"), capability);
            }

            //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
            //driver.Manage().Window.Maximize();
        }


        [Test]
        public void Test7R()
        {
            driver.Navigate().GoToUrl("http://onet.pl");
            //driver.FindElement(By.Name("username")).SendKeys("admin");
            //driver.FindElement(By.Name("password")).SendKeys("admin");
            //driver.FindElement(By.Name("login")).Click();
            Thread.Sleep(5000);


        }

        [Test]
        public void Test8R()
        {
            driver.Navigate().GoToUrl("http://www.google.com");
            Console.WriteLine(driver.Title);

            IWebElement query = driver.FindElement(By.Name("q"));
            query.SendKeys("Browserstack");
            //query.Submit();
            driver.FindElement(By.Name("btnG")).Click();
            Console.WriteLine(driver.Title);
            Thread.Sleep(5000);
        
            
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
        
    }
}