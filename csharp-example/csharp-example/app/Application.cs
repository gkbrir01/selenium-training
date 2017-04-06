using csharp_example.pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace csharp_example.app
{
    public class Application
    {
        private IWebDriver driver;

        private MainPage mainPage;
        private ProductPage productPage;
        private CartPage cartPage;

        public Application()
        {
            driver = new ChromeDriver();
            mainPage = new MainPage(driver);
            productPage = new ProductPage(driver);
            cartPage = new CartPage(driver);
        }
        
        public void Quit()
        {
            driver.Quit();
        }

        internal void choiceAndAddDuckToCart(int nrDuck)
        {
            mainPage.Open();
            Thread.Sleep(1000);
            
            //Choice Duck
            mainPage.ChoiceDuck(nrDuck);
             
            //Add Duck to cart
            productPage.AddDuckToCart("Madium");
            Thread.Sleep(1000);

            //Go to Main Page
            productPage.GoToMainPage();           
            Thread.Sleep(1000);
         }
    }
}
