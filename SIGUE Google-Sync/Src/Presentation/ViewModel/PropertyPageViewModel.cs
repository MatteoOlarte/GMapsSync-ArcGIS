namespace GMapsSync.Src.Presentation.ViewModel;

using System;

#nullable enable

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;

using GMapsSync.Src.Application.Ext;
using GMapsSync.Src.Core;

using Microsoft.Win32;

internal class PropertyPageViewModel : Page
{
    private readonly Settings _settings;
    private string _driverPath = string.Empty;
    private string _selectedBrowser = string.Empty;
    private ObservableCollection<string> _browsers = new();

    public ObservableCollection<string> Browsers
    {
        get => _browsers;
        set => SetProperty(ref _browsers, value);
    }

    public string SelectedBrowser
    {
        get => _selectedBrowser;
        set { if (SetProperty(ref _selectedBrowser, value)) IsModified = true; }
    }

    public string DriverPath
    {
        get => _driverPath;
        set { if (SetProperty(ref _driverPath, value)) IsModified = true; }
    }

    public ICommand BrowseDriverCommand { get; }

    public PropertyPageViewModel()
    {
        _settings = MainModule.Settings;
        BrowseDriverCommand = new RelayCommand(OnBrowseDriverExecute);
        Browsers = new ObservableCollection<string>(Enum.GetValues(typeof(Browser)).Cast<Browser>().Select(it => it.Value()));
        LoadSettings();
    }

    protected override Task CommitAsync()
    {
        _settings.web_browser = SelectedBrowser;
        _settings.driver_path = DriverPath;
        _settings.Save();
        System.Diagnostics.Debug.WriteLine($"Saving settings: browser={SelectedBrowser}, driver={DriverPath}");
        return Task.FromResult(0);
    }

    protected override Task InitializeAsync()
    {
        LoadSettings();
        return Task.FromResult(true);
    }

    protected override void Uninitialize()
    {
    }

    private void LoadSettings()
    {
        string? browser = _settings.web_browser;
        string? driver = _settings.driver_path;

        System.Diagnostics.Debug.WriteLine($"Loaded settings: browser={browser}, driver={driver}");

        this.SelectedBrowser = Browser.Chrome.Value();

        if (browser.IsNotNullOrEmpty() && this.Browsers.Contains(browser))
        {
            this.SelectedBrowser = browser;
        }

        if (driver.IsNotNullOrEmpty())
        {
            this.DriverPath = driver;
        }
    }

    private void OnBrowseDriverExecute()
    {
        var filter = new BrowseProjectFilter("esri_browseDialogFilters_executables");

        var dlg = new OpenItemDialog
        {
            Title = "Seleccionar Driver",
            BrowseFilter = filter,
            MultiSelect = false,
            InitialLocation = Project.Current?.HomeFolderPath
        };

        var ok = dlg.ShowDialog();
        
        if (ok == true && dlg.Items != null && dlg.Items.Any())
        {
            var item = dlg.Items.First();
            DriverPath = item.Path;
        }
    }
}


internal class PropertyPage_ShowButton : Button
{
    protected override void OnClick()
    {
        object[] data = ["Page UI content"];

        if (PropertySheet.IsVisible) return;

        PropertySheet.ShowDialog("SIGUE_Google-Sync_Images_PropertySheet", "SIGUE_Google-Sync_Images_PropertyPage", data);
    }
}
