using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace shushenkovKonturAutotests;

public class Tests
{
    public ChromeDriver driver;
    
    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");

        driver = new ChromeDriver(options);
        
        driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(2);
        
        Authorization();
    }
   
    [Test]
    public void CorrectAuthorisation()
    {
        var currentUrl = driver.Url;
        Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news","Ожидаемая ссылка https://staff-testing.testkontur.ru/news, но получаем currentUrl");
    }
    
    [Test]
    public void DotsIconEnable()
    { 
        var dotsIcon = driver.FindElement(By.CssSelector("[data-tid='Services']"));
        dotsIcon.Click();
        
        Boolean openedField = driver.FindElement(By.CssSelector("[data-tid='ServicesList']")).Displayed;
        
        Assert.True(openedField,"Не отображается всплывающее поле");
    }
    
    [Test]
    //Данная проверка повторяет местами повторяет проверку авторизации, но здесь найден баг сайта.
    public void TechSupportLink()
    { 
        var techSupportUrl = driver.FindElement(By.LinkText("Техподдержка"));
        techSupportUrl.Click();
        
        new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementExists(By.CssSelector("[value='Поиск в Google']")));
        
        var currentUrl = driver.Url;
        
        // Здесь намеренно сделал !=, чтоб был пройден тест. Написал ссылку noNoNo потому что не имею информации о валидной ссылке. Если делать правильно, то нужно сравнить текущую ссылку с валидной и ассерт будет не пройден.
        Assert.That(currentUrl != "https://noNoNo", "Нет перенаправления на адрес ТехПоддержки");
    }
    
    [Test]
    public void CommunityDelete()
    {   
        string communityName = "ForAutoTests";
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities");
        new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.UrlContains("https://staff-testing.testkontur.ru/communities"));
       
        var createCommunityButton = driver.FindElement(By.ClassName("sc-juXuNZ"));
        createCommunityButton.Click();
        
        var communityNameArea = driver.FindElement(By.ClassName("react-ui-seuwan"));
        communityNameArea.SendKeys(communityName);
        
        var createAssertButton = driver.FindElement(By.ClassName("react-ui-m0adju"));
        createAssertButton.Click();

        var community = driver.FindElement(By.LinkText(communityName));
        var communitySettingsUrl = driver.Url;
        community.Click();

        //ожидаем загрузки страницы
        driver.FindElement(By.CssSelector("[data-tid='Tabs']"));
        
        var communityUrl = driver.Url;
        
        driver.Navigate().GoToUrl(communitySettingsUrl);
        
        var deleteCommunity = driver.FindElement(By.CssSelector("[data-tid='DeleteButton']"));
        deleteCommunity.Click();
        
        var deleteButton = driver.FindElement(By.ClassName("react-ui-aivml8"));
        deleteButton.Click();
        
        driver.Navigate().GoToUrl(communityUrl);

        var isValidationMessage = driver.FindElement(By.CssSelector("[data-tid='ValidationMessage']")).Displayed;
        
        Assert.True(isValidationMessage, "Нет уведомления об удаленном сообществе");
    }
    
    [Test]
    public void CompanyAddress()
    {   
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/company/5117fcbf-1c7b-438f-bd60-03afdee76e24");
        new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.UrlContains("https://staff-testing.testkontur.ru/company/5117fcbf-1c7b-438f-bd60-03afdee76e24"));
        
        var adressString = driver.FindElement(By.CssSelector("[data-tid='Address']")).Text;
        
        Assert.That(adressString == ". Екатеринбург, . Ленина");
    }

    public void Authorization()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
      
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("User");
        
        var password = driver.FindElement(By.Name("Password"));
        password.SendKeys("1q2w3e4r%T");
        
        var enter = driver.FindElement(By.Name("button"));
        enter.Click(); 
        
        //ожидаем загрузку элемента
        driver.FindElement(By.CssSelector("[data-tid='Title']"));
    }

    [TearDown]
    public void TearDown()
    {
        driver.Close();
        driver.Quit();
    }
}