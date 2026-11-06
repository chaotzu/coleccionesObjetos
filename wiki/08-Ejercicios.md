# 📝 8. Ejercicios y autoevaluación

← [Comparativa](07-Comparativa-y-Complejidad) · [🏠 Inicio](Home) · [🔤 Glosario](Glosario)

> Dificultad: 🟢 básico · 🟡 intermedio · 🔴 reto. Intenta cada uno **antes** de abrir las pistas.

---

## 🧱 Arreglos

1. 🟢 Declara un `int[]` con 10 números. Calcula **suma, promedio, máximo y mínimo** usando solo ciclos (sin LINQ).
2. 🟢 Imprime un arreglo **al revés** sin usar `Array.Reverse`.
3. 🟡 Escribe `static int[] Rotar(int[] a, int k)` que rote *k* posiciones a la derecha: `[1,2,3,4,5]`, k=2 → `[4,5,1,2,3]`.
4. 🟡 Dada una matriz `int[,]` de 4×4, calcula la **suma de la diagonal principal** y **transpón** la matriz.
5. 🔴 Implementa **búsqueda binaria** sin usar `Array.BinarySearch` y compara cuántas comparaciones hace contra la búsqueda lineal en un arreglo de 1,000,000 de elementos.

<details><summary>💡 Pistas</summary>

- (3) Usa `(i + k) % a.Length` para calcular la nueva posición.
- (4) La diagonal cumple `fila == columna`; para transponer, `t[j, i] = m[i, j]`.
- (5) Mantén `izq`, `der`, `medio = izq + (der - izq) / 2` (evita desbordamiento). Cada paso descarta la mitad: ~20 comparaciones para un millón.
</details>

## 📈 Listas

6. 🟢 Crea una `List<string>` de nombres; agrega, inserta al inicio, elimina uno y verifica con `Contains`.
7. 🟢 Dada una `List<int>`, elimina todos los **números pares** de dos formas distintas (`RemoveAll` y ciclo hacia atrás).
8. 🟡 Con la clase `Estudiante` del repositorio, obtén **los 3 mejores promedios** sin modificar la lista original.
9. 🟡 Escribe `static List<T> SinDuplicados<T>(List<T> origen)` que conserve el orden de primera aparición. ¿Qué colección auxiliar vuelve esto O(n)?
10. 🔴 Imprime `Count` y `Capacity` al agregar 100,000 elementos y **cuenta cuántas redimensiones** ocurren. Verifica tu predicción teórica.

<details><summary>💡 Pistas</summary>

- (8) `OrderByDescending(...).Take(3).ToList()` no modifica el original; `Sort` sí.
- (9) Un `HashSet<T>` auxiliar: `if (vistos.Add(x)) resultado.Add(x);`
- (10) Si la capacidad inicial es 4 y se duplica, necesitas ≈ log₂(100000/4) redimensiones.
</details>

## 🥞 Pilas

11. 🟢 Lee una frase y determina si es **palíndroma** usando una pila.
12. 🟢 Convierte un número decimal a **binario** con una pila (divisiones sucesivas entre 2).
13. 🟡 Extiende `EstaBalanceada` para **ignorar** delimitadores dentro de comillas `"..."`.
14. 🟡 Implementa el **historial de navegación** de un navegador con botones *Atrás* y *Adelante* (dos pilas).
15. 🔴 Convierte una expresión **infija** (`3 + 4 * 2`) a **postfija** (`3 4 2 * +`) con el algoritmo *shunting-yard* y evalúala con `EvaluarPostfija`.

<details><summary>💡 Pistas</summary>

- (12) Los residuos salen en orden inverso: justo lo que hace una pila.
- (14) Visitar una página nueva vacía la pila *Adelante*, igual que en el editor de texto.
- (15) Operadores con mayor precedencia que el del tope se apilan; con menor o igual precedencia, primero se desapilan al resultado.
</details>

## 🚶 Colas

16. 🟢 Simula una **fila de banco**: llegan clientes y se atienden en orden; imprime el tamaño de la fila en cada paso.
17. 🟢 Invierte los primeros *k* elementos de una cola usando una pila auxiliar.
18. 🟡 Implementa el **juego de la papa caliente**: *n* jugadores en círculo; cada ronda se pasa la papa *k* veces y quien la tiene sale. ¿Quién gana?
19. 🟡 Implementa tu propia `ColaCircular<T>` con arreglo, `head`, `tail` y `Count` (capacidad fija).
20. 🔴 Con `PriorityQueue`, implementa la **fila de urgencias** de un hospital con desempate por hora de llegada.

<details><summary>💡 Pistas</summary>

- (17) Saca *k* a la pila; sácalos de la pila a la cola; rota los *n − k* restantes al final.
- (18) En cada pasada: `Enqueue(Dequeue())` *k − 1* veces y luego `Dequeue()` elimina al jugador.
- (19) Índices con `% capacidad`. Distingue "llena" de "vacía" con `Count`.
- (20) Usa una **tupla como prioridad**: `(gravedad, horaLlegada)`.
</details>

## 🗝️ Diccionarios

