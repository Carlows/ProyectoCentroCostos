using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CentroCostos.Models
{
    // Representa un modelo de zapato
    // cada ficha tecnica representa un modelo, asi que la ficha tecnica se representara con esta entidad
    public class Modelo
    {
        public int Id { get; set; }

        public string Codigo { get; set; }
        public string Horma { get; set; }
        public string Planta { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public string Tipo_Suela { get; set; }
        public Numeracion Numeracion { get; set; }
        public string URL_Imagen { get; set; }

        // A que se refiere con pieza?
        public string Pieza { get; set; }
        // Se encuentra especificado, pero no se si haya una ficha tecnica diferente por color
        // Lo dudo.
        public string Color { get; set; }

        [Required]
        public virtual Linea Linea { get; set; }
    }

    public class Numeracion
    {
        public byte Menor { get; set; }
        public byte Mayor { get; set; }
    }
}