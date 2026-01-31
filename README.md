# GMaps-Sync para ArcGIS Pro

> [!note]
> Este Add-In no integra Google Maps como un navegador embebido dentro de ArcGIS Pro; por lo tanto, no se infringe ninguno
> de los términos y condiciones de uso de Google Maps.

## Objetivo

Desarrollar un Add-In para ArcGIS Pro que permita integrar y sincronizar Google Street View y Google Maps con la vista de
mapas de ArcGIS Pro, facilitando la exploración, navegación y comparación espacial de ubicaciones en tiempo real, mediante
una sincronización bidireccional entre ambas plataformas.

## Requisitos

### Requisitos del Sistema

| Requisito            | Descripción                                                                     |
| -------------------- | ------------------------------------------------------------------------------- |
| Navegador web        | Contar con un navegador web compatible                                          |
| ArcGIS Pro           | Versión 3.5 o superior.                                                         |
| Entorno de ejecución | .NET 8 Desktop Runtime (generalmente incluido en la instalación de ArcGIS Pro). |
| Conectividad         | Conexión a Internet estable y activa para cargar los servicios de Google Maps.  |

### Navegadores Compatibles

| Nombre del Navegador |
| -------------------- |
| Google Chrome        |
| Firefox              |
| Microsoft Edge       |

## Instalación

### Paso 1: Obtención del archivo (Add-In)

El archivo de instalación del Add-In, con extensión `.esriAddinX`, debe descargarse desde la página oficial del proyecto,
disponible en el siguiente enlace:

