﻿using System;
using SimuRails.Model.Entities;
using System.Collections.Generic;

namespace SimuRails.Model.Simulacion
{
    public struct Parada
    {
        public Estacion nodo;
        public bool para;
    }

    public struct Flujo_Pasajeros
    {
        public int pasajerosQueSubieronAlTren;
        public int pasajerosQueNoSubieronAlTren;
    }

    public struct Estacion_Info
    {
        public int id_estacion;
        public int ultima_Atencion;
        public int tiempo_comprometido;
        public int gente_esperando;
    }

    /*todos los tiempos de la estructura estan en minutos*/
    public struct ResultadoFormacion
    {
        public int id_formacion;
        public string nombreFormacion;
        public int distanciaTotalRecorrida;
        public int pasajerosTotalesTransportados;
        public int vecesSuperoCapLegal;
        public int vecesNoHabiaPasajerosParados;
        public int tiempoTotalDemoradoIncidente;
        public int tiempoTotalDemoradoAtencion;
        public int tiempoTotalEnMovimiento;
    }

    public struct ResultadoSimulacion
    {
        public int id;
        public string nombre;
        public DateTime fecha;
        public double porcentajeSobrecarga;
        public double promedioDemoraIncidentes;
        public double promedioPasajeros;
        public double promedioDemoraAtencion;
        public double costoKm;
        public double costoPasajero;
        public double consumoElectricoKm;
        public double consumoDieselKm;
        public double consumoElectricoPasajero;
        public double consumoDieselPasajero;
        //necesito estos dos campos mas para generar el informe
        public string nombreSimulacion;
        public int idTraza;
        public int tiempoTotal;

        public List<ResultadoServicio> resultadosServicios;
        public int tiempoSimulado;
    }
}
