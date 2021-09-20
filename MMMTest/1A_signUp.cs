using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace MMMTest
{
    class signUp
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
        public void signUpTest()
        {
            //A Test To Check A User Can Successfully Sign Up

            Random randomGenerator = new Random();
            int randomInt = randomGenerator.Next(10000000);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            //Check Valid Postcode
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[@id='postcode']")).SendKeys("LS208JN");
            driver.FindElement(By.XPath("//*[@id='checkPostcode5']")).Click();

            //Wait For Element To Be Visible     
            IWebElement text = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/section[1]/h4")));
            
            //Get Text Of Header
            string actualvalue = driver.FindElement(By.XPath("/html/body/section[1]/h4")).Text;

            //Sign up
            if(actualvalue == "We deliver to your neighbourhood.")
            {
                driver.FindElement(By.XPath("//*[@id='forename']")).SendKeys("Jas");
                driver.FindElement(By.XPath("//*[@id='surname']")).SendKeys("Sidhu");
                driver.FindElement(By.XPath("//*[@id='email']")).SendKeys("testing@hotmail.com");
                driver.FindElement(By.XPath("//*[@id='phoneNo']")).SendKeys("07"+ randomInt + "560");
                driver.FindElement(By.XPath("//*[@id='password']")).SendKeys("Password_1");               
                var hearAboutUs = driver.FindElement(By.XPath("//*[@id='heardFrom']"));
                var selectElement = new SelectElement(hearAboutUs);
                selectElement.SelectByValue("Friends or family");             
                driver.FindElement(By.XPath("//*[@id='signup']/div[5]/div[1]/div/label")).Click();
                driver.FindElement(By.XPath("//*[@id='signup']/div[5]/div[2]/div/label")).Click();
                driver.SwitchTo().Window(driver.WindowHandles[1]).Close();
                driver.SwitchTo().Window(driver.WindowHandles[0]);
                driver.FindElement(By.XPath("//*[@id='signup']/div[6]/button")).Click();             
                IWebElement signInText = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/section[2]/div/div[1]/div[3]/div[1]/h4")));                
                string headerText = driver.FindElement(By.XPath("/html/body/section[2]/div/div[1]/div[1]/h1")).Text;
                NUnit.Framework.Assert.IsTrue(headerText.Contains("HI JAS"));
            }
        }

        [TearDown]
        public void quit_Browser()
        {
            driver.Quit();
        }
    }
}