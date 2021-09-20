using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace MMMTest
{
    class Login
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
        public void userLoginTest()
        {
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
        }

        [TearDown]
        public void quit_Browser()
        {
            driver.Quit();
        }
    }
}