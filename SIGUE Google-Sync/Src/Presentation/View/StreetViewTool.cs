#nullable enable

using System;
using System.Threading.Tasks;

using ArcGIS.Core.CIM;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;

using GMapsSync.Src.Presentation.ViewModel;

using Point = System.Windows.Point;

namespace GMapsSync.Src.Presentation.View;

internal sealed class StreetViewTool : MapTool
{
    private readonly StreetViewViewModel? viewModel;
    private MapPoint? point0, point1;
    private bool isMousePressed, isDrawingLine;
    private IDisposable? pointGraphic, lineGraphic;


    public StreetViewTool()
    {
        this.viewModel = this.CreateViewModelOrNull();
        this.point0 = null;
        this.point1 = null;
        this.isMousePressed = false;
        this.isDrawingLine = false;
    }

    protected override void OnToolMouseDown(MapViewMouseButtonEventArgs e)
    {
        if (e.ChangedButton == System.Windows.Input.MouseButton.Left) e.Handled = true;
    }

    protected override void OnToolMouseUp(MapViewMouseButtonEventArgs e)
    {
        if (e.ChangedButton == System.Windows.Input.MouseButton.Left) e.Handled = true;
    }

    protected override Task HandleMouseDownAsync(MapViewMouseButtonEventArgs e) => QueuedTask.Run(() => handleMousePressEvent(e));

    protected override Task HandleMouseUpAsync(MapViewMouseButtonEventArgs e) => QueuedTask.Run(() => handleMouseUpEvent(e));

    protected override void OnToolMouseMove(MapViewMouseEventArgs e) => this.handleMouseMoveEvent(e);

    private StreetViewViewModel? CreateViewModelOrNull()
    {
        try
        {
            return new();
        }
        catch (Exception)
        {
            return null;
        }
    }

    private async void handleMousePressEvent(MapViewMouseButtonEventArgs e)
    {
        if (this.isMousePressed)
        {
            return;
        }

        this.isMousePressed = true;
        this.point0 = MapView.Active.ClientToMap(e.ClientPoint);

        await QueuedTask.Run(() =>
        {
            if (pointGraphic != null)
            {
                pointGraphic.Dispose();
                pointGraphic = null;
            }

            var pointSymbol = SymbolFactory.Instance.ConstructPointSymbol(
                ColorFactory.Instance.CreateRGBColor(0, 255, 255),
                8,
                SimpleMarkerStyle.Cross
            );
            var symbolReference = new CIMSymbolReference
            {
                Symbol = pointSymbol
            };

            pointGraphic = MapView.Active.AddOverlay(point0, symbolReference);
        });
    }

    private void handleMouseMoveEvent(MapViewMouseEventArgs e)
    {
        if (!this.isMousePressed)
        {
            return;
        }
        if (!this.isDrawingLine)
        {
            this.isDrawingLine = true;
        }

        _ = DrawLineAsync(e.ClientPoint);
    }

    private async void handleMouseUpEvent(MapViewMouseButtonEventArgs e)
    {
        if (!this.isMousePressed)
        {
            return;
        }

        this.isMousePressed = false;
        this.point1 = MapView.Active.ClientToMap(e.ClientPoint);

        await QueuedTask.Run(() =>
        {
            pointGraphic?.Dispose();
            pointGraphic = null;

            lineGraphic?.Dispose();
            lineGraphic = null;


            if (this.viewModel is null)
            {
                MessageBox.Show(
                    messageText: "No se pudo inicializar la herramienta.\n\nVerifique la configuración del navegador y la ruta del driver. Si el problema persiste, contacte al administrador.",
                    caption: "Error - Falla en el Driver",
                    button: System.Windows.MessageBoxButton.OK,
                    icon: System.Windows.MessageBoxImage.Error
                );
                return;
            }

            if (point0 is not null && point1 is not null)
            {
                this.viewModel.LaunchStreetView(point0, point1);
            }
        });
    }

    private async Task DrawLineAsync(Point clientPoint)
    {
        await QueuedTask.Run(() =>
        {
            if (this.lineGraphic != null)
            {
                this.lineGraphic.Dispose();
                this.lineGraphic = null;
            }

            var current = MapView.Active.ClientToMap(clientPoint);
            var polyline = PolylineBuilderEx.CreatePolyline([this.point0, current]);
            var strokeA = SymbolFactory.Instance.ConstructStroke(
                ColorFactory.Instance.CreateRGBColor(0, 0, 0),
                0.75
            );
            var strokeB = SymbolFactory.Instance.ConstructStroke(
                ColorFactory.Instance.CreateRGBColor(0, 255, 255),
                0.75
            );
            var lineSymbol = new CIMLineSymbol
            {
                SymbolLayers = [strokeA, strokeB]
            };

            strokeA.Effects = [
                new CIMGeometricEffectDashes{DashTemplate = [3.0, 3.0]} // Dash 3px, Gap 3px
            ];

            strokeB.Effects = [
                new CIMGeometricEffectDashes{DashTemplate = [3.0, 3.0], OffsetAlongLine = 3.0}
            ];

            this.lineGraphic = MapView.Active.AddOverlay(polyline, new CIMSymbolReference { Symbol = lineSymbol });
        });
    }
}
