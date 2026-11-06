namespace Ejemplos;

/// <summary>Utilidades para que la salida de los ejemplos sea legible en clase.</summary>
public static class Consola
{
    public static void Encabezado(string titulo)
    {
        Console.WriteLine();
        Console.WriteLine(new string('=', 60));
        Console.WriteLine($"  {titulo}");
        Console.WriteLine(new string('=', 60));
    }

    public static void Seccion(string titulo)
    {
        Console.WriteLine();
        Console.WriteLine($"--- {titulo} ---");
    }

    /// <summary>Imprime cualquier secuencia como [a, b, c].</summary>
    public static string Formato<T>(IEnumerable<T> items) =>
        "[" + string.Join(", ", items) + "]";
}
