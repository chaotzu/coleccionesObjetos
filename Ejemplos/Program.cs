using Ejemplos;

// Menú principal: elige qué colección quieres explorar.
var demos = new (string Nombre, Action Ejecutar)[]
{
    ("Arreglos      (T[])",               DemoArreglos.Ejecutar),
    ("Listas        (List<T>)",           DemoListas.Ejecutar),
    ("Pilas         (Stack<T>)",          DemoPilas.Ejecutar),
    ("Colas         (Queue<T>)",          DemoColas.Ejecutar),
    ("Diccionarios  (Dictionary<K,V>)",   DemoDiccionarios.Ejecutar),
    ("Comparativa de rendimiento",        DemoRendimiento.Ejecutar),
};

// Permite ejecutar directamente:  dotnet run -- 3   (o)  dotnet run -- todo
if (args.Length > 0)
{
    if (args[0].Equals("todo", StringComparison.OrdinalIgnoreCase))
        foreach (var d in demos) d.Ejecutar();
    else if (int.TryParse(args[0], out int n) && n >= 1 && n <= demos.Length)
        demos[n - 1].Ejecutar();
    else
        Console.WriteLine($"Argumento no válido. Usa un número del 1 al {demos.Length} o 'todo'.");
    return;
}

while (true)
{
    Console.WriteLine();
    Console.WriteLine("=== Colecciones en C# · Ejemplos de clase ===");
    for (int i = 0; i < demos.Length; i++)
        Console.WriteLine($"  {i + 1}. {demos[i].Nombre}");
    Console.WriteLine("  T. Ejecutar todo");
    Console.WriteLine("  0. Salir");
    Console.Write("Opción: ");

    string? opcion = Console.ReadLine()?.Trim();
    if (opcion is null || opcion == "0") break;

    if (opcion.Equals("T", StringComparison.OrdinalIgnoreCase))
        foreach (var d in demos) d.Ejecutar();
    else if (int.TryParse(opcion, out int n) && n >= 1 && n <= demos.Length)
        demos[n - 1].Ejecutar();
    else
        Console.WriteLine("Opción no válida.");
}
