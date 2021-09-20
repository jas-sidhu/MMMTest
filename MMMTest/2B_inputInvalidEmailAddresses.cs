using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace MMMTest
{
    class inputInvalidEmailAddresses
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
        public void inputInvalidEmailAddressesTest()
        {
            //A Test To Check An Existing User Cannont Input Invalid Email Addresses

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            //Existing User Can Login
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("/html/body/nav/div/div/div[2]/ul/li[1]/a/img")).Click();
            driver.FindElement(By.XPath("//*[@id='phoneNo']")).SendKeys("07577929156");
            driver.FindElement(By.XPath("//*[@id='password']")).SendKeys("Password_1");
            driver.FindElement(By.XPath("//*[@id='checkLogin']")).Click();
            IWebElement signInText = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/section[2]/div/div[1]/div[3]/div[1]/h4")));
            string headerText = driver.FindElement(By.XPath("/html/body/section[2]/div/div[1]/div[1]/h1")).Text;
            NUnit.Framework.Assert.IsTrue(headerText.Contains("HI JAS"));

            //Navigate To Accounts Page
            driver.FindElement(By.XPath("/html/body/nav/div/div/div[2]/ul/li[1]/a/img")).Click();
            driver.FindElement(By.XPath("/html/body/nav/div/div/div[2]/ul/li[1]/ul/a[5]/li")).Click();
            driver.FindElement(By.XPath("/html/body/section[1]/div/div[2]/div[2]/div/ul/li[1]/a[2]")).Click();
            
            String[] emails = new string[]
            {
                "abc-@mail.com",
                "abc..def@mail.com",
                ".abc@mail.com",
                "abc#def@mail.com"
            };
          
            foreach (String email in emails)
            {
                driver.FindElement(By.XPath("//*[@id='email']")).Clear();
                driver.FindElement(By.XPath("//*[@id='email']")).SendKeys(email);
                driver.FindElement(By.XPath("//*[@id='update_profile']")).Click();
                string invalidEmailText = driver.FindElement(By.XPath("//*[@id='error-otp']")).Text;
                NUnit.Framework.Assert.IsTrue(invalidEmailText.Contains("Please enter the valid email"));
            }
        }

        [TearDown]
        public void quit_Browser()
        {
            driver.Quit();
        }
    }
}


