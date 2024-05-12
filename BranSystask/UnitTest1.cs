using System;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace LoginTest
{
    [TestFixture]
    public class LoginTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // Initialize Chrome WebDriver
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://crusader.bransys.com/");
        }

        [Test]
        public void PresenceOfLoginFields()
        {
            // Locate username and password input fields
            IWebElement usernameField = driver.FindElement(By.XPath("//*[@id=\"input-204\"]"));
            IWebElement passwordField = driver.FindElement(By.XPath("//*[@id=\"input-207\"]"));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);

            // Assert presence of login fields
            Assert.That(usernameField.Displayed && passwordField.Displayed, Is.True);
        }

        [Test]
        public void InputDataValidation()
        {
            // Locate username and password input fields
            IWebElement usernameField = driver.FindElement(By.XPath("//*[@id=\"input-204\"]"));
            IWebElement passwordField = driver.FindElement(By.XPath("//*[@id=\"input-207\"]"));

            // Input valid data values
            usernameField.SendKeys("valid_username");
            passwordField.SendKeys("valid_password");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);

            // Assert data values are entered
            Assert.That(usernameField.GetAttribute("value"), Is.EqualTo("valid_username"));
            Assert.That(passwordField.GetAttribute("value"), Is.EqualTo("valid_password"));
        }

        [Test]
        public void GenericMessagesDisplayed()
        {
            // Locate username and password input fields
            IWebElement usernameField = driver.FindElement(By.XPath("//*[@id=\"input-204\"]"));
            IWebElement passwordField = driver.FindElement(By.XPath("//*[@id=\"input-207\"]"));
            IWebElement loginButton = driver.FindElement(By.XPath("//*[@id=\"app\"]/div/div/div/div/div/div/div/div/div[2]/div[2]/form/div[2]/div[1]/button"));

            // Input invalid credentials
            usernameField.SendKeys("invalid_username");
            passwordField.SendKeys("invalid_password");
            loginButton.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);

            // Locate error message element
            IWebElement errorMessage = driver.FindElement(By.XPath("//*[@id=\"app\"]/div/div/div/div/div/div/div/div/div[2]/div[2]/form/div[1]/div[3]"));

            // Assert correct generic error message is displayed
            Assert.That(errorMessage.Text.Trim(), Is.EqualTo("Incorrect email/username or password"));

        }

        [Test]
        public void PasswordRequired()
        {
            // Locate username and password input fields
            IWebElement usernameField = driver.FindElement(By.XPath("//*[@id=\"input-204\"]"));
            IWebElement passwordField = driver.FindElement(By.XPath("//*[@id=\"input-207\"]"));

            usernameField.SendKeys("teste");
            passwordField.Click();
            usernameField.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            IWebElement requiredError = driver.FindElement(By.XPath("//*[@id=\"app\"]/div/div/div/div/div/div/div/div/div[2]/div[2]/form/div[1]/div[2]/div/div/div[2]/div/div/div"));

            Assert.That(requiredError.Text.Trim(), Is.EqualTo("Required"));

        }

        [Test]

        public void UsernameRequired()
        {
            IWebElement usernameField = driver.FindElement(By.XPath("//*[@id=\"input-204\"]"));
            IWebElement passwordField = driver.FindElement(By.XPath("//*[@id=\"input-207\"]"));

            passwordField.Click();
            usernameField.Click();
            passwordField.Click();


            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            IWebElement usernameError = driver.FindElement(By.XPath("//*[@id=\"app\"]/div/div/div/div/div/div/div/div/div[2]/div[2]/form/div[1]/div[1]/div/div/div[2]/div/div/div"));

            Assert.That(usernameError.Text.Trim(), Is.EqualTo("Required"));



        }

        [TearDown]
        public void TearDown()
        {
            // Quit WebDriver
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);
            driver.Quit();
        }

    }
}
