using ArcGIS.Desktop.Core.Events;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;

using GMapsSync.Src.Core;
using Common = GMapsSync.Src.Application.UseCases.Common;

namespace GMapsSync;

internal class MainModule : Module
{
    private static MainModule _this = null;
    private static Settings _settings = Settings.Default;
    public static MainModule Current => _this ??= (MainModule)FrameworkApplication.FindModule("SIGUE_Google_Sync_Module");
    public static Settings Settings => _settings;

    protected override bool CanUnload()
    {
        return true;
    }

    protected override bool Initialize()
    {
        ProjectOpenedEvent.Subscribe(this.OnProjectOpened);
        return base.Initialize();
    }

    protected override void Uninitialize()
    {
        GMapsSync.Src.Application.Services.WebDriverHelper.CloseAll(); // Ensure all WebDriver instances are closed
        base.Uninitialize();
    }

    public void OnProjectOpened(ProjectEventArgs args)
    {
        if (Common.ValidateDomain.Invoke())
        {
            FrameworkApplication.State.Activate("GMapsSync_InCompanyDomain");
            return;
        }
        FrameworkApplication.State.Deactivate("GMapsSync_InCompanyDomain");
    }
}
