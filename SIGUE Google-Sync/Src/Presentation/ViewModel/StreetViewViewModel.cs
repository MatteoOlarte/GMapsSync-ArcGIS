#nullable enable

namespace GMapsSync.Src.Presentation.ViewModel;

using System;
using System.Diagnostics;

using ArcGIS.Core.Geometry;

using GMapsSync.Src.Application.Services;

using UseCases = Application.UseCases.WebDriver;

internal sealed class StreetViewViewModel
{
    private record Cords(double Latitude, double Longitude, double Heading);
    private readonly WebDriverHelper driver;

    public StreetViewViewModel()
    {
        this.driver = UseCases.GetWebDriver.Invoke();
    }

    public void LaunchStreetView(MapPoint x, MapPoint y)
    {
        var cords = CalculateParams(x, y);
        var lat = cords.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
        var lon = cords.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
        var h = cords.Heading.ToString(System.Globalization.CultureInfo.InvariantCulture);
        var url = $"https://www.google.com/maps/@?api=1&map_action=pano&viewpoint={lat},{lon}&heading={h}&pitch=0&fov=120";
        this.driver.GoToUrl(url);
    }

    private Cords CalculateParams(MapPoint start, MapPoint end)
    {
        // Calcular el ángulo entre los dos puntos
        double deltaX = end.X - start.X;
        double deltaY = end.Y - start.Y;
        double angleRadians = Math.Atan2(deltaX, deltaY);

        // Convertir a grados y ajustar al rango 0-360
        double angleDegrees = angleRadians * (180.0 / Math.PI);
        if (angleDegrees < 0) angleDegrees += 360.0;

        // Transformar el punto inicial a WGS84 (EPSG:4326)
        var spatialReference = SpatialReferenceBuilder.CreateSpatialReference(4326);
        var transformedGeometry = GeometryEngine.Instance.Project(start, spatialReference);

        if (transformedGeometry is MapPoint transformedPoint)
        {
            return new Cords(transformedPoint.Y, transformedPoint.X, angleDegrees);
        }
        throw new InvalidOperationException("Error al transformar coordenadas.");
    }
}
