#  Colecciones de objetos en C#

[![C#](https://img.shields.io/badge/C%23-12-239120?logo=csharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-8.0%2B-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)

Apuntes de clase y ejemplos ejecutables sobre **arreglos, listas, pilas, colas y diccionarios** en C#.

-  **Apuntes:** [Wiki del repositorio]([https://github.com/chaotzu/colecciones-csharp/wiki](https://github.com/chaotzu/coleccionesObjetos/wiki))
-  **Código:** carpeta [`Ejemplos/`](Ejemplos)
-  **Fuente del wiki:** carpeta [`wiki/`](wiki) (ver [PUBLICAR-WIKI.md](PUBLICAR-WIKI.md))

##  Ejecutar

Requiere el [SDK de .NET 8](https://dotnet.microsoft.com/download) o superior.

```bash
cd Ejemplos
dotnet run               # menú interactivo
dotnet run -- 3          # solo el demo n.º 3
dotnet run -- todo       # todos los demos
dotnet run -c Release -- 6   # comparativa de rendimiento
```

##  Estructura

```
colecciones-csharp/
├── Ejemplos/
│   ├── Program.cs                      ← menú principal
│   ├── Comun/                          ← utilidades y modelo Estudiante
│   ├── 01_Arreglos/DemoArreglos.cs
│   ├── 02_Listas/DemoListas.cs
│   ├── 03_Pilas/DemoPilas.cs
│   ├── 04_Colas/DemoColas.cs
│   ├── 05_Diccionarios/DemoDiccionarios.cs
│   └── 06_Comparativa/DemoRendimiento.cs
└── wiki/                               ← páginas del wiki (Markdown)
```

Cada demo está dividido en métodos pequeños con comentarios `// #region`, pensados para proyectarse en clase.
