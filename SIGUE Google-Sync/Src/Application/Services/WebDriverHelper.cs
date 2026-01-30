namespace GMapsSync.Src.Application.Services;

#nullable enable

using System;

using GMapsSync.Src.Core;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

public class WebDriverHelper
{
    private static IWebDriver? _driver;

    public IWebDriver Driver => _driver!;

    public Browser BrowserType;

    public string DriverPath;

    public WebDriverHelper(Browser browser, string path)
    {
        if (_driver is null)
        {
            _driver = this.CreateDriver(browser, path);
        }

        this.BrowserType = browser;

        this.DriverPath = path;
    }

    public static void CloseAll()
    {
        if (_driver is null) return;
        _driver.Quit();
        _driver = null;
    }

    private IWebDriver CreateDriver(Browser browser, string path) => browser switch
    {
        Browser.Chrome => this.CreateChromeDriver(path),
        Browser.Firefox => this.CreateFirefoxDriver(path),
        _ => throw new NotSupportedException($"Browser {browser} is not supported.")
    };

    private ChromeDriver CreateChromeDriver(string path)
    {
        var service = ChromeDriverService.CreateDefaultService(path);
        var options = new ChromeOptions();

        service.HideCommandPromptWindow = true;
        options.AddExcludedArgument("enable-automation");
        options.AddArgument("--disable-infobars");
        options.AddArgument("--app=https://www.google.com/maps");
        options.AddArgument("--log-level=3");
        _driver?.Dispose();
        return new(service, options);
    }

    private FirefoxDriver CreateFirefoxDriver(string path)
    {
        var service = FirefoxDriverService.CreateDefaultService(path);
        var options = new FirefoxOptions();

        service.HideCommandPromptWindow = true;
        options.AddArgument("--disable-infobars");
        _driver?.Dispose();
        return new(service, options);
    }

    public void GoToUrl(string url)
    {
        try
        {
            _driver?.Navigate().GoToUrl(url);
        }
        catch (WebDriverException)
        {
            _driver = this.CreateDriver(this.BrowserType, this.DriverPath);
            this.GoToUrl(url);
        }
    }

    public string GetTitle()
    {
        try
        {
            return _driver?.Title ?? string.Empty;
        }
        catch (WebDriverException)
        {
            _driver = this.CreateDriver(this.BrowserType, this.DriverPath);
            return this.GetTitle();
        }
    }

    public string GetCurrentUrl()
    {
        try
        {
            return _driver?.Url ?? string.Empty;
        }
        catch (WebDriverException)
        {
            _driver = this.CreateDriver(this.BrowserType, this.DriverPath);
            return this.GetCurrentUrl();
        }
    }

    public void Refresh()
    {
        try
        {
            _driver?.Navigate().Refresh();
        }
        catch (WebDriverException)
        {
            _driver = this.CreateDriver(this.BrowserType, this.DriverPath);
            _driver?.Navigate().Refresh();
        }
    }

    public void Back()
    {
        try
        {
            _driver?.Navigate().Back();
        }
        catch (WebDriverException)
        {
            _driver = this.CreateDriver(this.BrowserType, this.DriverPath);
            _driver?.Navigate().Back();
        }
    }

    public void Forward()
    {
        try
        {
            _driver?.Navigate().Forward();
        }
        catch (WebDriverException)
        {
            _driver = this.CreateDriver(this.BrowserType, this.DriverPath);
            _driver?.Navigate().Forward();
        }
    }

    public void Close()
    {
        _driver?.Close();
    }

    public void Quit()
    {
        _driver?.Quit();
        _driver = null;
    }
}
