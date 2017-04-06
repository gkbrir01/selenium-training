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
using OpenQA.Selenium.Support.Events;
using System.Diagnostics;

namespace csharp_example
{
    [TestFixture]
    public class MyFirstTest
    {
        //private IWebDriver driver;
        private WebDriverWait wait;
        //private IWebDriver chromeDriver;
        private EventFiringWebDriver chromeDriver;
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
                chromeDriver = new EventFiringWebDriver(new ChromeDriver(optionsChrome));
                //Program obsługi na pewnych wydarzeń
                //chromeDriver.FindingElement += ChromeDriver_FindingElement;
                chromeDriver.FindingElement += (sender, e) => Console.WriteLine(e.FindMethod);
                chromeDriver.FindElementCompleted += (sender, e) => Console.WriteLine(e.FindMethod + " found");
                chromeDriver.ExceptionThrown += (sender, e) => Console.WriteLine(e.ThrownException);
            }
                 
            wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(10));
        }
        /*
        private void ChromeDriver_FindingElement(object sender, FindElementEventArgs e)
        {
            Console.WriteLine(e.FindMethod);
        }
        */
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
        [Test]
        public void SecondTest()
        {
            chromeDriver.Url = "http://www.google.pl";
            chromeDriver.FindElement(By.Name("q")).SendKeys("webdriver");
            chromeDriver.FindElement(By.Name("btnG")).Click();
            wait.Until(ExpectedConditions.TitleIs("webdriver - Szukaj w Google"));
            chromeDriver.GetScreenshot().SaveAsFile("screen.png", ScreenshotImageFormat.Png);

        }

        public void unhide(IWebDriver driver, IWebElement element)
        {
            String script = "arguments[0].style.opacity=1;"
              + "arguments[0].style['transform']='translate(0px, 0px) scale(1)';"
              + "arguments[0].style['MozTransform']='translate(0px, 0px) scale(1)';"
              + "arguments[0].style['WebkitTransform']='translate(0px, 0px) scale(1)';"
              + "arguments[0].style['msTransform']='translate(0px, 0px) scale(1)';"
              + "arguments[0].style['OTransform']='translate(0px, 0px) scale(1)';"
              + "return true;";
            ((IJavaScriptExecutor)driver).ExecuteScript(script, element);
        }

        public void attachFile(IWebDriver driver, By locator, String file)
        {
            IWebElement input = driver.FindElement(locator);
            //unhide(driver, input);
            input.SendKeys(file);
        }

        [Test]
        public void TestFile()
        {
            chromeDriver.Url = "http://blueimp.github.io/jQuery-File-Upload/basic.html";
            attachFile(chromeDriver, By.Id("fileupload"), "C:\\temp\\image1.png");
            Thread.Sleep(20000);
        }

        [Test]
        public void TestFile1()
        {
            chromeDriver.Url = "http://imgup.net";
            chromeDriver.FindElement(By.Id("image_image")).SendKeys("C:\\temp\\image1.png");
            Thread.Sleep(20000);
        }



        [TearDown]
        public void stop()
        {
            chromeDriver.Quit();
            chromeDriver = null;
        }

        

    }
    }
