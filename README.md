# SpotifyReactASPNetCore

![Spotify](https://user-images.githubusercontent.com/83036818/179426104-293cc5f1-5c80-44cf-832a-66a4cac737b1.jpg)

Este es un programa creado para consumir la API de Spotify siguiendo la guia mostrada en: https://developer.spotify.com/

Esta implementada con dos frameworks, uno para **BackEnd: ASP .Net Core Web API**, y otro para **FrontEnd: React.js.**

Sigue los pasos de **Authorization Code Flow** (0Auth) junto con el login de Spotify https://developer.spotify.com/documentation/general/guides/authorization/code-flow/

## **GUIA DE USO**
**1.-** Registrar la app a usar segun lo establecido en guia de Spotify Developer: https://developer.spotify.com/documentation/general/guides/authorization/app-settings/

**2.-** Debe registrar los usuarios que tendrán acceso a la app creada en el paso anterior.

**3.-** Colocar el **Client ID, Client Secret y Redirect URI** en: SpotifyReactNetCoreBackend/appsettings.json
![appsettings](https://user-images.githubusercontent.com/83036818/179426407-e31faa73-eff3-4ac5-80e5-194bb2c3d58d.PNG)

**4.-** Verificar que la dirección https de la aplicación ASP coincida con el setupProxy de React
![launchSettings](https://user-images.githubusercontent.com/83036818/179426737-c6e02eab-f6ac-4f05-a824-f290faad54a6.PNG)
![image](https://user-images.githubusercontent.com/83036818/179426794-ce14a8f3-61cf-4c2d-bad6-e62f67088160.png)

**5.-** Desplegar la aplicacion web.

VS Code -> En terminal

    ../SpotifyReactNetCoreBackend => dotnet run
    
    ../spotifyreactnetcorefrontend => npm start
 
 
Visual Studio 2022 -> En el explorador de soluciones:

    En la Solución, click derecho, Propiedades.

    Proyecto de inicio -> Seleccionar "Proyecto de Inicio Multiples" -> Iniciar en ambas opciones -> Aceptar.

    Depurar -> Iniciar Depuración.
