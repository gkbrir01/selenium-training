using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_example.pages
{
    internal class ProductPage : Page
    {
        public ProductPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[name = 'options[Size]']")]
        IList<IWebElement> selectSizeField;
        [FindsBy(How = How.CssSelector, Using = "#cart-wrapper .quantity")]
        internal IWebElement amountGoods;
        [FindsBy(How = How.Name, Using = "add_cart_product")]
        internal IWebElement addDuckToCart;
        [FindsBy(How = How.CssSelector, Using = ".fa.fa-home")]
        internal IWebElement toMainPage;
        
        [FindsBy(How = How.Name, Using = "create_account")]
        internal IWebElement CreateAccountButton;

        bool AreElementsPresent(IWebDriver driver, IList<IWebElement> elements)
        {
            return elements.Count > 0;
        }

        internal void SelectSize(string sizeDuck)
        {
            bool size = AreElementsPresent(driver, selectSizeField);
            if (size)
            {
                new SelectElement(selectSizeField[0]).SelectByValue(sizeDuck);
            }
        }

        internal void GoToMainPage()
        {
            toMainPage.Clear();
        }

        internal string GetAmountGoods()
        {
            return amountGoods.Text;
        }

        internal void AddDuckToCart(string sizeDuck)
        {
            SelectSize(sizeDuck);
            addDuckToCart.Click(); 
        }

    }
}
