# SimuRails


Algoritmo de ejecución de Tiempo comprometido:

Situación Actual: 
Se busca el tren que esta por partir de su estación inicial con menor tiempo de partida para todos los servicios de la traza, y se calcula su recorrido de ida. Luego se avanza el reloj hasta el horario de partida del siguiente tren que cumple la anterior condición mencionada.
Esto se repite hasta llegar a t > tf.

Situación Deseada:
Se busca el tren que esta por partir de su estación inicial *O DESDE EL TALLER DE MANTENIMIENTO* con menor tiempo de partida para todos los servicios de la traza, y se calcula su recorrido de ida *Y VUELTA*. Luego se avanza el reloj hasta el horario de partida del siguiente tren que cumple la anterior condición mencionada.
Esto se repite hasta llegar a t > tf.
