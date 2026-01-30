namespace GMapsSync.Src.Application.Builders;

#nullable enable

using System;
using System.Collections.Generic;

using GMapsSync.Src.Core;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

public interface IWebDriverBuilder
{
    IWebDriverBuilder WithHeadless(bool headless = true);
    IWebDriverBuilder WithStartUrl(string url);
    IWebDriverBuilder WithWindowSize(int width, int height);
    IWebDriverBuilder WithArgument(string argument);
    IWebDriverBuilder DisableAutomation();
    IWebDriverBuilder DisableInfobars();
    IWebDriverBuilder WithLogLevel(int level);
    IWebDriver Build();
}

public abstract class WebDriverBuilderBase : IWebDriverBuilder
{
    protected bool _headless;
    protected string? _startUrl;
    protected (int width, int height)? _windowSize;
    protected List<string> _arguments = new();
    protected bool _disableAutomation;
    protected bool _disableInfobars;
    protected int _logLevel = 3;

    public IWebDriverBuilder WithHeadless(bool headless = true)
    {
        _headless = headless;
        return this;
    }

    public IWebDriverBuilder WithStartUrl(string url)
    {
        _startUrl = url;
        return this;
    }

    public IWebDriverBuilder WithWindowSize(int width, int height)
    {
        _windowSize = (width, height);
        return this;
    }

    public IWebDriverBuilder WithArgument(string argument)
    {
        _arguments.Add(argument);
        return this;
    }

    public IWebDriverBuilder DisableAutomation()
    {
        _disableAutomation = true;
        return this;
    }

    public IWebDriverBuilder DisableInfobars()
    {
        _disableInfobars = true;
        return this;
    }

    public IWebDriverBuilder WithLogLevel(int level)
    {
        _logLevel = level;
        return this;
    }

    public abstract IWebDriver Build();
}

public class ChromeDriverBuilder : WebDriverBuilderBase
{
    public override IWebDriver Build()
    {
        var options = new ChromeOptions();

        if (_disableAutomation)
        {
            options.AddExcludedArgument("enable-automation");
        }

        if (_disableInfobars)
        {
            options.AddArgument("--disable-infobars");
        }

        if (_headless)
        {
            options.AddArgument("--headless=new");
        }

        if (_startUrl != null)
        {
            options.AddArgument($"--app={_startUrl}");
        }

        if (_windowSize.HasValue)
        {
            options.AddArgument($"--window-size={_windowSize.Value.width},{_windowSize.Value.height}");
        }

        options.AddArgument($"--log-level={_logLevel}");

        foreach (var arg in _arguments)
        {
            options.AddArgument(arg);
        }

        return new ChromeDriver(options);
    }
}

public class FirefoxDriverBuilder : WebDriverBuilderBase
{
    public override IWebDriver Build()
    {
        var options = new FirefoxOptions();

        if (_disableInfobars)
        {
            options.AddArgument("--disable-infobars");
        }

        if (_headless)
        {
            options.AddArgument("--headless");
        }

        if (_windowSize.HasValue)
        {
            options.AddArgument($"--width={_windowSize.Value.width}");
            options.AddArgument($"--height={_windowSize.Value.height}");
        }

        foreach (var arg in _arguments)
        {
            options.AddArgument(arg);
        }

        var driver = new FirefoxDriver(options);

        if (_startUrl != null)
        {
            driver.Navigate().GoToUrl(_startUrl);
        }

        return driver;
    }
}


public class EdgeDriverBuilder : WebDriverBuilderBase
{
    public override IWebDriver Build()
    {
        var options = new EdgeOptions();

        if (_disableAutomation)
        {
            options.AddExcludedArgument("enable-automation");
        }

        if (_disableInfobars)
        {
            options.AddArgument("--disable-infobars");
        }

        if (_headless)
        {
            options.AddArgument("--headless=new");
        }

        if (_startUrl != null)
        {
            options.AddArgument($"--app={_startUrl}");
        }

        if (_windowSize.HasValue)
        {
            options.AddArgument($"--window-size={_windowSize.Value.width},{_windowSize.Value.height}");
        }

        options.AddArgument($"--log-level={_logLevel}");

        foreach (var arg in _arguments)
        {
            options.AddArgument(arg);
        }

        return new EdgeDriver(options);
    }
}

public static class WebDriverBuilderFactory
{
    public static IWebDriverBuilder CreateBuilder(Browser browser) => browser switch
    {
        Browser.Chrome => new ChromeDriverBuilder(),
        Browser.Firefox => new FirefoxDriverBuilder(),
        Browser.MsEdge => new EdgeDriverBuilder(),
        _ => throw new NotSupportedException($"Browser {browser} is not supported.")
    };
}