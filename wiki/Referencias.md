# 📚 Referencias

[🏠 Inicio](Home) · [🔤 Glosario](Glosario)

Las citas dentro de los apuntes siguen el formato **APA 7.ª edición** (autor, año). Las fichas completas están aquí, ordenadas alfabéticamente.

---

## 📖 Libros y artículos

Albahari, J. (2023). *C# 12 in a nutshell: The definitive reference*. O'Reilly Media.
> Capítulo 7 (*Collections*): panorama de interfaces y colecciones de .NET, genéricas y no genéricas.

Cormen, T. H., Leiserson, C. E., Rivest, R. L., & Stein, C. (2022). *Introduction to algorithms* (4.ª ed.). MIT Press.
> Notación asintótica (cap. 3); heapsort y colas de prioridad (cap. 6); quicksort (cap. 7); ordenamiento y estabilidad (cap. 8); estructuras elementales: arreglos, pilas, colas (cap. 10); tablas hash (cap. 11); análisis amortizado y tablas dinámicas (cap. 16); BFS (cap. 20). 
> *Nota:* la numeración de capítulos corresponde a la 4.ª edición; en la 3.ª edición algunos capítulos tienen otro número (p. ej., análisis amortizado es el cap. 17).

Liskov, B., & Zilles, S. (1974). Programming with abstract data types. *ACM SIGPLAN Notices, 9*(4), 50–59. https://doi.org/10.1145/942572.807045
> Artículo fundacional sobre tipos abstractos de datos.

Sedgewick, R., & Wayne, K. (2011). *Algorithms* (4.ª ed.). Addison-Wesley.
> §1.3 *Bags, queues, and stacks*; §1.4 análisis de algoritmos; cap. 3 tablas de símbolos.

Silberschatz, A., Galvin, P. B., & Gagne, G. (2018). *Operating system concepts* (10.ª ed.). Wiley.
> Planificación de CPU, incluyendo *Round Robin*.

Troelsen, A., & Japikse, P. (2022). *Pro C# 10 with .NET 6: Foundational principles and practices in programming* (11.ª ed.). Apress.
> Capítulo sobre colecciones y genéricos (boxing, seguridad de tipos).

## 🌐 Documentación oficial de Microsoft

> Los enlaces apuntan a la versión en inglés; en la parte inferior de cada página de Microsoft Learn puedes cambiar el idioma.

Microsoft. (s. f.-a). *Collections and data structures*. Microsoft Learn. https://learn.microsoft.com/en-us/dotnet/standard/collections/

Microsoft. (s. f.-b). *Array class (System)*. Microsoft Learn. https://learn.microsoft.com/en-us/dotnet/api/system.array

Microsoft. (s. f.-c). *List\<T\> class (System.Collections.Generic)*. Microsoft Learn. https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1

Microsoft. (s. f.-d). *Dictionary\<TKey,TValue\> class (System.Collections.Generic)*. Microsoft Learn. https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2

## 🔎 Lecturas complementarias (no citadas directamente)

- Microsoft. (s. f.). *Selecting a collection class*. Microsoft Learn. https://learn.microsoft.com/en-us/dotnet/standard/collections/selecting-a-collection-class
- Documentación de `Stack<T>`: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1
- Documentación de `Queue<T>`: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1
- Documentación de `PriorityQueue<TElement,TPriority>`: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.priorityqueue-2
- Código fuente de .NET (para ver `List.cs`, `Dictionary.cs`, etc.): https://github.com/dotnet/runtime
- Skiena, S. S. (2020). *The algorithm design manual* (3.ª ed.). Springer.
- Knuth, D. E. (1997). *The art of computer programming: Vol. 1. Fundamental algorithms* (3.ª ed.). Addison-Wesley. (Sección 2.2, listas lineales: pilas y colas.)
- Knuth, D. E. (1998). *The art of computer programming: Vol. 3. Sorting and searching* (2.ª ed.). Addison-Wesley. (Sección 6.4, *hashing*.)
- BenchmarkDotNet, para mediciones rigurosas de rendimiento: https://benchmarkdotnet.org/

---

## ✍️ Nota sobre el rigor de estos apuntes

- Las afirmaciones sobre **complejidad** provienen de la bibliografía de algoritmos (Cormen et al., 2022; Sedgewick & Wayne, 2011) y de la documentación oficial de .NET.
- Los detalles de **implementación interna** (crecimiento por duplicación de `List<T>`, buffer circular de `Queue<T>`, encadenamiento con `buckets[]`/`entries[]` en `Dictionary`) corresponden al código abierto de .NET y **pueden cambiar entre versiones**: la documentación oficial no los promete como contrato. Para verificarlos, consulta el repositorio `dotnet/runtime`. Los valores de `Capacity` que ves en los ejemplos fueron obtenidos ejecutando el código con .NET 8.
- Antes de citar una edición, páginas o capítulos en un trabajo formal, **verifícalos contra tu ejemplar** (la paginación y numeración varían entre ediciones).
