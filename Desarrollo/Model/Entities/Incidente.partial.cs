namespace SimuRails.Model.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Incidente
    {
        public bool Ocurre()
        {
            Random random = new Random();

            if (random.Next(0, 100) <= ProbabilidadOcurrencia)
            {
                return true;
            }
            return false;
        }
    }
}