using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace csharp_example
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
            driver.Manage().Window.Maximize();
            mainPage = new MainPage(driver);
            productPage = new ProductPage(driver);
            cartPage = new CartPage(driver);
        }

        public void Quit()
        {
            driver.Quit();
        }

        public string GetAmountGoods()
        {
            return productPage.amountGoods.Text;
        }

        public IWebElement GetAmountGoodsLocator()
        {
            return productPage.amountGoods;
        }


        internal void choiceAndAddDuckToCartAndCheckCart(int numberDucks)
        {
            mainPage.Open();
            
            for (int i = 1; i <= numberDucks; i++)
            {
                //Choice Duck
                mainPage.ChoiceDuck(i);
                
                //Add Duck to cart
                productPage.AddDuckToCart("Medium");
                
                //Waiting for the new number of products in a cart
                productPage.CheckAddToCart(i);

                productPage.GoToMainPage();
            }
        }
        
        internal void RemoveDuckAndCheck(int numberDucks)
        {
           mainPage.GoToCart();

            for (int i = numberDucks; i > 0; i--)
            {
                cartPage.RemoveDuckAndWait();
                cartPage.LastRecordDisappearInTableWait(i);
            }

            cartPage.GoToMainPage();
        }

    }
}
