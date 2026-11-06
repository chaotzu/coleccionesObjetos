namespace Ejemplos;

/// <summary>
/// List&lt;T&gt;: arreglo dinámico. Crece automáticamente; acceso por índice en O(1).
/// Wiki: https://github.com/chaotzu/colecciones-csharp/wiki/03-Listas
/// </summary>
public static class DemoListas
{
    public static void Ejecutar()
    {
        Consola.Encabezado("LISTAS (List<T>)");
        OperacionesBasicas();
        CapacidadVsCount();
        BusquedaYFiltrado();
        Ordenamiento();
        ModificarDuranteForeach();
        EliminarConRemoveAll();
    }

    // #region Operaciones básicas
    static void OperacionesBasicas()
    {
        Consola.Seccion("Operaciones básicas");

        var numeros = new List<int>();
        numeros.Add(10);
        numeros.Add(20);
        numeros.AddRange(new[] { 30, 40, 50 });
        Console.WriteLine($"Después de Add/AddRange : {Consola.Formato(numeros)}");

        numeros.Insert(0, 5);                         // O(n): desplaza todo a la derecha
        Console.WriteLine($"Insert(0, 5)            : {Consola.Formato(numeros)}");

        numeros.Remove(30);                           // elimina la 1.ª coincidencia
        numeros.RemoveAt(1);                          // elimina por índice
        Console.WriteLine($"Remove(30) + RemoveAt(1): {Consola.Formato(numeros)}");

        Console.WriteLine($"Contains(40) = {numeros.Contains(40)}, IndexOf(50) = {numeros.IndexOf(50)}");
        Console.WriteLine($"Primero = {numeros[0]}, Último = {numeros[^1]}, Count = {numeros.Count}");

        var inicial = new List<string> { "Ana", "Luis", "Marta" };   // inicializador
        List<string> moderna = ["Ana", "Luis", "Marta"];              // expresión de colección
        Console.WriteLine($"Iguales en contenido: {inicial.SequenceEqual(moderna)}");
    }
    // #endregion

    // #region Capacity vs Count
    static void CapacidadVsCount()
    {
        Consola.Seccion("Count vs Capacity: el crecimiento por duplicación");

        var lista = new List<int>();
        int capacidadPrevia = -1;

        for (int i = 0; i < 40; i++)
        {
            lista.Add(i);
            if (lista.Capacity != capacidadPrevia)    // solo imprime cuando hay REDIMENSIÓN
            {
                Console.WriteLine($"Count = {lista.Count,2}  ->  Capacity = {lista.Capacity}");
                capacidadPrevia = lista.Capacity;
            }
        }

        // Si conoces el tamaño final, evita redimensiones intermedias:
        var presized = new List<int>(capacity: 1_000);
        Console.WriteLine($"new List<int>(1000): Count={presized.Count}, Capacity={presized.Capacity}");
    }
    // #endregion

    // #region Búsqueda y filtrado
    static void BusquedaYFiltrado()
    {
        Consola.Seccion("Find, FindAll, Exists, ConvertAll");

        var alumnos = DatosDeEjemplo();

        Estudiante? primero = alumnos.Find(e => e.Promedio >= 9.0);
        List<Estudiante> aprobados = alumnos.FindAll(e => e.Promedio >= 7.0);
        bool hayReprobado = alumnos.Exists(e => e.Promedio < 6.0);
        List<string> nombres = alumnos.ConvertAll(e => e.Nombre.ToUpper());

        Console.WriteLine($"Primero con >= 9.0 : {primero?.Nombre}");
        Console.WriteLine($"Aprobados          : {aprobados.Count} de {alumnos.Count}");
        Console.WriteLine($"¿Hay reprobados?   : {hayReprobado}");
        Console.WriteLine($"Nombres            : {Consola.Formato(nombres)}");
    }
    // #endregion

    // #region Ordenamiento
    static void Ordenamiento()
    {
        Consola.Seccion("Ordenamiento: Sort (inestable) vs OrderBy (estable)");

        var alumnos = DatosDeEjemplo();

        alumnos.Sort((a, b) => b.Promedio.CompareTo(a.Promedio));   // modifica la lista, O(n log n)
        Console.WriteLine("Sort por promedio desc:");
        foreach (var e in alumnos)
            Console.WriteLine($"  {e.Promedio,5:F1}  {e.Nombre} ({e.Grupo})");

        // LINQ crea una secuencia NUEVA y es estable: respeta el orden previo en empates
        var porGrupoYNombre = alumnos.OrderBy(e => e.Grupo).ThenBy(e => e.Nombre).ToList();
        Console.WriteLine("OrderBy grupo, luego nombre:");
        foreach (var e in porGrupoYNombre)
            Console.WriteLine($"  {e.Grupo}  {e.Nombre}");
    }
    // #endregion

    // #region Error clásico: modificar durante foreach
    static void ModificarDuranteForeach()
    {
        Consola.Seccion("Error clásico: modificar la lista dentro de un foreach");

        var lista = new List<int> { 1, 2, 3, 4 };
        try
        {
            foreach (int x in lista)
                if (x == 2) lista.Remove(x);          // cambia _version -> el enumerador falla
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
        }
    }
    // #endregion

    // #region Forma correcta de eliminar
    static void EliminarConRemoveAll()
    {
        Consola.Seccion("Forma correcta: RemoveAll (o recorrer hacia atrás)");

        var lista = new List<int> { 1, 2, 3, 4, 5, 6 };

        int eliminados = lista.RemoveAll(x => x % 2 == 0);       // una sola pasada, O(n)
        Console.WriteLine($"RemoveAll(pares) eliminó {eliminados}: {Consola.Formato(lista)}");

        var otra = new List<int> { 1, 2, 3, 4, 5, 6 };
        for (int i = otra.Count - 1; i >= 0; i--)                // hacia atrás: los índices no se corren
            if (otra[i] % 2 == 0) otra.RemoveAt(i);
        Console.WriteLine($"Recorrido inverso        : {Consola.Formato(otra)}");
    }
    // #endregion

    internal static List<Estudiante> DatosDeEjemplo() =>
    [
        new(1, "Carlos",  "A", 8.5),
        new(2, "Beatriz", "B", 9.4),
        new(3, "Diego",   "A", 5.8),
        new(4, "Ana",     "B", 7.2),
        new(5, "Elena",   "A", 9.4),
    ];
}
