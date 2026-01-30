namespace GMapsSync.Src.Core;

#nullable enable

public enum Browser
{
    Chrome,
    Firefox,
    MsEdge
}

public static class BrowserExtensions
{
    public static Browser ToBrowser(this string displayName) => displayName switch
    {
        "Google Chrome" => Browser.Chrome,
        "Firefox" => Browser.Firefox,
        "Microsoft Edge" => Browser.MsEdge,
        _ => Browser.MsEdge
    };

    public static string Value(this Browser browser) => browser switch
    {
        Browser.Chrome => "Google Chrome",
        Browser.Firefox => "Firefox",
        Browser.MsEdge => "Microsoft Edge",
        _ => browser.ToString()
    };
}
