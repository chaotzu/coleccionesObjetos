using System.Globalization;

namespace Ejemplos;

/// <summary>
/// Stack&lt;T&gt;: estructura LIFO (último en entrar, primero en salir).
/// Wiki: https://github.com/chaotzu/colecciones-csharp/wiki/04-Pilas
/// </summary>
public static class DemoPilas
{
    public static void Ejecutar()
    {
        Consola.Encabezado("PILAS (Stack<T>)");
        OperacionesBasicas();
        InvertirCadena();
        Delimitadores();
        DeshacerRehacer();
        PostfijaRPN();
    }

    // #region Operaciones básicas
    static void OperacionesBasicas()
    {
        Consola.Seccion("Push, Peek, Pop, TryPop");

        var pila = new Stack<string>();
        pila.Push("primero");
        pila.Push("segundo");
        pila.Push("tercero");

        Console.WriteLine($"Count = {pila.Count}, Peek = {pila.Peek()}  (no la modifica)");
        Console.WriteLine($"Orden de enumeración (tope -> fondo): {Consola.Formato(pila)}");

        while (pila.TryPop(out string? elemento))     // TryPop evita InvalidOperationException
            Console.WriteLine($"Pop -> {elemento}");

        try
        {
            pila.Pop();                               // pila vacía
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Pop en pila vacía lanza {ex.GetType().Name}");
        }

        // Ojo: el constructor que recibe una secuencia hace Push en ese orden.
        var desdeArreglo = new Stack<int>(new[] { 1, 2, 3 });
        Console.WriteLine($"new Stack<int>({{1,2,3}}).Pop() = {desdeArreglo.Pop()}");
    }
    // #endregion

    // #region Invertir una cadena
    static void InvertirCadena()
    {
        Consola.Seccion("Aplicación 1: invertir una cadena");

        string texto = "estructuras";
        var pila = new Stack<char>(texto);            // el último carácter queda en el tope
        string invertido = string.Concat(pila);       // se enumera del tope al fondo

        Console.WriteLine($"{texto} -> {invertido}");
    }
    // #endregion

    // #region Delimitadores balanceados
    static void Delimitadores()
    {
        Consola.Seccion("Aplicación 2: delimitadores balanceados");

        string[] pruebas = { "(a + b) * [c - {d / e}]", "((a + b)", "{[}]", "" };
        foreach (string p in pruebas)
            Console.WriteLine($"{(EstaBalanceada(p) ? "OK  " : "MAL ")} \"{p}\"");
    }

    public static bool EstaBalanceada(string texto)
    {
        var pila = new Stack<char>();

        foreach (char c in texto)
        {
            switch (c)
            {
                case '(' or '[' or '{':
                    pila.Push(c);
                    break;

                case ')' or ']' or '}':
                    if (!pila.TryPop(out char apertura) || !Coinciden(apertura, c))
                        return false;                 // cierre sin apertura o de otro tipo
                    break;
            }
        }
        return pila.Count == 0;                       // si sobran aperturas, no está balanceada
    }

    static bool Coinciden(char apertura, char cierre) =>
        (apertura, cierre) is ('(', ')') or ('[', ']') or ('{', '}');
    // #endregion

    // #region Deshacer / Rehacer con dos pilas
    static void DeshacerRehacer()
    {
        Consola.Seccion("Aplicación 3: deshacer / rehacer con dos pilas");

        var editor = new EditorDeTexto();
        editor.Escribir("Hola");
        editor.Escribir(", mundo");
        editor.Escribir("!!!");
        Console.WriteLine($"Texto:    \"{editor.Texto}\"");

        editor.Deshacer();
        editor.Deshacer();
        Console.WriteLine($"Deshacer x2: \"{editor.Texto}\"");

        editor.Rehacer();
        Console.WriteLine($"Rehacer x1:  \"{editor.Texto}\"");

        editor.Escribir("?");                         // una acción nueva invalida el 'rehacer'
        Console.WriteLine($"Escribir '?': \"{editor.Texto}\"  (¿puede rehacer? {editor.PuedeRehacer})");
    }

    private sealed class EditorDeTexto
    {
        private readonly Stack<string> _deshacer = new();
        private readonly Stack<string> _rehacer = new();

        public string Texto { get; private set; } = "";
        public bool PuedeRehacer => _rehacer.Count > 0;

        public void Escribir(string fragmento)
        {
            _deshacer.Push(Texto);                    // guarda el estado ANTERIOR
            Texto += fragmento;
            _rehacer.Clear();
        }

        public void Deshacer()
        {
            if (_deshacer.TryPop(out string? anterior))
            {
                _rehacer.Push(Texto);
                Texto = anterior;
            }
        }

        public void Rehacer()
        {
            if (_rehacer.TryPop(out string? siguiente))
            {
                _deshacer.Push(Texto);
                Texto = siguiente;
            }
        }
    }
    // #endregion

    // #region Evaluación de notación postfija (RPN)
    static void PostfijaRPN()
    {
        Consola.Seccion("Aplicación 4: evaluar expresión postfija (RPN)");

        string[] expresiones = { "3 4 +", "5 1 2 + 4 * + 3 -", "10 2 8 * + 3 -" };
        foreach (string e in expresiones)
            Console.WriteLine($"{e,-20} = {EvaluarPostfija(e)}");
    }

    public static double EvaluarPostfija(string expresion)
    {
        var operandos = new Stack<double>();

        foreach (string token in expresion.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            if (double.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out double valor))
            {
                operandos.Push(valor);
                continue;
            }

            double b = operandos.Pop();               // el 2.º operando sale primero
            double a = operandos.Pop();
            operandos.Push(token switch
            {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" => a / b,
                _ => throw new ArgumentException($"Operador no soportado: {token}")
            });
        }
        return operandos.Pop();
    }
    // #endregion
}
