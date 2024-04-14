using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace TestProject1;
    
public class SeleniumTests
{
    [Test]
    public void SeleniumTest()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");

        var driver = new ChromeDriver();
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");

        Thread.Sleep(4000);

        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("User");

        var password = driver.FindElement(By.Name("Password"));
        password.SendKeys("1q2w3e4r%T");

        Thread.Sleep(4000);

        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
        
        Thread.Sleep(4000);

        var currentUrl = driver.Url;
        Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news");
        
        driver.Quit();
    }
} 