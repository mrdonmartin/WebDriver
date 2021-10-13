using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.ObjectModel;


namespace WebDriver01
{
    class Lecture10
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver("C:\\Users\\martin.montemayor\\source");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Url = "https://www.google.com/";

            IWebElement searchBox = driver.FindElement(By.CssSelector("[name = 'q']"));
            searchBox.SendKeys("This is a test");
            searchBox.SendKeys(Keys.Enter);

            ReadOnlyCollection<IWebElement> allH3Elements = driver.FindElements(By.TagName("h3"));
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(allH3Elements[i].Text);

            }


            Console.WriteLine("Test Passed");
            driver.Quit();
        }
    }
}
