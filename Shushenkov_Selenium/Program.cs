using System.Configuration;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace Shushenkov_Selenium

{
    internal class Program
    {
        [Test]
        public static void Main(string[] args)
        {
            var options = new ChromeOptions();
            options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");
            
            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");

            //Thread.Sleep(4000);
            
            var login = driver.findElement(By.Id("Username"));
            login.SendKeys("User");
            
            var password = driver.findElement(By.Name("Password"));
            login.SendKeys("1q2w3e4r%T");
            
            Thread.Sleep(3000);

            var enter = driver.findElement(By.Name("Button"));
            enter.click();

            var currentUrl = driver.url;
            Assert.That(currentUrl == "https://staff-testing.testkontur.ru");
            
            driver.Quit();

        }
    }
}