namespace Ejemplos;

/// <summary>
/// Queue&lt;T&gt;: estructura FIFO (primero en entrar, primero en salir) y PriorityQueue.
/// Wiki: https://github.com/chaotzu/colecciones-csharp/wiki/05-Colas
/// </summary>
public static class DemoColas
{
    public static void Ejecutar()
    {
        Consola.Encabezado("COLAS (Queue<T>)");
        OperacionesBasicas();
        TurnosDeBanco();
        PlanificadorRoundRobin();
        RecorridoEnAnchura();
        ColaDePrioridad();
    }

    // #region Operaciones básicas
    static void OperacionesBasicas()
    {
        Consola.Seccion("Enqueue, Peek, Dequeue, TryDequeue");

        var cola = new Queue<string>();
        cola.Enqueue("Ana");
        cola.Enqueue("Luis");
        cola.Enqueue("Marta");

        Console.WriteLine($"Count = {cola.Count}, Peek = {cola.Peek()}");
        Console.WriteLine($"Orden de enumeración (frente -> final): {Consola.Formato(cola)}");

        Console.WriteLine($"Dequeue -> {cola.Dequeue()}");
        Console.WriteLine($"Dequeue -> {cola.Dequeue()}");

        cola.Enqueue("Pedro");
        Console.WriteLine($"Estado actual: {Consola.Formato(cola)}");

        while (cola.TryDequeue(out string? atendido))
            Console.WriteLine($"Atendido: {atendido}");

        Console.WriteLine($"TryDequeue en cola vacía devuelve false (sin excepción).");
    }
    // #endregion

    // #region Turnos de banco
    static void TurnosDeBanco()
    {
        Consola.Seccion("Aplicación 1: turnos de atención");

        var turnos = new Queue<(int Numero, string Cliente)>();
        string[] clientes = { "Sofía", "Raúl", "Valeria", "Hugo" };

        for (int i = 0; i < clientes.Length; i++)
            turnos.Enqueue((i + 1, clientes[i]));

        while (turnos.Count > 0)
        {
            var (numero, cliente) = turnos.Dequeue();
            Console.WriteLine($"Turno #{numero}: pase a ventanilla, {cliente}");
        }
    }
    // #endregion

    // #region Planificador Round Robin
    static void PlanificadorRoundRobin()
    {
        Consola.Seccion("Aplicación 2: planificación Round Robin (quantum = 3)");

        const int quantum = 3;
        var listos = new Queue<Proceso>();
        listos.Enqueue(new Proceso("P1", 5));
        listos.Enqueue(new Proceso("P2", 2));
        listos.Enqueue(new Proceso("P3", 7));

        int reloj = 0;
        while (listos.TryDequeue(out Proceso? actual))
        {
            int uso = Math.Min(quantum, actual.Restante);
            reloj += uso;
            actual.Restante -= uso;

            if (actual.Restante > 0)
            {
                Console.WriteLine($"t={reloj,2}: {actual.Nombre} corrió {uso} y vuelve al final (restan {actual.Restante})");
                listos.Enqueue(actual);               // vuelve a formarse al final de la cola
            }
            else
            {
                Console.WriteLine($"t={reloj,2}: {actual.Nombre} TERMINÓ");
            }
        }
    }

    private sealed class Proceso(string nombre, int restante)
    {
        public string Nombre { get; } = nombre;
        public int Restante { get; set; } = restante;
    }
    // #endregion

    // #region BFS: recorrido en anchura
    static void RecorridoEnAnchura()
    {
        Consola.Seccion("Aplicación 3: recorrido en anchura (BFS) en un grafo");

        // Grafo como diccionario de listas de adyacencia (¡colecciones combinadas!)
        var grafo = new Dictionary<string, List<string>>
        {
            ["A"] = ["B", "C"],
            ["B"] = ["D"],
            ["C"] = ["D", "E"],
            ["D"] = ["F"],
            ["E"] = ["F"],
            ["F"] = [],
        };

        Console.WriteLine($"BFS desde A: {Consola.Formato(Bfs(grafo, "A"))}");
    }

    public static List<string> Bfs(Dictionary<string, List<string>> grafo, string inicio)
    {
        var orden = new List<string>();
        var visitados = new HashSet<string> { inicio };
        var frontera = new Queue<string>();
        frontera.Enqueue(inicio);

        while (frontera.TryDequeue(out string? nodo))
        {
            orden.Add(nodo);
            foreach (string vecino in grafo[nodo])
                if (visitados.Add(vecino))            // Add devuelve false si ya estaba
                    frontera.Enqueue(vecino);
        }
        return orden;
    }
    // #endregion

    // #region PriorityQueue
    static void ColaDePrioridad()
    {
        Consola.Seccion("PriorityQueue<TElement, TPriority>: urgencias de un hospital");

        // Prioridad MENOR = se atiende ANTES (es un min-heap)
        var urgencias = new PriorityQueue<string, int>();
        urgencias.Enqueue("Dolor de cabeza leve", 5);
        urgencias.Enqueue("Paro cardiaco", 1);
        urgencias.Enqueue("Fractura", 3);
        urgencias.Enqueue("Alergia", 4);
        urgencias.Enqueue("Hemorragia", 2);

        while (urgencias.TryDequeue(out string? paciente, out int prioridad))
            Console.WriteLine($"Prioridad {prioridad}: {paciente}");
    }
    // #endregion
}
