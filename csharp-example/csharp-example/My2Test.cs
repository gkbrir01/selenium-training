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
                ChromeOptions optionsChrome = new ChromeOptions();
                optionsChrome.AddArguments("start-maximized");
                //Console.WriteLine(optionsChrome);
                driver = new ChromeDriver(optionsChrome);
            }

            //DesiredCapabilities caps = new DesiredCapabilities();
           
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
            //driver.Manage().Window.Maximize();
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
                        Console.WriteLine("submenu " + submenuTXT + " and title " + titleTXT + " are not the same");
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
                Console.WriteLine("Text: " + element.Text);
                Console.WriteLine("TextAtrr: " + element.GetAttribute("textContent"));

                IList<IWebElement> sticks = element.FindElements(By.CssSelector("div.sticker"));

                if (sticks.Count > 1)
                {
                    Console.WriteLine("Duck has more than 1 stick !!!!!");
                }

                Console.WriteLine("The number od stickers: " + sticks.Count);

                foreach (IWebElement stick in sticks)
                {

                    Console.WriteLine("Kind of sticker: " + stick.Text);
                    Console.WriteLine("Kind of stickerAtt: " + stick.GetAttribute("textContent"));
                }

            }
        }
        //--------------------------------------------------------------------------------------------------------
        //WORK 9


        //The function to validate sort the items in the list
        public void CheckSortingList(List<string> listC, List<string> listCTemp)
        {
            int listItems = listC.Count;
            for (i = 0; i < listItems; i++)
            {
                bool comparison = true;
                comparison = Comparer.Equals(listC[i], listCTemp[i]);
                if (!comparison)
                {
                    Console.WriteLine("Sorting Wrong " + listC[i] + "  " + listCTemp[i]);
                }

            }
        }

        [Test]
        public void Test9()
        {
            List<string> countriesZones = new List<string>();
            List<string> countries = new List<string>();
            List<string> countriesTemp = new List<string>();
            //Login to shop
            driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            //Go to Page Countries
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=countries&doc=countries");

            IWebElement table = driver.FindElement(By.CssSelector("table.dataTable"));
            IList<IWebElement> rows = table.FindElements(By.CssSelector("tr.row"));

            foreach (IWebElement row in rows)
            {
                IList<IWebElement> cells = row.FindElements(By.TagName("td"));
                string country = cells[4].Text;
                string zones = cells[5].Text;

                //Save the side of the country the number of Zones > 0
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

            //Sorting the list of temporary
            countriesTemp.Sort();

            //Checking sort
            CheckSortingList(countries, countriesTemp);

            //Go to Websites countries the number of Zones> 0
            foreach (string countryZone in countriesZones)
            {
                List<string> countriesS = new List<string>();
                List<string> countriesTempS = new List<string>();

                driver.FindElement(By.LinkText(countryZone)).Click();
                Console.WriteLine("Passage to the side of the country the number of Zones > 0: " + countryZone);

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
                    }
                }

                //Sorting the list of temporary
                countriesTempS.Sort();
                //Checking sort
                CheckSortingList(countriesS, countriesTempS);

                Thread.Sleep(2000);
                driver.Navigate().Back();

            }

            //Go to Page Geo Zones
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
                //Go to Websites countries
                driver.FindElement(By.LinkText(countryGeo)).Click();
                Thread.Sleep(2000);

                IWebElement tableGeoS = driver.FindElement(By.CssSelector("table.dataTable"));
                IList<IWebElement> rowsGeoS = tableGeoS.FindElements(By.CssSelector("tr:not(.header)"));
                int ilZones = rowsGeoS.Count;

                for (int i = 0; i < (ilZones - 1); i++)
                {
                    IList<IWebElement> cellsGeoS = rowsGeoS[i].FindElements(By.CssSelector("td [selected]"));
                    string countryGeoS = cellsGeoS[1].Text;
                    countriesGeoS.Add(countryGeoS);
                    countriesTempGeoS.Add(countryGeoS);
                    //Console.WriteLine("Country: " + countryGeoS);
                }
                //Sorting the list of temporary
                countriesTempGeoS.Sort();

                //Checking sort
                CheckSortingList(countriesGeoS, countriesTempGeoS);

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

            //Name Duck on Main Page
            IWebElement nameElement = driver.FindElement(By.CssSelector("#box-campaigns .name"));
            string nameM = nameElement.GetAttribute("textContent");

            //Price1 Duck on Main Page
            IWebElement price1Element = driver.FindElement(By.CssSelector("#box-campaigns .regular-price"));
            string price1M = price1Element.Text;
            //Price1 Color Duck on Main Page
            string price1MColor = price1Element.GetCssValue("Color");
            //Price1 Text-Decoration Duck on Main Page
            string price1MDeco = price1Element.GetCssValue("text-decoration");
            //Price2 Duck on Main Page
            IWebElement price2Element = driver.FindElement(By.CssSelector("#box-campaigns .campaign-price"));
            string price2M = price2Element.Text;
            //Price2 Color Duck on Main Page
            string price2MColor = price2Element.GetCssValue("Color");
            //Price1 Text-Decoration Duck on Main Page
            string price2MDeco = price2Element.GetCssValue("font-weight");
            Thread.Sleep(1000);
            element.Click();
            Thread.Sleep(2000);

            //Name Duck on Product Page
            IWebElement nameElementP = driver.FindElement(By.CssSelector("#box-product h1"));
            string nameP = nameElementP.GetAttribute("textContent");

            //Comparison of names    
            Console.WriteLine("Comparison Name Duck main page: " + nameP + " product page: " + nameP);
            NUnit.Framework.Assert.AreEqual(nameM, nameP);

            //Price1 Duck on Product Page
            IWebElement price1ElementP = driver.FindElement(By.CssSelector(".information .regular-price"));
            string price1P = price1ElementP.Text;
            //Price1 Color Duck on Product Page
            string price1PColor = price1ElementP.GetCssValue("Color");
            //Price1 Text-decoration Duck on Product Page
            string price1PDeco = price1ElementP.GetCssValue("text-decoration");

            //Comparison of Price1      
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

        //-------------------------------------------------------------------------------------------------------
        //WORK 11
        //-------------------------------------------------------------------------------------------------------
        string email = "user@wp.pl";
        [Test]
        public void Test11()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/en/create_account");

            //Create account
            driver.FindElement(By.Name("firstname")).SendKeys("Grzegorz");
            driver.FindElement(By.Name("lastname")).SendKeys("Kozłowowski");
            driver.FindElement(By.Name("address1")).SendKeys("ul.Magiera 3/22");
            driver.FindElement(By.Name("postcode")).SendKeys("01-873");
            driver.FindElement(By.Name("city")).SendKeys("Warszawa");
            new SelectElement(driver.FindElement(By.CssSelector("select[name=country_code]"))).SelectByValue("PL");
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("phone")).SendKeys("+48600300000");
            driver.FindElement(By.Name("password")).SendKeys("elixir");
            driver.FindElement(By.Name("confirmed_password")).SendKeys("elixir");
            driver.FindElement(By.Name("create_account")).Click();

            //Logout
            driver.FindElement(By.CssSelector("div#box-account li:nth-child(4)>a")).Click();

            //Login
            driver.FindElement(By.CssSelector("input[name=email]")).SendKeys(email);
            driver.FindElement(By.CssSelector("input[name=password]")).SendKeys("elixir");
            driver.FindElement(By.CssSelector("button[name=login]")).Click();

            //Logout
            driver.FindElement(By.CssSelector("div#box-account li:nth-child(4)>a")).Click();

        }

        //-------------------------------------------------------------------------------------------------------
        //WORK 12
        //-------------------------------------------------------------------------------------------------------
        string name = "Black Duck";
        [Test]
        public void Test12()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            //Menu Catalog
            driver.FindElement(By.CssSelector("#app-:nth-child(2)")).Click();

            //Add New Product
            driver.FindElement(By.CssSelector(".button:nth-child(2)")).Click();

            //Tab General
            //Status
            driver.FindElement(By.CssSelector("input[name=status]")).Click();

            //Name
            driver.FindElement(By.CssSelector("#tab-general .input-wrapper>input")).SendKeys(name);

            //Code
            driver.FindElement(By.CssSelector("#tab-general tr>td>input")).SendKeys("D0005");

            //Categories
            IWebElement element = driver.FindElement(By.CssSelector("div.input-wrapper input[value='0']"));
            string check = element.GetAttribute("checked");
            if (check == "true")
            {
                element.Click();
            }

            driver.FindElement(By.CssSelector("div.input-wrapper input[value='1']")).Click();

            //Product Groups
            driver.FindElement(By.CssSelector("div.input-wrapper input[value='1-3']")).Click();

            //Quantity
            IWebElement quantity = driver.FindElement(By.CssSelector("#tab-general [name=quantity]"));
            quantity.Clear();
            quantity.SendKeys("20,00");

            //Sold Out Status
            new SelectElement(driver.FindElement(By.CssSelector("[name=sold_out_status_id]"))).SelectByValue("2");

            //Upload Images
            driver.FindElement(By.CssSelector("input[type=file]")).SendKeys(@"C:\Users\Public\Pictures\Sample Pictures\blackduck.jpg");

            //Lap Information
            driver.FindElement(By.CssSelector(".tabs li:nth-child(2)")).Click();

            //Manufacturer
            new SelectElement(driver.FindElement(By.Name("manufacturer_id"))).SelectByValue("1");

            //Keywords
            driver.FindElement(By.Name("keywords")).SendKeys("Duck");

            //Short Description
            driver.FindElement(By.CssSelector("[name^=short_description]")).SendKeys("name");

            //Descripton
            driver.FindElement(By.ClassName("trumbowyg-editor")).SendKeys("Black Duck Black Duck Black Duck Black Duck Black Duck Black Duck Black Duck Black Duck");

            //Head Title
            driver.FindElement(By.CssSelector("[name^=head_title]")).SendKeys("Black Duck");
            //Thread.Sleep(10000);

            //Lap Prices
            driver.FindElement(By.CssSelector(".tabs li:nth-child(4)")).Click();

            //Purchase Price
            IWebElement purchase = driver.FindElement(By.Name("purchase_price"));
            purchase.Clear();
            purchase.SendKeys("10,00");

            //Type of currency
            new SelectElement(driver.FindElement(By.Name("purchase_price_currency_code"))).SelectByValue("USD");

            //Price
            driver.FindElement(By.CssSelector("[name = 'prices[USD]']")).SendKeys("20.00");

            //Save Product
            driver.FindElement(By.CssSelector("button[name = save]")).Click();

            //Menu Catalog
            driver.FindElement(By.CssSelector("#app-:nth-child(2)>a")).Click();

            //Checking product was added to the system
            driver.FindElement(By.CssSelector(".dataTable tr:nth-child(3) a")).Click();
            Thread.Sleep(2000);
            IWebElement table = driver.FindElement(By.CssSelector("table.dataTable"));
            IList<IWebElement> rows = table.FindElements(By.CssSelector("tr.row"));

            foreach (IWebElement row in rows)
            {
                IList<IWebElement> cells = row.FindElements(By.TagName("td"));
                string product = cells[2].Text;
                if (product == name)
                {
                    Console.WriteLine("Product: " + product + " correctly added to the system");
                }
            }

        }

