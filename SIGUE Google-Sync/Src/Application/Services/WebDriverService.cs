namespace GMapsSync.Src.Application.Services;

#nullable enable

using GMapsSync.Src.Application.Builders;
using GMapsSync.Src.Core;

using OpenQA.Selenium;

public class WebDriverService
{
    private static IWebDriver? _driver;

    public IWebDriver Driver => _driver!;

    public WebDriverService(Browser browser)
    {
        if (_driver is null)
        {
            _driver = CreateDriver(browser);
        }

        BrowserType = browser;
    }

    private IWebDriver CreateDriver(Browser browser)
    {
        _driver?.Dispose();

        return WebDriverBuilderFactory.CreateBuilder(browser).WithStartUrl("https://www.google.com/maps").Build();
    }

    public static void CloseAll()
    {
        if (_driver is null) return;
        _driver.Quit();
        _driver = null;
    }

    public void GoToUrl(string url)
    {
        try
        {
            _driver?.Navigate().GoToUrl(url);
        }
        catch (WebDriverException)
        {
            _driver = CreateDriver(BrowserType);
            GoToUrl(url);
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
            _driver = CreateDriver(BrowserType);
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
            _driver = CreateDriver(BrowserType);
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
            _driver = CreateDriver(BrowserType);
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

    public string GetTitle()
    {
        try
        {
            return _driver?.Title ?? string.Empty;
        }
        catch (WebDriverException)
        {
            _driver = CreateDriver(BrowserType);
            return GetTitle();
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
            _driver = CreateDriver(BrowserType);
            return GetCurrentUrl();
        }
    }

    public Browser BrowserType { get; private set; }

    public string Title => this.GetTitle();

    public string CurrentUrl => this.GetCurrentUrl();
}