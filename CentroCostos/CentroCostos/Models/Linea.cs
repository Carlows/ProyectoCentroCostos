using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCostos.Models
{
    // Representa una linea
    // tendra relacion con modelos de zapato
    public class Linea
    {
        public int Id { get; set; }

        public string Nombre_Linea { get; set; }

        public virtual IEnumerable<Modelo> Modelos_Linea { get; set; }
    }
}