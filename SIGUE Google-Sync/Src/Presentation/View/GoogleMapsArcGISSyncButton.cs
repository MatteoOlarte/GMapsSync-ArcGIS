namespace GMapsSync.Src.Presentation.View;

#nullable enable

using System;

using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;

using GMapsSync.Src.Presentation.ViewModel;

internal class GoogleMapsArcGISSyncButton : Button
{
    private readonly GoogleMapsArcGISSyncViewModel? viewModel;

    public GoogleMapsArcGISSyncButton()
    {
        viewModel = this.CreateViewModelOrNull();
    }

    protected override void OnClick() => QueuedTask.Run(this.HandleClick);

    private GoogleMapsArcGISSyncViewModel? CreateViewModelOrNull()
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

    private void HandleClick()
    {
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
        try
        {
            this.viewModel.SyncToArcGIS();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                messageText: $"Error al sincronizar con Google Maps: {ex.Message}",
                caption: "Error - Sincronización Fallida",
                button: System.Windows.MessageBoxButton.OK,
                icon: System.Windows.MessageBoxImage.Error
            );
        }
    }
}
