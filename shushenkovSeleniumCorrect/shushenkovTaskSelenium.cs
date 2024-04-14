using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace shushenkovSeleniumCorrect;
    
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

        driver.FindElement(By.Name("Password"));
        login.SendKeys("1q2w3e4r%T");

        Thread.Sleep(3000);

        var enter = driver.FindElement(By.Name("Button"));
        enter.Click();

        var currentUrl = driver.Url;
        Assert.That(currentUrl == "https://staff-testing.testkontur.ru");
        
        driver.Quit();
    }
}