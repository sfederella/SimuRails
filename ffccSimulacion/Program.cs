﻿using System;
using System.Linq;
using System.Windows.Forms;
using ffccSimulacion.Model.Entities;

namespace ffccSimulacion
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ffccSimulacionEntities testDb = new ffccSimulacionEntities();

            //Console.WriteLine(c.SetCoche(new Coches { Modelo = "pepito", CantidadAsientos = 100, MaximoLegalPasajeros = 100, CapacidadMaximaPasajeros = 212 }));
            //c.GetAllCoches().ForEach(x => Console.WriteLine(x.Modelo));

            
            //DataBaseManager db = new DataBaseManager();
            //db.PruebasBD();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                testDb.Coches.ToList();
            }
            catch
            {
                MessageBox.Show("No hay Conexión con la Base de Datos. Ver Archivo de Configuración.\nLa Aplicación se Cerrará.");
                return;
            }            
            Application.Run(new Escritorio());
             
             
            //Simulacion simulador = new Simulacion(1);
            //simulador.EstrategiaDeSimulacion = new TiempoComprometido(0, 100);
            //simulador.EjecutarSimulacion();   
        }

        
    }
}
