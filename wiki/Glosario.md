# 🔤 Glosario

[🏠 Inicio](Home) · [📚 Referencias](Referencias)

| Término | Definición |
|---------|------------|
| **Amortizado** | Costo promedio por operación calculado sobre una *secuencia completa* de operaciones, aunque alguna individual sea cara (p. ej., `List.Add` con redimensión). |
| **Arreglo (array)** | Colección de tamaño fijo, de elementos del mismo tipo, en memoria contigua e indexada desde 0. |
| **Arreglo dinámico** | Arreglo que se redimensiona automáticamente al llenarse. Es la base de `List<T>`, `Stack<T>` y `Queue<T>`. |
| **Boxing / unboxing** | Conversión de un tipo por valor (`int`) a `object` en el *heap* y de regreso. Costoso; los genéricos lo evitan. |
| **Buffer circular** | Arreglo cuyos índices "dan la vuelta" (módulo capacidad); permite colas con operaciones O(1). |
| **Cubeta (bucket)** | Posición de una tabla hash donde se agrupan las claves cuyo hash cae allí. |
| **Capacity** | Número de elementos que una `List<T>` puede contener sin reservar un arreglo nuevo. |
| **Colisión** | Situación en que dos claves distintas producen la misma cubeta en una tabla hash. |
| **Colección** | Objeto que agrupa otros objetos y ofrece operaciones para almacenarlos, recuperarlos y recorrerlos. |
| **Complejidad temporal** | Cómo crece el tiempo de una operación al aumentar *n*, expresado con notación O. |
| **Enumerador** | Objeto que recorre una colección con `MoveNext()` y `Current`; es lo que usa `foreach`. |
| **Estable (ordenamiento)** | Un ordenamiento que conserva el orden relativo de elementos con clave igual. |
| **Estructura de datos** | Implementación concreta de un ADT: organización de memoria y algoritmos. |
| **FIFO** | *First In, First Out*: el primero en entrar es el primero en salir (cola). |
| **Factor de carga** | Razón entre entradas almacenadas y número de cubetas en una tabla hash. |
| **Función hash** | Función que convierte una clave en un entero; en C#, `GetHashCode()`. |
| **Genéricos** | Mecanismo para parametrizar tipos (`List<T>`) con seguridad de tipos en compilación. |
| **Heap (montículo, estructura)** | Árbol binario completo guardado en arreglo que mantiene el menor (o mayor) elemento en la raíz; base de `PriorityQueue`. |
| **Heap (memoria)** | Región de memoria administrada donde viven los objetos (distinta de la pila de llamadas). |
| **Igualdad por valor / por referencia** | Dos objetos son "iguales" si tienen el mismo contenido / si son el mismo objeto. |
| **Immutable (inmutable)** | Objeto que no cambia tras crearse; ideal como clave de diccionario. |
| **LIFO** | *Last In, First Out*: el último en entrar es el primero en salir (pila). |
| **Localidad de memoria** | Propiedad de acceder a datos cercanos entre sí, que aprovecha la caché de la CPU. |
| **Notación O (Big-O)** | Cota superior asintótica del crecimiento del costo de un algoritmo. |
| **RPN** | Notación polaca inversa o postfija: el operador va después de sus operandos. |
| **Tabla hash** | Estructura que ubica elementos calculando un índice a partir del hash de la clave. |
| **Tipo abstracto de datos (ADT)** | Especificación de un conjunto de operaciones y su comportamiento, sin fijar la implementación. |
| **`_version`** | Contador interno de modificaciones que usan las colecciones para detectar cambios durante una enumeración. |
