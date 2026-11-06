namespace Ejemplos;

/// <summary>
/// Arreglos (T[]): tamaño fijo, memoria contigua, acceso por índice en O(1).
/// Wiki: https://github.com/chaotzu/colecciones-csharp/wiki/02-Arreglos
/// </summary>
public static class DemoArreglos
{
    public static void Ejecutar()
    {
        Consola.Encabezado("ARREGLOS (T[])");
        Declaracion();
        Indices();
        Recorrido();
        Multidimensional();
        Escalonado();
        MetodosDeArray();
        SemanticaDeReferencia();
        CovarianzaPeligrosa();
    }

    // #region Declaración e inicialización
    static void Declaracion()
    {
        Consola.Seccion("Declaración e inicialización");

        int[] a = new int[5];                    // reserva 5 enteros, todos en 0
        int[] b = { 10, 20, 30 };                // inicializador abreviado
        string[] c = new string[3];              // tipos por referencia -> null
        int[] d = [1, 2, 3, 4];                  // expresión de colección (C# 12)

        Console.WriteLine($"a = {Consola.Formato(a)}  (valores por defecto)");
        Console.WriteLine($"b = {Consola.Formato(b)}");
        Console.WriteLine($"c[0] es null? {c[0] is null}");
        Console.WriteLine($"d = {Consola.Formato(d)}  Length={d.Length}");
    }
    // #endregion

    // #region Índices, fuera de rango, ^ y rangos
    static void Indices()
    {
        Consola.Seccion("Índices, ^ y rangos");

        int[] n = { 10, 20, 30, 40, 50 };
        Console.WriteLine($"n[0] = {n[0]},  n[^1] = {n[^1]} (último)");
        Console.WriteLine($"n[1..3]  = {Consola.Formato(n[1..3])}  (índice 3 excluido)");
        Console.WriteLine($"n[..2]   = {Consola.Formato(n[..2])}");
        Console.WriteLine($"n[^2..]  = {Consola.Formato(n[^2..])}");

        try
        {
            int x = n[5];                         // índice válido: 0..4
            Console.WriteLine(x);
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine($"¡Cuidado! {ex.GetType().Name}: {ex.Message}");
        }
    }
    // #endregion

    // #region Recorrido
    static void Recorrido()
    {
        Consola.Seccion("Recorrido: for vs foreach");

        double[] notas = { 8.5, 9.0, 7.5, 10.0 };

        double suma = 0;
        for (int i = 0; i < notas.Length; i++)   // necesitas el índice
            suma += notas[i];

        double maximo = double.MinValue;
        foreach (double nota in notas)           // solo lectura de cada elemento
            if (nota > maximo) maximo = nota;

        Console.WriteLine($"Promedio = {suma / notas.Length:F2}, máximo = {maximo}");
    }
    // #endregion

    // #region Multidimensional
    static void Multidimensional()
    {
        Consola.Seccion("Arreglo multidimensional (rectangular) int[,]");

        int[,] m = new int[3, 4];
        for (int fila = 0; fila < m.GetLength(0); fila++)
            for (int col = 0; col < m.GetLength(1); col++)
                m[fila, col] = (fila + 1) * (col + 1);   // tabla de multiplicar

        for (int fila = 0; fila < m.GetLength(0); fila++)
        {
            for (int col = 0; col < m.GetLength(1); col++)
                Console.Write($"{m[fila, col],4}");
            Console.WriteLine();
        }
        Console.WriteLine($"Rank={m.Rank}, Length={m.Length} (total de celdas)");
    }
    // #endregion

    // #region Escalonado
    static void Escalonado()
    {
        Consola.Seccion("Arreglo escalonado (jagged) int[][]");

        int[][] triangulo = new int[4][];
        for (int i = 0; i < triangulo.Length; i++)
        {
            triangulo[i] = new int[i + 1];       // cada fila con distinta longitud
            triangulo[i][0] = triangulo[i][i] = 1;
            for (int j = 1; j < i; j++)
                triangulo[i][j] = triangulo[i - 1][j - 1] + triangulo[i - 1][j];
        }

        foreach (int[] fila in triangulo)
            Console.WriteLine(Consola.Formato(fila));   // triángulo de Pascal
    }
    // #endregion

    // #region Métodos de Array
    static void MetodosDeArray()
    {
        Consola.Seccion("Clase System.Array: utilidades estáticas");

        int[] datos = { 42, 7, 19, 3, 25 };

        Array.Sort(datos);
        Console.WriteLine($"Sort          -> {Consola.Formato(datos)}");

        int pos = Array.BinarySearch(datos, 19);      // requiere arreglo ORDENADO
        Console.WriteLine($"BinarySearch(19) = {pos}");

        Array.Reverse(datos);
        Console.WriteLine($"Reverse       -> {Consola.Formato(datos)}");

        Console.WriteLine($"IndexOf(25)   = {Array.IndexOf(datos, 25)}");

        Array.Resize(ref datos, 7);                   // crea un arreglo NUEVO y copia: O(n)
        Console.WriteLine($"Resize(7)     -> {Consola.Formato(datos)}");

        int[] copia = new int[3];
        Array.Copy(datos, 1, copia, 0, 3);            // origen, idxOrigen, destino, idxDestino, n
        Console.WriteLine($"Copy parcial  -> {Consola.Formato(copia)}");

        Array.Clear(datos);                           // todo a valor por defecto
        Console.WriteLine($"Clear         -> {Consola.Formato(datos)}");
    }
    // #endregion

    // #region Semántica de referencia
    static void SemanticaDeReferencia()
    {
        Consola.Seccion("Los arreglos son tipos por REFERENCIA");

        int[] original = { 1, 2, 3 };
        int[] alias = original;                       // mismo objeto en el heap
        int[] clon = (int[])original.Clone();         // copia superficial

        alias[0] = 99;
        Console.WriteLine($"original = {Consola.Formato(original)}  <- cambió por el alias");
        Console.WriteLine($"clon     = {Consola.Formato(clon)}  <- independiente");
    }
    // #endregion

    // #region Covarianza
    static void CovarianzaPeligrosa()
    {
        Consola.Seccion("Covarianza de arreglos (verificada en tiempo de ejecución)");

        string[] palabras = { "uno", "dos" };
        object[] objetos = palabras;                  // el compilador lo permite...

        try
        {
            objetos[0] = 123;                         // ...pero en ejecución falla
        }
        catch (ArrayTypeMismatchException ex)
        {
            Console.WriteLine($"{ex.GetType().Name}: el arreglo real sigue siendo string[]");
        }
    }
    // #endregion
}
