using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Lecture09
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver("C:\\Users\\martin.montemayor\\source");
            driver.Manage().Window.Maximize();

            driver.Url = "https://www.facebook.com/";

            IWebElement email = driver.FindElement(By.CssSelector("[name = 'email']"));
            IWebElement password = driver.FindElement(By.Id("pass"));
            IWebElement login = driver.FindElement(By.Name("login"));

            email.SendKeys("This is a test");
            password.SendKeys("1234Test");
            login.Click();

            Console.WriteLine("Test Passed");
        }
    }
}
