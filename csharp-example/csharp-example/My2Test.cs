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
    public class My2Test
    {
        private IWebDriver driver;
        //private WebDriverWait wait;
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
            //wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }


        //WORK 7
        [Test]
        public void Test7()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            //wait.Until(ExpectedConditions.TitleIs("My Store"));

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
            //wait.Until(ExpectedConditions.TitleIs("Online Store | My Store"));
            //Thread.Sleep(1000);

            IList<IWebElement> elements = driver.FindElements(By.CssSelector("a.link[title$=Duck]"));
            


            foreach (IWebElement element in elements)
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Duck: " + ++i);
                Console.WriteLine("Położenie: " + element.Location);
                Console.WriteLine("Rozmiar: " + element.Size);
                Console.WriteLine("Kolor: " + element.GetCssValue("color"));
                Console.WriteLine("Text: "+element.Text);
                Console.WriteLine("TextAtrr: " + element.GetAttribute("textContent"));

                IList<IWebElement> sticks = element.FindElements(By.CssSelector("div.sticker"));

                if(sticks.Count > 1)
                {
                    Console.WriteLine("Duck has more than 1 stick !!!!!");
                }

                Console.WriteLine("The number od stickers: "+ sticks.Count);

                foreach (IWebElement stick in sticks)
                {
                
                    Console.WriteLine("Kind of sticker: " + stick.Text);
                    Console.WriteLine("Kind of stickerAtt: " + stick.GetAttribute("textContent"));
                }
                
              }
        }
//--------------------------------------------------------------------------------------------------------
//WORK 9
        [Test]
        public void Test9()
        {
            List<string> countriesZones = new List<string>();
            List<string> countries = new List<string>();
            List<string> countriesTemp = new List<string>();

            driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=countries&doc=countries");

            IWebElement table = driver.FindElement(By.CssSelector("table.dataTable"));
            IList<IWebElement> rows = table.FindElements(By.CssSelector("tr.row"));

            foreach (IWebElement row in rows)
            {
                IList<IWebElement> cells = row.FindElements(By.TagName("td"));
                string country = cells[4].Text;
                string zones = cells[5].Text;
                if (zones != "0")
                {
                    countriesZones.Add(country);
                }

                 if (country != "")
                 {
                    countries.Add(country);
                    countriesTemp.Add(country);
                  }                
            }
                    
            countriesTemp.Sort();
            //countriesTemp[5]="AAAA";
            int numberCountries = countries.Count;
            for (i=0;i < numberCountries; i++)
            {
                bool comparison = true;
                comparison = Comparer.Equals(countries[i], countriesTemp[i]);
                if (!comparison)
                {
                    Console.WriteLine("Sorting Wrong: " + comparison);
                }
               
            }

            foreach (string countryZone in countriesZones)
            {
                List<string> countriesS = new List<string>();
                List<string> countriesTempS = new List<string>();

                driver.FindElement(By.LinkText(countryZone)).Click();

                IWebElement tableS = driver.FindElement(By.CssSelector("table.dataTable"));
                IList<IWebElement> rowsS = tableS.FindElements(By.CssSelector("tr:not(.header)"));
                foreach (IWebElement row in rowsS)
                {
                    IList<IWebElement> cellsS = row.FindElements(By.TagName("td"));
                    string countryS = cellsS[2].Text;
                    
                   if (countryS != "")
                    {
                        countriesS.Add(countryS);
                        countriesTempS.Add(countryS);
                        Console.WriteLine("Country: " + countryS);
                    }
                   
                }

                countriesTempS.Sort();
                //countriesTemp[5]="AAAA";

                int numberCountriesS= countriesS.Count;

                for (i = 0; i < numberCountriesS; i++)
                {
                    bool comparison = true;
                    comparison = Comparer.Equals(countriesS[i], countriesTempS[i]);
                    if (!comparison)
                    {
                        Console.WriteLine("Sorting Wrong: " + comparison);
                        break;
                    }

                }
                Thread.Sleep(2000);
                driver.Navigate().Back();
                
            }
           
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones");
            List<string> countriesGeo = new List<string>();
           
            IWebElement tableGeo = driver.FindElement(By.CssSelector("table.dataTable"));
            IList<IWebElement> rowsGeo = tableGeo.FindElements(By.CssSelector("tr.row"));

            foreach (IWebElement rowGeo in rowsGeo)
            {
                IList<IWebElement> cellsGeo = rowGeo.FindElements(By.TagName("td"));
                string countryGeo = cellsGeo[2].Text;
                countriesGeo.Add(countryGeo);
                Console.WriteLine("Country: " + countryGeo);
            }

            foreach (string countryGeo in countriesGeo)
            {

                List<string> countriesGeoS = new List<string>();
                List<string> countriesTempGeoS = new List<string>();

                Console.WriteLine("Link do Kraju: " + countryGeo);
                driver.FindElement(By.LinkText(countryGeo)).Click();
                Thread.Sleep(2000);

                IWebElement tableGeoS = driver.FindElement(By.CssSelector("table.dataTable"));
                IList<IWebElement> rowsGeoS = tableGeoS.FindElements(By.CssSelector("tr:not(.header)"));
                int ilZones = rowsGeoS.Count;

                for (int i=0;i<(ilZones-1);i++)
                {
                    IList<IWebElement> cellsGeoS = rowsGeoS[i].FindElements(By.CssSelector("td [selected]"));
                    string countryGeoS = cellsGeoS[1].Text;
                    countriesGeoS.Add(countryGeoS);
                    countriesTempGeoS.Add(countryGeoS);
                    Console.WriteLine("Kraina: " + countryGeoS);
                }

                countriesTempGeoS.Sort();

                int liczbKrajowGeoS = countriesGeoS.Count;
                for (i = 0; i < liczbKrajowGeoS; i++)
                {
                    bool porownanie = true;
                    porownanie = Comparer.Equals(countriesGeoS[i], countriesTempGeoS[i]);
                    if (!porownanie)
                    {
                        Console.WriteLine("Sorting Wrong: " + porownanie);
                        break;
                    }

                }
                driver.Navigate().Back();
            }
        }
