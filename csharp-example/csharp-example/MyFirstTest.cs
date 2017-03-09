using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using System.Threading;

namespace csharp_example
{
    [TestFixture]
    public class MyFirstTest
    {
        //private IWebDriver driver;
        private WebDriverWait wait;
        private IWebDriver chromeDriver;
        private IWebDriver ieDriver;
        private IWebDriver firefoxDriver;
        public const string nazwaPrzegladarki = "Chrome";

        [SetUp]
        public void start()
        {
            if (nazwaPrzegladarki == "Firefox")
            {
                //Firefox nowy
                FirefoxOptions optionsFirefox = new FirefoxOptions();
                optionsFirefox.UseLegacyImplementation = false;
                //FirefoxProfile profile = new FirefoxProfile();
                //profile.SetPreference();        
                firefoxDriver = new FirefoxDriver(optionsFirefox);

                //Firefox stary
                //FirefoxOptions options = new FirefoxOptions();
                //options.UseLegacyImplementation = true;
                //driver = new FirefoxDriver(options);
            }

            if (nazwaPrzegladarki == "IE")
            {
                //W przeciwieństwie do innych języków, w C# konstruktor sterownika zazwyczaj przyjmuje nie obiekt typu ICapabilities,
                //a bardziej specjalistyczny obiekt, który zawiera zestaw opcji sterownika, i dla każdego sterownika jest swój. 
                InternetExplorerOptions optionsIE = new InternetExplorerOptions();
                optionsIE.UnexpectedAlertBehavior = InternetExplorerUnexpectedAlertBehavior.Dismiss;
                optionsIE.RequireWindowFocus = true;
                optionsIE.IgnoreZoomLevel = true;
                //lepiej samemu ustawić ręcznie w przegladarce tryb chroniony, ustawienie poniżej czasami działa żle 
                optionsIE.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                ieDriver = new InternetExplorerDriver(optionsIE);
            }

            if (nazwaPrzegladarki == "Chrome")
            {
                ChromeOptions optionsChrome = new ChromeOptions();
                //wskazanie ścieżki do pliku
                //optionsChrome.BinaryLocation = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
                //optionsChrome.AddArguments("start-fullscreen");
                optionsChrome.AddArguments("start-maximized");
                //instalacja rozszerzenia
                //optionsChrome.AddExtensions(@"C:\Program Files (x86)\Google\Chrome\Application\extension.crx");
                Console.WriteLine(optionsChrome.ToCapabilities());
                chromeDriver = new ChromeDriver(optionsChrome);
            }
                 
            wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void FirstTest()
        {
            chromeDriver.Url = "http://www.google.pl";
            chromeDriver.FindElement(By.Name("q")).SendKeys("webdriver");
            IWebElement button = chromeDriver.FindElement(By.Name("btnG"));

            if (button.Displayed)
            {
                button.Click();
            }
            Thread.Sleep(3000);

            wait.Until(ExpectedConditions.TitleIs("webdriver - Szukaj w Google"));
          
            
            //cookies
            //Przyłączanie do istniejacej sesji
            chromeDriver.Manage().Cookies.AddCookie(new Cookie("test", "test"));
            //Cookies według określonej nazwy
            Cookie testCookie = chromeDriver.Manage().Cookies.GetCookieNamed("test");
            Console.WriteLine(testCookie);
            //Lista cookies
            ICollection<Cookie> cookies = chromeDriver.Manage().Cookies.AllCookies;
            foreach (Cookie link in cookies)
            {
                Console.WriteLine(link);
            }

            chromeDriver.Manage().Cookies.DeleteCookieNamed("test");
            chromeDriver.Manage().Cookies.DeleteAllCookies();
        }

        [TearDown]
        public void stop()
        {
            chromeDriver.Quit();
            chromeDriver = null;
        }
      }
    }
