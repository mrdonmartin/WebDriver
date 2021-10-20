using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace FinalExam
{
    [TestFixture]
    public class Tests
    {
        IWebDriver driver;
        string timeStamp = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        string username;
        string email;
        string password;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://candidatex:qa-is-cool@qa-task.backbasecloud.com";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
            Console.WriteLine("Test Data Created:");
            Console.WriteLine("Username: " + username);
            Console.WriteLine("Password: " + password);
        }

        [Test, Order(1)]
        [Category("Smoke Test")]
        public void SignUp()
        {
            username = "test" + timeStamp;
            email = username + "@test.com";
            password = "test";
            driver.FindElement(By.XPath("//a[contains(text(),'Sign up')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            driver.FindElement(By.XPath("//input[@formcontrolname='username']")).SendKeys(username);
            driver.FindElement(By.XPath("//input[@formcontrolname='email']")).SendKeys(email);
            driver.FindElement(By.XPath("//input[@formcontrolname='password']")).SendKeys(password);
            driver.FindElement(By.XPath("//button[contains(text(),'Sign up')]")).Click();
            Assert.IsTrue(driver.FindElements(By.XPath("//div[contains(text(),'No articles are here... yet.')]")).Count >= 1);
        }

        [Test, Order(2)]
        [Category("Smoke Test")]
        public void SignIn()
        {
            driver.FindElement(By.XPath("//a[contains(text(),'Sign in')]")).Click();
            driver.FindElement(By.XPath("//input[@formcontrolname='email']")).SendKeys(email);
            driver.FindElement(By.XPath("//input[@formcontrolname='password']")).SendKeys(password);
            driver.FindElement(By.XPath("//button[contains(text(),'Sign in')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            IWebElement newArticle = driver.FindElement(By.XPath("//a[contains(text(),'New Article')]"));
            Assert.AreEqual(true, newArticle.Displayed);
        }

        [Test, Order(3)]
        [Category("Smoke Test")]
        public void CreateArticle()
        {
            SignIn();
            driver.FindElement(By.XPath("//a[contains(text(),'New Article')]")).Click();
            driver.FindElement(By.XPath("//input[@formcontrolname='title']")).SendKeys("Title test" + timeStamp);
            driver.FindElement(By.XPath("//input[@formcontrolname='description']")).SendKeys("Description test");
            driver.FindElement(By.TagName("textarea")).SendKeys("Body Test");
            driver.FindElement(By.XPath("//input[@placeholder='Enter tags']")).SendKeys("tag01");
            driver.FindElement(By.XPath("//button[contains(text(),'Publish Article')]")).Click();

            IWebElement Title = driver.FindElement(By.XPath("//h1[contains(text(),'Title test')]"));
            Assert.AreEqual(true, Title.Displayed);

            IWebElement Body = driver.FindElement(By.XPath("//p[contains(text(),'Body Test')]"));
            Assert.AreEqual(true, Body.Displayed);

        }

        [Test, Order(4)]
        [Category("Smoke Test")]
        public void Logout()
        {
            SignIn();
            driver.FindElement(By.XPath("//a[contains(text(),'Settin')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            driver.FindElement(By.XPath("//button[contains(text(),'logout.')]")).Click();
            Assert.IsTrue(driver.FindElements(By.XPath("//a[contains(text(),'Sign up')]")).Count >= 1);
        }

        [SetUpFixture]
        public class AssemblySetup
        {

        }
    }
}