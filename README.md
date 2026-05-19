# Sistema de control de inventario

## Requisitos previos para la ejecución

Antes de compilar y ejecutar la apliación, asegúrese de contar con el siguiente entorno instalado:

- **SDK de .NET 8** o superior.
- **Servidor MySQL**.
- Un IDE compatible (Visual Studio o VS Code).

---

## Instrucciones de configuración e Instalación

### 1. Preparación de Base de Datos

1. Abra su gestor de base de datos preferido (DBeaver, MySQL Workbench, etc.).
2. Copie, pegue y ejecute íntegradamente el archivo `script.sql` adjunto en la raíz de este repositorio

### 2. Configuración de la cadena de conexión

Abra el archivo `appsettings.json` en el proyecto web y configure sus credenciales locales de acceso a MySQL en la seccipon de `ConnectionStrings`

### 3. Pasos para ejecutar la aplicación

**Desde Visual Studio (recomendado):**

1. Abra el explorador de archivos y haga doble clic sobre el archivo de solución `.sln` del proyecto para cargardo en el IDE
2. Asegúrese de que el proyecto esté configurado como el proyecto de inicio
3. Presione la tecla `F5` o haga clic en el botón (icono de play verde) en la barra de herramientas superior
4. Compilará la solución y levantará un servidor local web abriendo su navegador por defecto en la pantalla del inventario
