# GMaps-Sync para ArcGIS Pro

Un Add-In para ArcGIS Pro que potencia tu experiencia de mapeo integrando Google Street View y sincronizando la vista con Google Maps de forma bidireccional. Explora, navega y compara ubicaciones en tiempo real, todo desde la comodidad de tu entorno de ArcGIS Pro.

> [!note]
> Este Add-In no integra Google Maps como un navegador embebido dentro de ArcGIS Pro. Al abrir Street View o sincronizar la vista, se abrirá una ventana de navegador aparte.

## Requisitos del sistema

### Requisitos obligatorios

* Google Chrome instalado (versión estable más reciente recomendada) o Mozilla Firefox

* ArcGIS Pro 3.4 o superior ejecutándose correctamente

* .NET 8 Runtime (normalmente incluido con ArcGIS Pro)

* Conexión a Internet activa

## Instalación

### Paso 1: Obtencion del archivo (Add-In)

Recibirás el archivo de instalación con extensión .esriAddinX a través de uno de estos métodos:

* USB: Archivo físico en una unidad USB

* Correo electrónico: Archivo adjunto o enlace de descarga

* Red compartida: Acceso a carpeta de red compartida

Guarda el archivo GMaps-Sync.esriAddinX en una ubicación accesible como:

* Escritorio

* Carpeta de Descargas

* Documentos

### Paso 2: Instalación del complemento

1. Cierra ArcGIS Pro completamente si está ejecutándose

2. Navega hasta la ubicación del archivo GMaps-Sync.esriAddinX

3. Haz doble clic en el archivo

4. Aparecerá el instalador de Add-In de ArcGIS Pro

5. Haz clic en "Instalar" y acepte los términos si se le solicitan

6. Espera a que aparezca el mensaje de "Instalación completada"

7. Inicia ArcGIS Pro


### Paso 3:Configuración del WebDriver

Para que el Add-In funcione correctamente, es necesario configurar el WebDriver:

