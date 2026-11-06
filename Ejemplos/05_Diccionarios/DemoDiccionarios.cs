namespace Ejemplos;

/// <summary>
/// Dictionary&lt;TKey, TValue&gt;: tabla hash. Búsqueda, inserción y borrado en O(1) promedio.
/// Wiki: https://github.com/chaotzu/colecciones-csharp/wiki/06-Diccionarios
/// </summary>
public static class DemoDiccionarios
{
    public static void Ejecutar()
    {
        Consola.Encabezado("DICCIONARIOS (Dictionary<TKey, TValue>)");
        OperacionesBasicas();
        AccesoSeguro();
        OrdenNoGarantizado();
        FrecuenciaDePalabras();
        AgrupacionPorClave();
        ComparadoresDeCadenas();
        ClavesPersonalizadas();
        ConjuntosHashSet();
    }

    // #region Operaciones básicas
    static void OperacionesBasicas()
    {
        Consola.Seccion("Agregar, leer, actualizar, eliminar, recorrer");

        var capitales = new Dictionary<string, string>
        {
            ["México"] = "Ciudad de México",
            ["Argentina"] = "Buenos Aires",
            ["Chile"] = "Santiago",
        };

        capitales.Add("Perú", "Lima");                // Add LANZA excepción si la clave ya existe
        capitales["Colombia"] = "Bogotá";             // el indexador agrega O actualiza
        capitales["Chile"] = "Santiago de Chile";     // actualiza
        capitales.Remove("Argentina");

        foreach (KeyValuePair<string, string> par in capitales)
            Console.WriteLine($"  {par.Key,-10} -> {par.Value}");

        Console.WriteLine($"Count = {capitales.Count}");
        Console.WriteLine($"Claves : {Consola.Formato(capitales.Keys)}");
        Console.WriteLine($"Valores: {Consola.Formato(capitales.Values)}");
    }
    // #endregion

    // #region Acceso seguro
    static void AccesoSeguro()
    {
        Consola.Seccion("Acceso seguro: TryGetValue, TryAdd, GetValueOrDefault");

        var precios = new Dictionary<string, decimal> { ["pan"] = 12.5m, ["leche"] = 28m };

        try
        {
            decimal x = precios["queso"];             // la clave no existe
            Console.WriteLine(x);
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"{ex.GetType().Name} al usar el indexador con clave inexistente");
        }

        if (precios.TryGetValue("leche", out decimal precio))   // UNA sola búsqueda hash
            Console.WriteLine($"leche cuesta {precio:F2} MXN");

        Console.WriteLine($"TryAdd('pan', 99)  -> {precios.TryAdd("pan", 99m)}  (ya existía, no cambia)");
        Console.WriteLine($"TryAdd('huevo', 3) -> {precios.TryAdd("huevo", 3m)}");
        Console.WriteLine($"GetValueOrDefault('queso', -1) = {precios.GetValueOrDefault("queso", -1m)}");
    }
    // #endregion

    // #region Orden no garantizado
    static void OrdenNoGarantizado()
    {
        Consola.Seccion("El orden de un Dictionary NO está garantizado");

        var d = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 };
        Console.WriteLine($"Inicial            : {Consola.Formato(d.Keys)}");

        d.Remove("b");                                // deja un hueco interno reutilizable
        d["d"] = 4;                                   // la clave nueva ocupa ese hueco
        Console.WriteLine($"Tras Remove + nuevo: {Consola.Formato(d.Keys)}");
        Console.WriteLine("Si necesitas orden, usa SortedDictionary o ordena al consumir (OrderBy).");
    }
    // #endregion

    // #region Frecuencia de palabras
    static void FrecuenciaDePalabras()
    {
        Consola.Seccion("Patrón clásico: contar frecuencias");

        string texto = "la pila y la cola son colecciones la lista tambien";
        var frecuencia = new Dictionary<string, int>();

        foreach (string palabra in texto.Split(' '))
            frecuencia[palabra] = frecuencia.GetValueOrDefault(palabra) + 1;

        foreach (var (palabra, veces) in frecuencia.OrderByDescending(p => p.Value).ThenBy(p => p.Key))
            Console.WriteLine($"  {palabra,-12} {new string('#', veces)} ({veces})");
    }
    // #endregion

    // #region Agrupación por clave
    static void AgrupacionPorClave()
    {
        Consola.Seccion("Patrón: agrupar (Dictionary<K, List<V>>)");

        var porGrupo = new Dictionary<string, List<Estudiante>>();

        foreach (var e in DemoListas.DatosDeEjemplo())
        {
            if (!porGrupo.TryGetValue(e.Grupo, out var lista))
            {
                lista = new List<Estudiante>();
                porGrupo[e.Grupo] = lista;            // crea la lista la primera vez
            }
            lista.Add(e);
        }

        foreach (var (grupo, alumnos) in porGrupo)
            Console.WriteLine($"  Grupo {grupo}: {string.Join(", ", alumnos.Select(a => a.Nombre))}");
    }
    // #endregion

    // #region Comparadores
    static void ComparadoresDeCadenas()
    {
        Consola.Seccion("IEqualityComparer: claves sin distinguir mayúsculas");

        var sensible = new Dictionary<string, int> { ["Admin"] = 1 };
        var insensible = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase) { ["Admin"] = 1 };

        Console.WriteLine($"Por defecto          ContainsKey(\"ADMIN\") = {sensible.ContainsKey("ADMIN")}");
        Console.WriteLine($"OrdinalIgnoreCase    ContainsKey(\"ADMIN\") = {insensible.ContainsKey("ADMIN")}");
    }
    // #endregion

    // #region Claves personalizadas
    static void ClavesPersonalizadas()
    {
        Consola.Seccion("Claves personalizadas: Equals y GetHashCode importan");

        // CASO MAL HECHO: clase sin Equals/GetHashCode -> igualdad por REFERENCIA
        var malo = new Dictionary<PuntoClase, string> { [new PuntoClase(1, 2)] = "A" };
        Console.WriteLine($"Clase sin Equals/GetHashCode: ContainsKey(new(1,2)) = {malo.ContainsKey(new PuntoClase(1, 2))}");

        // CASO BIEN HECHO: record struct -> igualdad por VALOR generada por el compilador
        var bueno = new Dictionary<PuntoValor, string> { [new PuntoValor(1, 2)] = "A" };
        Console.WriteLine($"record struct:                ContainsKey(new(1,2)) = {bueno.ContainsKey(new PuntoValor(1, 2))}");
    }

    private sealed class PuntoClase(int x, int y)
    {
        public int X { get; } = x;
        public int Y { get; } = y;
    }

    private readonly record struct PuntoValor(int X, int Y);
    // #endregion

    // #region HashSet
    static void ConjuntosHashSet()
    {
        Consola.Seccion("Hermano cercano: HashSet<T> (solo claves, sin valores)");

        var a = new HashSet<int> { 1, 2, 3, 4 };
        var b = new HashSet<int> { 3, 4, 5, 6 };

        Console.WriteLine($"Add(3) devuelve {a.Add(3)} (duplicado ignorado)");

        var union = new HashSet<int>(a); union.UnionWith(b);
        var interseccion = new HashSet<int>(a); interseccion.IntersectWith(b);
        var diferencia = new HashSet<int>(a); diferencia.ExceptWith(b);

        Console.WriteLine($"A ∪ B = {Consola.Formato(union)}");
        Console.WriteLine($"A ∩ B = {Consola.Formato(interseccion)}");
        Console.WriteLine($"A \\ B = {Consola.Formato(diferencia)}");
    }
    // #endregion
}
