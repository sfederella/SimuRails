using System;
using System.Collections.Generic;
using System.Linq;
using SimuRails.Model.Entities;

namespace SimuRails.Model.DataBase
{
    public class Context
    {
        private SimuRailsEntities context;
        
        public Context()
        {
            context = new SimuRailsEntities();
        }


        //Coches
        
        public List<Coches> GetAllCoches()
        {
            return context.Coches.ToList();
        }

        public Coches GetCocheById(int id)
        {
            return context.Coches.ToList().Where(x => x.Id == id).First();
        }

        public int InsertCoche(Coches coche)
        {
            try
            {
                context.Coches.Add(coche);
                
                context.SaveChanges();
            }
            catch
            {
                throw new Exception("Error Insert Coche");

                //return -1;
            }

            return context.Coches.ToList().Find(x => x.Equals(coche)).Id;
        }

        public void DeleteCoche(Coches coche)
        {
            try
            {
                context.Coches.Remove(coche);

                context.SaveChanges();
            }
            catch 
            {
                throw new Exception("Error delete coche");
            }
        }
    }
}