//-------------------------------------------------------------------------------------------------------
//WORK 13
//-------------------------------------------------------------------------------------------------------
        bool AreElementsPresent(IWebDriver driver, By locator)
        {
            return driver.FindElements(locator).Count > 0;
        }

        int numberDucks = 3;

        [Test]
        public void Test13()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Navigate().GoToUrl("http://localhost/litecart");

            for (int i = 1; i <= numberDucks; i++)
            {
                //Choice Duck
                driver.FindElement(By.CssSelector("#box-latest-products li:nth-child(" + i + ")>a.link")).Click();

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
               
                //Waiting for the new number of products in a cart
                wait.Until(ExpectedConditions.TextToBePresentInElement(amountGoods, "" + i + ""));
                amountGoods = driver.FindElement(By.CssSelector("#cart-wrapper .quantity"));
                Console.WriteLine("Number of ducks in a cart: " + amountGoods.Text);
                //Checking the number of products in a cart with a number of added
                NUnit.Framework.Assert.IsTrue(amountGoods.Text.Equals("" + i + ""));

                //Go to Main Page
                driver.FindElement(By.CssSelector(".fa.fa-home")).Click();
            }

            //Opening cart
            driver.FindElement(By.CssSelector("#cart .link")).Click();

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

                IWebElement lastRow = driver.FindElement(By.CssSelector(".dataTable.rounded-corners tr:nth-child("+(i+1)+") td.item"));
                //Wait as the last line of the product will disappear from the table
                wait.Until(ExpectedConditions.StalenessOf(lastRow));
            }

            //Wait until the table disappears
            wait.Until(ExpectedConditions.StalenessOf(tableCart));

            //Go to Main Page
            driver.FindElement(By.CssSelector(".fa.fa-home")).Click();
            //Thread.Sleep(10000);
        }

        //-------------------------------------------------------------------------------------------------------
        //WORK 14
        //-------------------------------------------------------------------------------------------------------
        [Test]
        public void Test14()
        {
            //Login to shop
            driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            //Go to Page Countries
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=countries&doc=countries");

            //Add New Country
            driver.FindElement(By.ClassName("button")).Click();

            //Select links on the page Add New Country
            IList<IWebElement> links = driver.FindElements(By.CssSelector(".fa.fa-external-link"));

            foreach (IWebElement link in links)
            {

                string curentWindow = driver.CurrentWindowHandle;
                ICollection<string> windows = driver.WindowHandles;
                int numberWindows = windows.Count;
                //Select icon with arrow
                link.Click();

            Start:
                windows = driver.WindowHandles;
                int numberWindowsNow = windows.Count;

                if (numberWindows == numberWindowsNow)
                {
                    Thread.Sleep(2000);
                    goto Start;
                }

                foreach (string window in windows)
                {
                    if (window != curentWindow)
                    {
                        driver.SwitchTo().Window(window);
                        driver.Close();
                    }
                }

                driver.SwitchTo().Window(curentWindow);
            }
        }

