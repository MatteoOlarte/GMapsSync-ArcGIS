namespace GMapsSync.Src.Application.Ext;

#nullable enable

using ArcGIS.Core.Geometry;

public static class EnvelopeExtensions
{
    public static Envelope? ToWGS84(this Envelope extent)
    {
        var currentSR = extent.SpatialReference;
        Envelope? wgs84Extent;

        if (currentSR.Wkid != SpatialReferences.WGS84.Wkid)
        {
            wgs84Extent = GeometryEngine.Instance.Project(extent, SpatialReferences.WGS84) as Envelope;
        }
        else
        {
            wgs84Extent = extent;
        }
        return wgs84Extent;
    }
}
