using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace MMMTest
{
    class addingItemToBasket
    {

        ChromeDriver driver = new ChromeDriver(@"C:\Program Files (x86)\Google\Chrome\Application");

        [SetUp]
        public void navigateToWebpage()
        {
            // Navigate to Website
            driver.Navigate().GoToUrl("http://cust.themodernmilkman.co.uk/");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void addingItemToBasketTest()
        {
            //Random randomGenerator = new Random();
            //int randomInt = randomGenerator.Next(10000000);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            //Check Existing User Can Login
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/nav/div/div/div[2]/ul/li[1]/a/img")).Click();
            driver.FindElement(By.XPath("//*[@id='phoneNo']")).SendKeys("07912124556");
            driver.FindElement(By.XPath("//*[@id='password']")).SendKeys("Password_1");
            driver.FindElement(By.XPath("//*[@id='checkLogin']")).Click();
            IWebElement signInText = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/section[2]/div/div[1]/div[3]/div[1]/h4")));
            string headerText = driver.FindElement(By.XPath("/html/body/section[2]/div/div[1]/div[1]/h1")).Text;
            NUnit.Framework.Assert.IsTrue(headerText.Contains("HI JAS"));

            //Add Product To Basket
            driver.FindElement(By.XPath("/html/body/section[2]/div/div[1]/div[2]/div/div[2]/div/div/div[1]/a/img")).Click();
            IWebElement waitForProductsToLoad = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@id='producs_list']/li[1]/div[2]/h4")));
            string wholeMilkSilverTop = driver.FindElement(By.XPath("//*[@id='producs_list']/li[1]/div[2]/h4")).Text;
            NUnit.Framework.Assert.IsTrue(wholeMilkSilverTop.Contains("Whole milk (Silver top)"));
            driver.FindElement(By.XPath("//*[@id='producs_list']/li[1]/div[2]/a/button")).Click();
            IWebElement orderDateScreen = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@id='startPauseDate1']")));
            driver.FindElement(By.XPath("//*[@id='addToCart']/div[1]/ul/li[1]/div/input[3]")).Click();
            driver.FindElement(By.XPath("//*[@id='startPauseDate1']")).Click();
            driver.FindElement(By.XPath("/html/body/div[8]/div[1]/table/thead/tr[2]/th[3]")).Click();
            driver.FindElement(By.XPath("/html/body/div[8]/div[1]/table/tbody/tr[4]/td[2]")).Click();
            driver.FindElement(By.XPath("//*[@id='addSubscription']")).Click();

            //Assert Product In Basket
            IWebElement waitForBasketToLoad = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@id='drawer-1-title']")));
            string basketHeader = driver.FindElement(By.XPath("//*[@id='drawer-1-title']")).Text;
            NUnit.Framework.Assert.IsTrue(basketHeader.Contains("Basket"));
            IWebElement waitBasketForProductsToLoad = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@id='drawer1']/div/div[2]/div[1]")));
            Thread.Sleep(3000);
            string wholeMilkSilverTopBasket = driver.FindElement(By.XPath("//*[contains(@id,'item_2212')]/div/div[2]/div/div[1]/div[1]/h4")).Text;
            NUnit.Framework.Assert.IsTrue(wholeMilkSilverTop.Contains("Whole milk (Silver top)"));
            
            //Remove Item From Basket
            driver.FindElement(By.XPath("//*[contains(@id,'item_2212')]/div/div[2]/div/div[1]/div[2]/a")).Click();
            IWebElement waitForPopUpToLoad = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/div[9]/div/div[3]/button[1]")));
            driver.FindElement(By.XPath("/html/body/div[9]/div/div[3]/button[1]")).Click();
            Thread.Sleep(5000);

        }

        [TearDown]
        public void quit_Browser()
        {
            driver.Quit();
        }
    }
}