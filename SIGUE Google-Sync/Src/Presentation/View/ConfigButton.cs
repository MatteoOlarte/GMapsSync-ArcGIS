using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;

namespace GMapsSync.Src.Presentation.View;

internal class ConfigButton : Button
{
    protected override void OnClick()
    {
        var data = new object[] { "Configuración" };

        if (!PropertySheet.IsVisible)
        {
            PropertySheet.ShowDialog("GMapsSync_ConfigSheet", "GMapsSync_ConfigPage", data);
        }
    }
}
