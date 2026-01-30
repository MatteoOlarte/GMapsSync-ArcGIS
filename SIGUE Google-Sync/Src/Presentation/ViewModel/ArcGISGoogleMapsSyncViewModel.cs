#nullable enable

using System;

using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Mapping;

using GMapsSync.Src.Application.Ext;
using GMapsSync.Src.Application.Services;

using UseCases = GMapsSync.Src.Application.UseCases;

namespace GMapsSync.Src.Presentation.ViewModel;

internal class ArcGISGoogleMapsSyncViewModel
{
    private readonly WebDriverService driver;

    private readonly string url;

    public ArcGISGoogleMapsSyncViewModel()
    {
        this.driver = UseCases.WebDriver.GetWebDriver.Invoke();
        this.url = "https://www.google.com/maps";
    }

    public void SyncGoogleMapsView()
    {
        var mapView = MapView.Active;

        if (mapView is not null)
        {
            // Obtener la extensión actual
            var extent = mapView.Extent;

            // Obtener el sistema de coordenadas WGS84
            var wgs84Extent = extent.ToWGS84();

            if (wgs84Extent is not null)
            {
                var centerLat = ((wgs84Extent.YMin + wgs84Extent.YMax) / 2).ToString(System.Globalization.CultureInfo.InvariantCulture);
                var centerLon = ((wgs84Extent.XMin + wgs84Extent.XMax) / 2).ToString(System.Globalization.CultureInfo.InvariantCulture);
                var zoom = this.CalculateScaleFactor(mapView.Camera.Scale).ToString(System.Globalization.CultureInfo.InvariantCulture);
                var args = $"{centerLat},{centerLon},{zoom}z";
                this.driver.GoToUrl($"{this.url}/@{args}");
                return;
            }
        }
        MessageBox.Show("No hay una vista de mapa activa o no se pudo transformar la extensión a WGS84.", "Error");
    }

    private double CalculateScaleFactor(double x)
    {
        return -1.443 * Math.Log(x) + 29.14;
    }

    [Obsolete]
    private int GetZoomLevelFromScale(double scale)
    {
        if (scale > 100000000) return 3;
        if (scale > 50000000) return 4;
        if (scale > 25000000) return 5;
        if (scale > 10000000) return 6;
        if (scale > 5000000) return 7;
        if (scale > 2500000) return 8;
        if (scale > 1000000) return 9;
        if (scale > 500000) return 10;
        if (scale > 250000) return 11;
        if (scale > 100000) return 12;
        if (scale > 50000) return 13;
        if (scale > 25000) return 14;
        if (scale > 10000) return 15;
        if (scale > 5000) return 16;
        if (scale > 2500) return 18;
        if (scale > 1000) return 18;
        if (scale > 500) return 19;
        return 20;
    }
}
