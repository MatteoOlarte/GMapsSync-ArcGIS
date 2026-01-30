#nullable enable

using System;
using System.Text.RegularExpressions;

using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Mapping;

using GMapsSync.Src.Application.Services;

using UseCases = GMapsSync.Src.Application.UseCases;

namespace GMapsSync.Src.Presentation.ViewModel;

public class GoogleMapsArcGISSyncViewModel
{
    private static readonly Regex Pattern = new Regex(@"@(-?\d+\.{1}\d+),(-?\d+\.{1}\d+),(\d+\.?\d*)[z|m]", RegexOptions.Compiled);
    private readonly WebDriverService driver;


    public GoogleMapsArcGISSyncViewModel()
    {
        driver = UseCases.WebDriver.GetWebDriver.Invoke();
    }

    public void SyncToArcGIS()
    {
        var url = this.driver.GetCurrentUrl();
        var (lat, lon, z) = GetUrlArgs(url);
        var view = MapView.Active;
        var wgs84 = MapPointBuilderEx.CreateMapPoint(lon, lat, SpatialReferences.WGS84);
        var mapSR = view!.Map.SpatialReference;
        MapPoint targetPoint;

        if (mapSR.Wkid != SpatialReferences.WGS84.Wkid)
        {
            targetPoint = (GeometryEngine.Instance.Project(wgs84, mapSR) as MapPoint)!;
        }
        else
        {
            targetPoint = wgs84;
        }

        if (targetPoint is not null)
        {
            var scale = CalculateScaleFactor(z);
            var cam = view.Camera;

            cam.Scale = Math.Round(scale);
            view.Camera.Scale = scale;
            view.ZoomToAsync(cam);
            view.PanTo(targetPoint);
            return;
        }
        MessageBox.Show("No se pudieron transformar las coordenadas.", "Error");
    }


    private (double, double, double) GetUrlArgs(string url)
    {
        var match = Pattern.Match(url);
        var lat = double.Parse(match.Groups[1].Value, System.Globalization.CultureInfo.InvariantCulture);
        var lon = double.Parse(match.Groups[2].Value, System.Globalization.CultureInfo.InvariantCulture);
        var z = double.Parse(match.Groups[3].Value, System.Globalization.CultureInfo.InvariantCulture);
        return (lat, lon, z);
    }

    private double CalculateScaleFactor(double x)
    {
        return 591657550.45 * Math.Pow(Math.E, -0.69 * x);
    }

    [Obsolete]
    private double GetScaleFromZoomLevel(double value) => value switch
    {
        0.0 => 591657550.5,
        1.0 => 295828775.3,
        2.0 => 147914387.6,
        3.0 => 73957193.82,
        4.0 => 36978596.91,
        5.0 => 18489298.45,
        6.0 => 9244649.227,
        7.0 => 4622324.614,
        8.0 => 2311162.307,
        9.0 => 1155581.153,
        10.0 => 577790.5767,
        11.0 => 288895.2883,
        12.0 => 144447.6442,
        13.0 => 72223.82209,
        14.0 => 36111.91104,
        15.0 => 18055.95552,
        16.0 => 9027.977761,
        17.0 => 4513.98888,
        18.0 => 2256.99444,
        19.0 => 1128.49722,
        20.0 => 564.248611,
        21.0 => 282.124305,
        _ => 282.124305
    };
}
