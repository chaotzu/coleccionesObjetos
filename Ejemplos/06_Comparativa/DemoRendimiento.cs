using System.Diagnostics;

namespace Ejemplos;

/// <summary>
/// Mediciones didácticas con Stopwatch. NO sustituyen a un benchmark riguroso
/// (para eso usa BenchmarkDotNet), pero muestran la diferencia entre O(n) y O(1).
/// Wiki: https://github.com/chaotzu/colecciones-csharp/wiki/07-Comparativa-y-Complejidad
/// </summary>
public static class DemoRendimiento
{
    public static void Ejecutar()
    {
        Consola.Encabezado("COMPARATIVA DE RENDIMIENTO (didáctica)");
        Console.WriteLine("Compila en Release para resultados representativos: dotnet run -c Release");

        BusquedaListaVsHashSetVsDiccionario();
        SacarDelFrenteListaVsCola();
    }

    // #region Búsqueda O(n) vs O(1)
    static void BusquedaListaVsHashSetVsDiccionario()
    {
        Consola.Seccion("Buscar elementos: List vs HashSet vs Dictionary");

        const int n = 50_000;
        const int consultas = 2_000;

        var lista = Enumerable.Range(0, n).ToList();
        var conjunto = new HashSet<int>(lista);
        var diccionario = lista.ToDictionary(x => x, x => x);

        // Buscamos valores que NO existen: peor caso para la lista (recorre todo)
        int[] ausentes = Enumerable.Range(n, consultas).ToArray();

        long tLista = Medir(() => () => { foreach (int k in ausentes) lista.Contains(k); });
        long tConjunto = Medir(() => () => { foreach (int k in ausentes) conjunto.Contains(k); });
        long tDicc = Medir(() => () => { foreach (int k in ausentes) diccionario.ContainsKey(k); });

        Console.WriteLine($"n = {n:N0}, consultas = {consultas:N0}");
        Console.WriteLine($"  List.Contains        O(n)  : {tLista,6} ms");
        Console.WriteLine($"  HashSet.Contains     O(1)* : {tConjunto,6} ms");
        Console.WriteLine($"  Dictionary.ContainsKey O(1)*: {tDicc,6} ms");
        Console.WriteLine("  (* promedio, con una buena función hash)");
    }
    // #endregion

    // #region Sacar del frente
    static void SacarDelFrenteListaVsCola()
    {
        Consola.Seccion("Sacar el primer elemento: List.RemoveAt(0) vs Queue.Dequeue");

        const int n = 30_000;

        // Cada medición recibe una estructura NUEVA y llena (la operación la vacía).
        long tLista = Medir(() =>
        {
            var lista = Enumerable.Range(0, n).ToList();
            return () => { while (lista.Count > 0) lista.RemoveAt(0); };   // O(n) cada vez -> O(n²)
        });

        long tCola = Medir(() =>
        {
            var cola = new Queue<int>(Enumerable.Range(0, n));
            return () => { while (cola.Count > 0) cola.Dequeue(); };       // O(1) cada vez -> O(n)
        });

        Console.WriteLine($"n = {n:N0}");
        Console.WriteLine($"  List.RemoveAt(0) x n : {tLista,6} ms   (O(n²) en total)");
        Console.WriteLine($"  Queue.Dequeue    x n : {tCola,6} ms   (O(n) en total)");
        Console.WriteLine("  Moraleja: la estructura correcta importa más que micro-optimizar.");
    }
    // #endregion

    /// <summary>
    /// Recibe una fábrica que PREPARA los datos y devuelve la acción a medir.
    /// Se ejecuta una vez de calentamiento (JIT) y otra medida, siempre con datos frescos.
    /// </summary>
    private static long Medir(Func<Action> preparar)
    {
        preparar()();                                 // calentamiento (JIT)

        Action accion = preparar();                   // la preparación NO entra en el tiempo medido
        var reloj = Stopwatch.StartNew();
        accion();
        reloj.Stop();
        return reloj.ElapsedMilliseconds;
    }
}
