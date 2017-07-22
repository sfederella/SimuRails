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
        
        public List<Coche> GetAllCoches()
        {
            return context.Coche.ToList();
        }

        public Coche GetCocheById(int id)
        {
            return context.Coche.ToList().Where(x => x.Id == id).First();
        }

        public int InsertCoche(Coche coche)
        {
            try
            {
                context.Coche.Add(coche);
                
                context.SaveChanges();
            }
            catch
            {
                throw new Exception("Error Insert Coche");

                //return -1;
            }

            return context.Coche.ToList().Find(x => x.Equals(coche)).Id;
        }

        public void DeleteCoche(Coche coche)
        {
            try
            {
                context.Coche.Remove(coche);

                context.SaveChanges();
            }
            catch 
            {
                throw new Exception("Error delete coche");
            }
        }
    }
}
