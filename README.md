# SimuRails


Algoritmo de ejecución de Tiempo comprometido:

Situación Actual: 
Se busca el tren que esta por partir de su estación inicial según la programación con menor tiempo de partida para todos los servicios de la traza, y se calcula su recorrido de ida. Luego se avanza el reloj hasta el horario de partida del siguiente tren que cumple la anterior condición mencionada.
Esto se repite hasta llegar a t > tf.

Situación Deseada:
Se busca el tren que esta por partir de su estación inicial *O DESDE EL TALLER DE MANTENIMIENTO SEGÚN LA PROGRAMACIÓN Y LA DISPONIBILIDAD LIMITADA POR LA CANTIDAD DE TRENES* con menor tiempo de partida para todos los servicios de la traza, y se calcula su recorrido de ida *Y VUELTA*. Luego se avanza el reloj hasta el horario de partida del siguiente tren que cumple la anterior condición mencionada.
Esto se repite hasta llegar a t > tf.
