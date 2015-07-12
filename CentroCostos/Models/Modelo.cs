using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentroCostos.Models
{
    // Representa un modelo de zapato
    // cada ficha tecnica representa un modelo, asi que la ficha tecnica se representara con esta entidad
    [Table("Modelo")]
    public class Modelo
    {
        public Modelo()
        {
            this.Fecha_Creacion = DateTime.Now;
            this.Fecha_Ultima_Modificacion = DateTime.Now;
        }

        public int Id { get; set; }

        public string Codigo { get; set; }
        public string Horma { get; set; }
        public string Planta { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public DateTime Fecha_Ultima_Modificacion { get; set; }
        public string Tipo_Suela { get; set; }
        public Numeracion Numeracion { get; set; }
        public string URL_Imagen { get; set; }

        // A que se refiere con pieza?
        public string Pieza { get; set; }
        public string Color { get; set; }

        [Required]
        public virtual Linea Linea { get; set; }

        public IEnumerable<Material> Materiales_Produccion { get; set; }
    }

    public class Numeracion
    {
        public byte Menor { get; set; }
        public byte Mayor { get; set; }
    }
}