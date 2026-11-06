# 🧱 2. Arreglos (`T[]`)

← [Introducción](01-Introduccion-a-las-Colecciones) · [🏠 Inicio](Home) · Siguiente: [📈 Listas →](03-Listas)

> **Objetivo:** dominar la estructura más básica y eficiente: secuencia de tamaño fijo en memoria contigua.
>
> 💻 **Código de este tema:** [`DemoArreglos.cs`](https://github.com/chaotzu/colecciones-csharp/blob/main/Ejemplos/01_Arreglos/DemoArreglos.cs) → `dotnet run -- 1`

---

## 2.1 Definición

Un **arreglo** es una colección de **tamaño fijo** que almacena elementos **del mismo tipo** en posiciones **consecutivas de memoria**, identificados por un **índice** que en C# comienza en **0** (Cormen et al., 2022, cap. 10; Microsoft, s. f.-b).

Como los elementos son contiguos y del mismo tamaño, la posición de cada uno se calcula con aritmética simple:

```
dirección(i) = dirección_base + i × tamaño_del_elemento
```

Por eso acceder a `a[i]` cuesta **O(1)**: no hay que recorrer nada.

```
Índice:     0     1     2     3     4
          ┌─────┬─────┬─────┬─────┬─────┐
int[] a   │ 10  │ 20  │ 30  │ 40  │ 50  │   Length = 5
          └─────┴─────┴─────┴─────┴─────┘
            ▲                         ▲
           a[0]                   a[^1] = a[4]
```

🔬 **En C#, todo arreglo es un objeto** que hereda de `System.Array` y vive en el *heap*; la variable guarda una **referencia**. Los elementos de un `int[]` se guardan directamente (tipos por valor); en un `string[]` se guardan referencias.

## 2.2 Declaración e inicialización

```csharp
int[]    a = new int[5];              // [0, 0, 0, 0, 0]   valores por defecto
int[]    b = { 10, 20, 30 };          // inicializador abreviado
int[]    c = new int[] { 1, 2, 3 };   // explícito
int[]    d = [1, 2, 3, 4];           // expresión de colección (C# 12)
string[] s = new string[3];           // [null, null, null]  (tipos por referencia)
```

| Tipo de elemento | Valor por defecto |
|------------------|-------------------|
| numéricos | `0` |
| `bool` | `false` |
| `char` | `'\0'` |
| referencias (`string`, clases) | `null` |

## 2.3 Acceso, índices desde el final y rangos

```csharp
int[] n = { 10, 20, 30, 40, 50 };

n[0];        // 10  primer elemento
n[^1];       // 50  último (^1 = "1 desde el final")
n[1..3];     // [20, 30]  rango: incluye 1, EXCLUYE 3
n[..2];      // [10, 20]
n[^2..];     // [40, 50]
```

⚠️ **Error clásico:** `n[5]` en un arreglo de 5 elementos lanza `IndexOutOfRangeException`. Los índices válidos son `0` a `Length - 1`. C# verifica los límites en cada acceso, lo que evita corrupción de memoria (un problema histórico en C/C++).

> Los rangos `n[1..3]` **copian** los elementos a un arreglo nuevo. Para ver una porción sin copiar, existe `Span<T>` (tema avanzado).

## 2.4 Recorrido

```csharp
// for: cuando necesitas el índice o quieres modificar elementos
for (int i = 0; i < notas.Length; i++)
    suma += notas[i];

// foreach: cuando solo lees cada elemento
foreach (double nota in notas)
    if (nota > maximo) maximo = nota;
```

💡 Con `foreach` **no puedes** asignar a la variable de iteración (`nota = 10;` no compila). Si necesitas modificar elementos de un arreglo, usa `for`.

## 2.5 Arreglos multidimensionales vs. escalonados

| | Rectangular `int[,]` | Escalonado (*jagged*) `int[][]` |
|--|----------------------|----------------------------------|
| Forma | Todas las filas con igual longitud | Cada fila puede tener longitud distinta |
| Creación | `new int[3, 4]` | `new int[3][]` y luego cada fila |
| Acceso | `m[fila, col]` | `m[fila][col]` |
| Memoria | Un solo bloque | Un arreglo de referencias a arreglos |
| Uso típico | Matrices, tableros | Triángulo de Pascal, listas de longitud variable |

```csharp
int[,] m = new int[3, 4];
m.GetLength(0);   // 3 filas
m.GetLength(1);   // 4 columnas
m.Length;         // 12 celdas en total
```

Ejemplo completo: tabla de multiplicar y triángulo de Pascal en [`DemoArreglos.cs`](https://github.com/chaotzu/colecciones-csharp/blob/main/Ejemplos/01_Arreglos/DemoArreglos.cs) (métodos `Multidimensional` y `Escalonado`).

## 2.6 La clase `System.Array`: utilidades

| Método | Qué hace | Costo |
|--------|----------|-------|
| `Array.Sort(a)` | Ordena en sitio | O(n log n) |
| `Array.BinarySearch(a, x)` | Busca en arreglo **ya ordenado** | O(log n) |
| `Array.IndexOf(a, x)` | Búsqueda lineal | O(n) |
| `Array.Reverse(a)` | Invierte en sitio | O(n) |
| `Array.Copy(src, i, dst, j, n)` | Copia un tramo | O(n) |
| `Array.Resize(ref a, m)` | Crea un arreglo **nuevo** de tamaño *m* y copia | O(n) |
| `Array.Clear(a)` | Pone todos los elementos en su valor por defecto | O(n) |

🔬 `Array.Sort` usa un algoritmo híbrido (*introsort*: quicksort + heapsort + insertion sort) cuyo peor caso sigue siendo O(n log n) (Microsoft, s. f.-b; Cormen et al., 2022, cap. 7 y 6). **No es estable.**

⚠️ **`Array.Resize` no "agranda" el arreglo**: es imposible porque los vecinos en memoria podrían estar ocupados. Reserva uno nuevo, copia todo y la variable pasa a apuntar al nuevo. Por eso, si necesitas crecer a menudo, usa [`List<T>`](03-Listas).

## 2.7 Semántica de referencia

```csharp
int[] original = { 1, 2, 3 };
int[] alias = original;                 // MISMO objeto
int[] clon  = (int[])original.Clone();  // copia superficial (nuevo objeto)

alias[0] = 99;
// original -> [99, 2, 3]   (cambió)
// clon     -> [1, 2, 3]    (independiente)
```

💡 `Clone()` y `Array.Copy` hacen copia **superficial**: si el arreglo contiene objetos, se copian las *referencias*, no los objetos.

## 2.8 🔬 Covarianza de arreglos: una trampa heredada

C# permite asignar un `string[]` a una variable `object[]`. El compilador lo acepta, pero el tipo real del arreglo sigue siendo `string[]`, así que escribir un `int` en él falla **en ejecución** con `ArrayTypeMismatchException`:

```csharp
string[] palabras = { "uno", "dos" };
object[] objetos = palabras;   // permitido (covarianza)
objetos[0] = 123;              // 💥 ArrayTypeMismatchException
```

Es una decisión de diseño del lenguaje que traslada una verificación de tipos del compilador al tiempo de ejecución. Las colecciones genéricas como `List<T>` **no** son covariantes en escritura, y por eso son más seguras (Albahari, 2023).

## 2.9 Complejidad

| Operación | Costo | Comentario |
|-----------|:-----:|------------|
| Acceso por índice | **O(1)** | Aritmética de direcciones |
| Búsqueda lineal | O(n) | Recorre hasta encontrar |
| Búsqueda binaria | O(log n) | Solo si está ordenado |
| Insertar / eliminar en medio | O(n) | Hay que desplazar elementos (y el tamaño es fijo) |
| Cambiar de tamaño | O(n) | Crea un arreglo nuevo |
| Recorrer | O(n) | |

## 2.10 ¿Cuándo usar un arreglo?

✅ **Úsalo cuando…**
- El tamaño se conoce y no cambia (días de la semana, tablero 8×8).
- Necesitas máximo rendimiento y mínimo uso de memoria (datos numéricos, buffers).
- Trabajas con APIs que lo exigen (`string[] args`, `Main`, `params`).

❌ **Evítalo cuando…**
- La cantidad de elementos cambia con frecuencia → [`List<T>`](03-Listas).
- Necesitas buscar por clave → [`Dictionary`](06-Diccionarios).

## 🧠 Preguntas para pensar

1. ¿Por qué es O(1) acceder a `a[i]` pero O(n) insertar en `a[0]`?
2. ¿Qué diferencia hay entre `int[] b = a;` y `int[] b = (int[])a.Clone();`?
3. Un arreglo `int[1000, 1000]` y uno `int[1000][]` guardan la misma cantidad de números. ¿En qué se diferencia su organización en memoria?

📝 Practica en [Ejercicios](08-Ejercicios#-arreglos).

---
← [Introducción](01-Introduccion-a-las-Colecciones) · [🏠 Inicio](Home) · Siguiente: [📈 Listas →](03-Listas) · [📚 Referencias](Referencias)
