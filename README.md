# SimuRails

# Instalación

Instalar Visual Studio 2015 (2017 también funciona)
Instalar SQL Server 2012 (instancia SQLSERVER2012) 
Ejecutar script /SimuRails/db/SimuRails_DB.script.sql
Compilar, ejecutar localizado en \SimuRails\Desarrollo\bin\Debug\SimuRails.exe

# Notas a desarrollar:

Algoritmo de ejecución de Tiempo comprometido:

Situación Actual: 
Se busca el tren que esta por partir de su estación inicial según la programación con menor tiempo de partida para todos los servicios de la traza, y se calcula su recorrido de ida. Luego se avanza el reloj hasta el horario de partida del siguiente tren que cumple la anterior condición mencionada.
Esto se repite hasta llegar a t > tf.

Situación Deseada:
Se busca el tren que esta por partir de su estación inicial *O DESDE EL TALLER DE MANTENIMIENTO SEGÚN LA PROGRAMACIÓN Y LA DISPONIBILIDAD LIMITADA POR LA CANTIDAD DE TRENES* con menor tiempo de partida para todos los servicios de la traza, y se calcula su recorrido de ida *Y VUELTA*. Luego se avanza el reloj hasta el horario de partida del siguiente tren que cumple la anterior condición mencionada.
Esto se repite hasta llegar a t > tf.