21. 🟢 Crea un diccionario *producto → precio*; permite consultar y manejar productos inexistentes **sin excepciones**.
22. 🟢 Cuenta cuántas veces aparece cada **letra** en una frase.
23. 🟡 Dada una lista de palabras, **agrupa los anagramas** (`"roma"`, `"amor"`, `"mora"`). Pista: la clave es la palabra con sus letras ordenadas.
24. 🟡 Implementa **Two Sum**: dado `int[] nums` y un `objetivo`, devuelve los índices de dos números que sumen `objetivo` en **O(n)**.
25. 🔴 Implementa una **caché LRU** simple (capacidad fija; al llenarse se descarta el menos recientemente usado) combinando `Dictionary` + `LinkedList`.

<details><summary>💡 Pistas</summary>

- (23) `new string(palabra.OrderBy(c => c).ToArray())` como clave de `Dictionary<string, List<string>>`.
- (24) Guarda `valor → índice`; para cada `x` pregunta si `objetivo - x` ya está en el diccionario.
- (25) El diccionario da acceso O(1) al **nodo** de la lista enlazada; la lista mantiene el orden de uso.
</details>

## 🏁 Proyecto integrador: **Mini sistema de biblioteca**

Diseña una aplicación de consola que use **todas** las colecciones, y justifica por escrito cada elección con su complejidad:

| Requisito | Colección sugerida |
|-----------|--------------------|
| Catálogo de libros consultable por ISBN | `Dictionary<string, Libro>` |
| Lista de géneros disponibles (fija) | `string[]` |
| Libros prestados a cada usuario | `Dictionary<int, List<Libro>>` |
| Lista de espera por un libro | `Queue<Usuario>` |
| Historial de acciones con "deshacer préstamo" | `Stack<Accion>` |
| Evitar registrar dos veces el mismo usuario | `HashSet<int>` |

**Entregable:** código + un documento de una página con una tabla *decisión → justificación → complejidad* (usa el [árbol de decisión](07-Comparativa-y-Complejidad#73-árbol-de-decisión) y cita al menos dos fuentes de [Referencias](Referencias)).

---

## ✅ Autoevaluación (con respuestas)

**1. ¿Cuál es el costo amortizado de `List<T>.Add`?**
- a) O(n)  b) O(log n)  c) O(1)  d) O(n²)

<details><summary>Ver respuesta</summary>
<b>c) O(1) amortizado.</b> Ocasionalmente hay una redimensión O(n), pero al duplicar la capacidad el costo total de <i>n</i> inserciones es lineal (§3.2).
</details>

**2. Pones en una pila `1, 2, 3` y haces `Pop()`. ¿Qué devuelve?**
- a) 1  b) 2  c) 3  d) Lanza excepción

<details><summary>Ver respuesta</summary>
<b>c) 3.</b> LIFO: el último en entrar es el primero en salir.
</details>

**3. ¿Por qué `Queue<T>.Dequeue()` es O(1) mientras `List<T>.RemoveAt(0)` es O(n)?**

<details><summary>Ver respuesta</summary>
La cola usa un <b>buffer circular</b>: solo avanza el índice <code>head</code>. La lista debe <b>desplazar</b> todos los elementos restantes una posición a la izquierda.
</details>

**4. Una clase sin `Equals`/`GetHashCode` sobrescritos se usa como clave y `ContainsKey(new Clave(1,2))` devuelve `false` aunque exista otra con los mismos datos. ¿Por qué?**

<details><summary>Ver respuesta</summary>
Por defecto, las clases comparan por <b>referencia</b> y su hash depende de la identidad del objeto, no de su contenido. Debe sobrescribirse <code>Equals</code> y <code>GetHashCode</code> (o usar <code>record</code>) para igualdad por valor.
</details>

**5. ¿Qué excepción ocurre al hacer `lista.Remove(x)` dentro de un `foreach` sobre esa lista?**

<details><summary>Ver respuesta</summary>
<code>InvalidOperationException</code> ("Collection was modified"). El enumerador detecta que cambió la versión de la colección.
</details>

**6. ¿Qué colección usarías para atender pacientes por gravedad?**

<details><summary>Ver respuesta</summary>
<code>PriorityQueue&lt;Paciente, int&gt;</code>. Enqueue/Dequeue son O(log n) y sale primero el de menor valor de prioridad.
</details>

**7. Verdadero o falso: "un `Dictionary` siempre devuelve sus elementos en el orden en que se insertaron".**

<details><summary>Ver respuesta</summary>
<b>Falso.</b> El orden no está garantizado; tras eliminar e insertar, las entradas reutilizan huecos y el orden cambia (§6.6).
</details>

**8. ¿Qué diferencia fundamental hay entre un ADT y una estructura de datos?**

<details><summary>Ver respuesta</summary>
El ADT define <b>qué</b> operaciones ofrece y su comportamiento (el contrato); la estructura de datos es <b>cómo</b> se implementa (organización en memoria y algoritmos). Una pila (ADT) puede implementarse con un arreglo o con una lista enlazada.
</details>

---
← [Comparativa](07-Comparativa-y-Complejidad) · [🏠 Inicio](Home) · [📚 Referencias](Referencias)