//-------------------------------------------------------------------------------------------------------
//WORK 17
//-------------------------------------------------------------------------------------------------------
        [Test]
        public void Test17()
        {
            //Login to shop
            driver.Navigate().GoToUrl("http://localhost/litecart/admin");
            
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            //Go to Product Page
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1");
            Thread.Sleep(2000);
            
            for (int i=5;i<=9;i++)
            {
                driver.FindElement(By.CssSelector(".dataTable tr.row:nth-child("+i+") a:nth-child(1)")).Click();
                Thread.Sleep(1000);
                string titlePage = driver.Title;
                
                IList<LogEntry> logs = driver.Manage().Logs.GetLog("browser");
                if (logs.Count != 0)
                {
                    Console.WriteLine("Page " + titlePage + " contains the following errors:");
                    foreach (LogEntry log in logs)
                    {
                        Console.WriteLine(log);
                    }
                }
                else
                {
                    Console.WriteLine("There are no errors on the page " + titlePage);
                }

                driver.Navigate().Back();
                Thread.Sleep(2000);
             }
        }
        [Test]
        public void Test18()
        {
            //Login to shop
            driver.Navigate().GoToUrl("http://onet.pl");
            string titlePage = driver.Title;

           
            IList<LogEntry> logs = driver.Manage().Logs.GetLog("browser");
            if (logs.Count != 0)
            {
                Console.WriteLine("Page " + titlePage + " contains the following errors:");
                foreach (LogEntry log in logs)
                {
                    Console.WriteLine(log);
                }
            }
            else
            {
                Console.WriteLine("There are no errors on the page " + titlePage);
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