//-------------------------------------------------------------------------------------------------------
        //WORK 10
        [Test]
        public void Test10()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart");

            IWebElement element = driver.FindElement(By.CssSelector("#box-campaigns .link"));
                        
            //Name Duck Main Page
            IWebElement nameElement = driver.FindElement(By.CssSelector("#box-campaigns .name"));
            string nameM = nameElement.GetAttribute("textContent");
                   
            //Price1 Duck Main Page
            IWebElement price1Element = driver.FindElement(By.CssSelector("#box-campaigns .regular-price"));
            string price1M = price1Element.Text;
            //Price1 Color Duck Main Page
            string price1MColor = price1Element.GetCssValue("Color");
            //Price1 Text-Decoration Duck Main Page
            string price1MDeco = price1Element.GetCssValue("text-decoration");
            

            //Price2 Duck Main Page
            IWebElement price2Element = driver.FindElement(By.CssSelector("#box-campaigns .campaign-price"));
            string price2M = price2Element.Text;
            //Price2 Color Duck Main Page
            string price2MColor = price2Element.GetCssValue("Color");
            //Price1 Text-Decoration Duck Main Page
            string price2MDeco = price2Element.GetCssValue("font-weight");
            
            Thread.Sleep(1000);
            element.Click();
            Thread.Sleep(4000);

            //Name Duck Product Page
            IWebElement nameElementP = driver.FindElement(By.CssSelector("#box-product h1"));
            string nameP = nameElementP.GetAttribute("textContent");

            //Comparison of names    
            Console.WriteLine("Comparison Name Duck main page: "+nameP+ " product page: " + nameP);
            NUnit.Framework.Assert.AreEqual(nameM, nameP);

            //Price1 Duck Product Page
            IWebElement price1ElementP = driver.FindElement(By.CssSelector(".information .regular-price"));
            string price1P = price1ElementP.Text;
            //Price1 Color Duck Product Page
            string price1PColor = price1ElementP.GetCssValue("Color");
            //Price1 Text-decoration Duck Product Page
            string price1PDeco = price1ElementP.GetCssValue("text-decoration");

                  
            Console.WriteLine("Comparison Price1 Duck main page: " + price1M + " product page: " + price1P);
            NUnit.Framework.Assert.AreEqual(price1M, price1P);
            //Price1 Color comparison - Here is the difference of color
            Console.WriteLine("Comparison Price1 Color Duck main page: " + price1MColor + " product page: " + price1PColor);
            //NUnit.Framework.Assert.AreEqual(price1MColor, price1PColor);
            //Price1 Text-decoration comparison
            Console.WriteLine("Comparison Price1 Text-decoration Duck main page: " + price1MDeco + " product page: " + price1PDeco);
            NUnit.Framework.Assert.AreEqual(price1MDeco, price1PDeco);

            //Price2 Duck Product Page
            IWebElement price2ElementP = driver.FindElement(By.CssSelector(".information .campaign-price"));
            string price2P = price2ElementP.Text;
            //Price2 Color Duck Product Page
            string price2PColor = price2ElementP.GetCssValue("Color");
            //Price1 Text-decoration Duck Product Page
            string price2PFontW = price2ElementP.GetCssValue("font-weight");

            //Price2 comparison
            Console.WriteLine("Comparison Price2 Duck main page: " + price2M + " product page: " + price2P);
            NUnit.Framework.Assert.AreEqual(price2M, price2P);
            //Price2 Color comparison
            Console.WriteLine("Comparison Price2 Color Duck main page: " + price2MColor + " product page: " + price2PColor);
            NUnit.Framework.Assert.AreEqual(price2MColor, price2PColor);
            //Price1 Text-decoration comparison
            Console.WriteLine("Comparison Price2 Font-Weight Duck main page: " + price2MDeco + " product page: " + price2PFontW);
            NUnit.Framework.Assert.AreEqual(price2MDeco, price2PFontW);
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}