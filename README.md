# Proyecto .NET Core API

Este proyecto es una API REST desarrollada en .NET 8 con arquitectura limpia, implementando patrones de diseño como Repository y Dependency Injection.

## 📋 Requisitos Previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) (opcional, usa InMemory por defecto)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [VS Code](https://code.visualstudio.com/)

## 🚀 Instalación

### 1. Clonar el repositorio

```bash
git clone https://github.com/https://github.com/JuanDBecerra/FNTecnologiaAuth
cd FNTecnologiaAuth
```

### 2. Restaurar paquetes NuGet

```bash
dotnet restore

## 🏗️ Compilación y Ejecución

### Compilar el proyecto

```bash
# Compilar en modo Debug
dotnet build

# Compilar en modo Release
dotnet build --configuration Release

# Compilar proyecto específico
dotnet build ./src/Prueba.Api/Prueba.Api.csproj

# Limpiar compilación
dotnet clean
```

### Ejecutar la aplicación

```bash
# Ejecutar en modo desarrollo
dotnet run

# Ejecutar proyecto específico
dotnet run --project ./src/Prueba.Api/Prueba.Api.csproj

# Ejecutar con watch (recarga automática)
dotnet watch run

# Ejecutar en modo producción
dotnet run --configuration Release
```

### Publicar aplicación

```bash
# Publicar para deployment
dotnet publish -c Release -o ./publish

# Publicar para un runtime específico
dotnet publish -c Release -r win-x64 --self-contained

# Publicar como Single File
dotnet publish -c Release -r win-x64 --self-contained -p:PublishSingleFile=true
```

## 🧪 Pruebas

### Ejecutar todas las pruebas

```bash
# Ejecutar todas las pruebas
dotnet test

# Ejecutar pruebas con detalles
dotnet test --verbosity normal