👉 [Releases · GMapsSync-ArcGIS](https://github.com/MatteoOlarte/GMapsSync-ArcGIS/releases)

Una vez descargado, guarda el archivo `SIGUE Google-Sync.esriAddinX` en una ubicación accesible del equipo, por ejemplo:

* Escritorio

* Descargas

* Documentos

### Paso 2: Instalación del complemento

1. Cierra completamente **ArcGIS Pro** si se encuentra en ejecución.

2. Navega hasta la ubicación donde guardaste el archivo **SIGUE Google-Sync.esriAddinX**.

3. Haz doble clic sobre el archivo.

4. Se abrirá el instalador de Add-Ins de ArcGIS Pro.

5. Haz clic en **“Instalar”** y acepta los términos si se te solicitan.

6. Espera a que aparezca el mensaje de **“Instalación completada”**.

7. Inicia nuevamente **ArcGIS Pro**.

### Paso 3: Configuración del WebDriver

> [!tip]
> La configuración manual del WebDriver es **opcional**.

De forma predeterminada, el Add-In **descarga y configura automáticamente** el WebDriver necesario según el navegador
seleccionado.

Para realizar la configuración inicial:

1. Abre **ArcGIS Pro**.

2. Ve al menú **Proyecto > Opciones > GMaps**.

3. Selecciona el navegador que deseas utilizar.
   
   * Google Chrome
   
   * Mozilla Firefox
   
   * Microsoft Edge

4. El Add-In intentará descargar automáticamente el WebDriver correspondiente y dejarlo listo para su uso.

#### Configuración manual (solo si la configuración automática falla)

En caso de que la descarga o configuración automática del WebDriver no funcione correctamente, puedes realizar la
configuración manual:

1. Descarga el binario del WebDriver correspondiente a tu navegador:
   
   * **Chrome**: ChromeDriver
   * **Firefox**: GeckoDriver
   * **Edge**: Edge WebDriver

2. Vuelve a **Proyecto > Opciones > GMaps**.

3. Selecciona el navegador correspondiente.

4. Especifica manualmente la ruta donde se encuentra el archivo del WebDriver descargado.

Una vez configurado, utiliza la opción de prueba de conexión para verificar que el Add-In puede comunicarse correctamente
con el navegador.

### Paso 4: Verificación de la instalación

Después de iniciar ArcGIS Pro:

1. Abre o crea un proyecto

2. En la cinta de opciones principal, busca la pestaña "GMaps"

3. Si la pestaña está visible, la instalación fue exitosa

## Uso de las herramientas

Una vez configurado correctamente, el Add-In ofrece tres herramientas principales accesibles desde la pestaña "GMaps"
en la cinta de opciones de ArcGIS Pro. A continuación, se detalla el funcionamiento de cada una:

### 1. Google Street View

#### Propósito

Obtener una vista de Street View de cualquier punto en su mapa.

| Claro                                                                       | Oscuro                                                                          |
| --------------------------------------------------------------------------- | ------------------------------------------------------------------------------- |
| ![Herramienta Street View](SIGUE%20Google-Sync/Images/StreetViewTool32.png) | ![Herramienta Street View](SIGUE%20Google-Sync/DarkImages/StreetViewTool32.png) |

#### ¿Cómo usar esta herramienta?

1. Activa la Herramienta: En la pestaña GMaps, haz clic en el botón "Street View".

2. Selecciona Punto y Dirección:
   
   * En el mapa, haz clic y mantén presionado el botón del ratón en la ubicación deseada.
   * Sin soltar, arrastra el ratón para definir la dirección de visualización. Verás una línea amarilla que indica la
     dirección.
   * Suelta el botón del ratón para confirmar.

#### Resultado

Se abrirá automáticamente una ventana de su navegador predeterminado (Chrome/Firefox) mostrando la vista de Google
Street View para la ubicación y dirección seleccionadas.

![Street View](docs/images/StreetView-GIF.gif)

### 2. Sincronización en tiempo real: ArcGIS Pro - Google Maps

#### Propósito

Mantener una ventana de Google Maps constantemente sincronizada con su vista actual en ArcGIS Pro. Cada vez que mueva el
mapa en ArcGIS, Google Maps se actualizará automáticamente.

| Claro                                                            | Oscuro                                                               |
| ---------------------------------------------------------------- | -------------------------------------------------------------------- |
| ![Traer de Maps](SIGUE%20Google-Sync/Images/OpenInBrowser32.png) | ![Traer de Maps](SIGUE%20Google-Sync/DarkImages/OpenInBrowser32.png) |

#### ¿Cómo usar esta herramienta?

1. Activa sincronización continua: En la pestaña GMaps, haz clic en el botón "Abrir en Maps".

2. Navega en ArcGIS:
   
   * Se abrirá una ventana de Google Maps en tu navegador.
   
   * A partir de este momento, cada vez que:
     
     * Muevas el mapa (pan)
     * Hagas zoom in/out
     * Cambies la extensión visible
   
   * Google Maps se actualizará en tiempo real para reflejar exactamente la misma vista.

3. Modo de sincronización activa:
   
   * El botón "Abrir en Maps" permanecerá resaltado indicando que la sincronización está activa.
   
   * Puedes continuar trabajando en ArcGIS Pro normalmente mientras Google Maps muestra paralelamente la misma vista.

![ArcGIS - Google Maps sync](docs/images/ArcGIS-GMaps-GIF.gif)

### 3. Sincronización puntual: Google Maps - ArcGIS sync

#### Propósito

Traer de manera puntual la vista actual de Google Maps a ArcGIS Pro.

Esta función permite importar la vista actual de Google Maps a ArcGIS Pro. Al activarla, el mapa de ArcGIS se centra y
automáticamente según la ubicación y el nivel de zoom que tenga Google Maps.

| Claro                                                              | Oscuro                                                                 |
| ------------------------------------------------------------------ | ---------------------------------------------------------------------- |
| ![Traer de Maps](SIGUE%20Google-Sync/Images/OpenFromBrowser32.png) | ![Traer de Maps](SIGUE%20Google-Sync/DarkImages/OpenFromBrowser32.png) |

#### ¿Cómo usar esta herramienta?

1. Prerrequisito: Debes tener la sincronización activa (ventana de Maps abierta mediante "Abrir en Maps").

2. Navega en Google Maps:
   
   * Utiliza la ventana de Google Maps para:
     
     * Moverse a una nueva ubicación.
     
     * Hacer zoom a un nivel diferente.
     
     * Cambiar a vista Satélite/Híbrida.
     
     * Explorar puntos de interés.

3. Actualiza ArcGIS Pro:
   
   * En la pestaña GMaps, haz clic en el botón "Traer de Maps".
   
   * Resultado inmediato: La vista en ArcGIS Pro se ajustará automáticamente para coincidir exactamente con lo que está viendo en Google Maps.

#### Resultado

Se abrirá automáticamente una ventana de su navegador predeterminado (Chrome/Firefox) mostrando la vista de Google Street
View para la ubicación y dirección seleccionadas.

![ArcGIS - Google Maps sync](docs/images/GMaps-ArcGIS-GIF.gif)

## Solución de problemas

### Error al abrir Street View o sincronizar mapas

![Error al inicializar Street View](docs/images/error-inicializar-streetview.png)

* Verifica que el WebDriver esté correctamente configurado en las opciones del Add-In
* Asegúrate de que la versión del WebDriver sea compatible con la versión de tu navegador

### La sincronización no funciona correctamente

![Error al inicializar Street View](docs/images/error-inicializar-streetview.png)

* Verifica que tienes una conexión a internet activa
* Asegúrate de que la pestaña de Google Maps esté activa y visible

## Desarrollo

Si deseas contribuir al desarrollo de este Add-In, sigue estos pasos:

### Configuración del entorno de desarrollo

1. Clona el repositorio
2. Abre la solución en Visual Studio (2022 o superior recomendado)
3. Instala las dependencias NuGet requeridas
4. Configura la ruta de salida para apuntar a la carpeta de Add-Ins de ArcGIS Pro

### Estructura del proyecto

```text
SIGUE Google-Sync/
├── Application/
│   ├── Ext/              # Extensiones de utilidades
│   ├── Services/         # Servicios como WebDriverHelper
│   └── UseCases/         # Casos de uso de la aplicación
├── Core/
│   ├── Browser.cs        # Enumeraciones y tipos core
│   └── Settings.cs       # Configuración del add-in
├── Presentation/
│   ├── View/             # Vistas XAML y code-behind
│   └── ViewModel/        # ViewModels para las vistas (MVVM)
├── Images/               # Recursos gráficos para modo claro
├── DarkImages/           # Recursos gráficos para modo oscuro
├── Config.daml           # Archivo de configuración para ArcGIS Pro
└── MainModule.cs         # Punto de entrada del add-in
```

### Compilación y depuración

Para compilar y probar el Add-In:

1. Compila el proyecto en Visual Studio
2. El Add-In se instalará automáticamente en la ubicación de desarrollo de ArcGIS Pro
3. Inicia ArcGIS Pro para probar los cambios

## Licencia

Este proyecto está bajo la Licencia MIT. Consulta el archivo [LICENSE](LICENSE) para más detalles.

## Créditos y Agradecimientos

* [Selenium WebDriver](https://www.selenium.dev/documentation/en/webdriver/)
* [ArcGIS Pro SDK](https://github.com/Esri/arcgis-pro-sdk)
* [Google Maps API](https://developers.google.com/maps/documentation)