1. Descarga el WebDriver correspondiente a tu navegador:

   - [ChromeDriver](https://googlechromelabs.github.io/chrome-for-testing/) para Google Chrome
   - [GeckoDriver](https://github.com/mozilla/geckodriver/releases) para Mozilla Firefox

2. Abre ArcGIS Pro y ve a la configuración del Add-In:
   - En el menú, selecciona "Proyecto" > "Opciones" > "GMaps"
   - Selecciona el navegador que utilizarás
   - Especifica la ruta donde se encuentra el archivo del WebDriver

### Paso 4: Verificación de la instalación

Después de iniciar ArcGIS Pro:

1. Abre o crea un proyecto

2. En la cinta de opciones principal, buscaa la pestaña "GMaps"

3. Si la pestaña está visible, la instalación fue exitosa


## Configuración 

### Configuración inicial obligatoria

Al utilizar el Add-In por primera vez, es necesario realizar una configuración inicial para asegurar su correcto funcionamiento. 

Sigue estos pasos:

1. Acceder a la Configuración del Add-In:

   - Abra ArcGIS Pro.
   - En la esquina superior izquierda, haga clic en "Project".
   - En el menú desplegable, seleccione "Options".

2. Navegar a la Sección GMaps:

- En la ventana de Options, localiza y haz clic en la categoría "GMaps" en el panel izquierdo.

3. Seleccionar navegador y configurar WebDriver:

- En el panel de configuración de GMaps:
   - Selecciona tu navegador preferido: Elije entre Google Chrome o Mozilla Firefox desde el menú desplegable.
   - Especifica la ruta del WebDriver: Haz clic en "Examinar" o "Browse" y navega hasta la ubicación donde tienes guardado el archivo del controlador (WebDriver) correspondiente a su navegador.

      - Para Chrome: chromedriver.exe

      - Para Firefox: geckodriver.exe

![add-ln](docs/images/project-options-gmaps.png) 

4. Verificar la configuración:

- Haz clic en "Probar Conexión" o "Test Connection" para confirmar que la configuración es correcta y que el Add-In puede comunicarse con el navegador.

- Si la prueba es exitosa, aparecerá un mensaje de confirmación. Haz clic en "Aceptar" o "OK" para guardar la configuración.

Nota: Es fundamental que la versión del WebDriver coincida exactamente con la versión de su navegador para evitar errores. Si no tiene el WebDriver, consulte la sección "Requisitos del Sistema" para obtener enlaces de descarga.


## Cómo usar el Add-ln GMaps

Una vez configurado correctamente, el Add-In ofrece tres herramientas principales accesibles desde la pestaña "GMaps" en la cinta de opciones de ArcGIS Pro. A continuación, se detalla el funcionamiento de cada una:


### 1. Google Street View

Propósito: Obtener una vista de Street View de cualquier punto en su mapa.



| Modo Claro                                                                  | Modo Oscuro                                                                     |
| --------------------------------------------------------------------------- | ------------------------------------------------------------------------------- |
| ![Herramienta Street View](SIGUE%20Google-Sync/Images/StreetViewTool32.png) | ![Herramienta Street View](SIGUE%20Google-Sync/DarkImages/StreetViewTool32.png) |

Pasos para usar:

1. Activar la Herramienta: En la pestaña GMaps, haga clic en el botón "Street View".

2. Seleccionar Punto y Dirección:

   - En el mapa, haga clic y mantenga presionado el botón del ratón en la ubicación deseada.
   - Sin soltar, arrastre el ratón para definir la dirección de visualización. Verá una línea amarilla que indica la dirección.
   - Suelta el botón del ratón para confirmar.

Resultado: Se abrirá automáticamente una ventana de su navegador predeterminado (Chrome/Firefox) mostrando la vista de Google Street View para la ubicación y dirección seleccionadas.

![muestra](docs/images/muestra.png)


#### 2. Sincronización en tiempo real: ArcGIS Pro - Google Maps

Propósito: Mantener una ventana de Google Maps constantemente sincronizada con su vista actual en ArcGIS Pro. Cada vez que mueva el mapa en ArcGIS, Google Maps se actualizará automáticamente.

![Street View](docs/images/StreetView-GIF.gif)

Pasos para usar:

1. Activar sincronización continua: En la pestaña GMaps, haga clic en el botón "Abrir en Maps".

2. Navegar en ArcGIS Pro:

- Se abrirá una ventana de Google Maps en su navegador.

- A partir de este momento, cada vez que:
   - Muevas el mapa (pan)
   - Hagas zoom in/out
   - Cambies la extensión visible

- Google Maps se actualizará en tiempo real para reflejar exactamente la misma vista.

3. Modo de sincronización activa:

- El botón "Abrir en Maps" permanecerá resaltado indicando que la sincronización está activa.

- Puede continuar trabajando en ArcGIS Pro normalmente mientras Google Maps muestra paralelamente la misma vista.


![ArcGIS - Google Maps sync](docs/images/ArcGIS-GMaps-GIF.gif)



   Nota: Esta sincronización unidireccional (ArcGIS → Maps) se mantendrá activa hasta que cierre la ventana del navegador o desactive manualmente la herramienta.



### 3. Sincronización puntual: Google Maps - ArcGIS sync

Propósito: Traer de manera puntual la vista actual de Google Maps a ArcGIS Pro.



| Modo Claro                                                        | Modo Oscuro                                                           |
| ----------------------------------------------------------------- | --------------------------------------------------------------------- |
| ![Traer de Maps](SIGUE%20Google-Sync/Images/BrowserUpdated32.png) | ![Traer de Maps](SIGUE%20Google-Sync/DarkImages/BrowserUpdated32.png) |

Esta función permite importar la vista actual de Google Maps a ArcGIS Pro. Al activarla, el mapa de ArcGIS se centra y ajusta automáticamente según la ubicación y el nivel de zoom que tenga Google Maps.

#### ¿Como usar esta herramienta?

1. Prerrequisito: Debe tener la sincronización activa (ventana de Maps abierta mediante "Abrir en Maps").

2. Navegar en Google Maps:

- Utiliza la ventana de Google Maps para:

   - Moverse a una nueva ubicación

   - Hacer zoom a un nivel diferente

   - Cambiar a vista Satélite/Híbrida

   - Explorar puntos de interés

3. Actualizar ArcGIS Pro:

- En la pestaña GMaps, haz clic en el botón "Traer de Maps".

- Resultado inmediato: La vista en ArcGIS Pro se ajustará automáticamente para coincidir exactamente con lo que está viendo en Google Maps.

![ArcGIS - Google Maps sync](docs/images/GMaps-ArcGIS-GIF.gif)


> [!WARNING]
> Esta función solo funciona si la ventana emergente de Google Maps, abierta por el Add-In, sigue activa y visible en tu navegador. Si la cierras o navegas a otra página, la sincronización no será posible.



## Solución de problemas

### Error al abrir Street View o sincronizar mapas

![Error al inicializar Street View](docs/images/error-inicializar-streetview.png)

- Verifica que el WebDriver esté correctamente configurado en las opciones del Add-In
- Asegúrate de que la versión del WebDriver sea compatible con la versión de tu navegador

### La sincronización no funciona correctamente

![Error al inicializar Street View](docs/images/error-inicializar-streetview.png)

- Verifica que tienes una conexión a internet activa
- Asegúrate de que la pestaña de Google Maps esté activa y visible

## Desarrollo

Si deseas contribuir al desarrollo de este Add-In, sigue estos pasos:

### Configuración del entorno de desarrollo

1. Clona el repositorio
2. Abre la solución en Visual Studio (2022 o superior recomendado)
3. Instala las dependencias NuGet requeridas
4. Configura la ruta de salida para apuntar a la carpeta de Add-Ins de ArcGIS Pro

### Estructura del proyecto

```
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

- [Selenium WebDriver](https://www.selenium.dev/documentation/en/webdriver/)
- [ArcGIS Pro SDK](https://github.com/Esri/arcgis-pro-sdk)
- [Google Maps API](https://developers.google.com/maps/documentation)
