using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace MMMTest
{
    class updateEmail
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
        public void updateEmailTest()
        {
            //A Test To Check An Existing User Can Update Their Email Address

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            //Check Existing User Can Login
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/nav/div/div/div[2]/ul/li[1]/a/img")).Click();
            driver.FindElement(By.XPath("//*[@id='phoneNo']")).SendKeys("07577929156");
            driver.FindElement(By.XPath("//*[@id='password']")).SendKeys("Password_1");
            driver.FindElement(By.XPath("//*[@id='checkLogin']")).Click();
            IWebElement signInText = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/section[2]/div/div[1]/div[3]/div[1]/h4")));
            string headerText = driver.FindElement(By.XPath("/html/body/section[2]/div/div[1]/div[1]/h1")).Text;
            NUnit.Framework.Assert.IsTrue(headerText.Contains("HI JAS"));

            //Update Email
            driver.FindElement(By.XPath("/html/body/nav/div/div/div[2]/ul/li[1]/a/img")).Click();
            driver.FindElement(By.XPath("/html/body/nav/div/div/div[2]/ul/li[1]/ul/a[5]/li")).Click();
            driver.FindElement(By.XPath("/html/body/section[1]/div/div[2]/div[2]/div/ul/li[1]/a[2]")).Click();
            driver.FindElement(By.XPath("//*[@id='email']")).Clear();
            driver.FindElement(By.XPath("//*[@id='email']")).SendKeys("updated@hotmail.com");
            driver.FindElement(By.XPath("//*[@id='update_profile']")).Click();
            IWebElement popUpWindow = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@class='swal2-title']")));
            string success = driver.FindElement(By.XPath("//*[@class='swal2-title']")).Text;
            NUnit.Framework.Assert.IsTrue(success.Contains("Success"));
            driver.FindElement(By.XPath("/html/body/div[6]/div/div[3]/button[1]")).Click();

            //revert changes
            IWebElement accountDetailsArrow = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/section[1]/div/div[2]/div[2]/div/ul/li[1]/a[2]")));
            driver.FindElement(By.XPath("/html/body/section[1]/div/div[2]/div[2]/div/ul/li[1]/a[2]")).Click();
            driver.FindElement(By.XPath("//*[@id='email']")).Clear();
            driver.FindElement(By.XPath("//*[@id='email']")).SendKeys("test@hotmail.com");
            driver.FindElement(By.XPath("//*[@id='update_profile']")).Click();
            IWebElement popUpWindowRevert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@class='swal2-title']")));
            NUnit.Framework.Assert.IsTrue(success.Contains("Success"));
            driver.FindElement(By.XPath("/html/body/div[6]/div/div[3]/button[1]")).Click();
        }

        [TearDown]
        public void quit_Browser()
        {
            driver.Quit();
        }
    }
}