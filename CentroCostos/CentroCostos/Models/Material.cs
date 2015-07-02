using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CentroCostos.Models
{
    // Representa un material o un insumo
    public class Material
    {
        public int Id { get; set; }

        public string Codigo { get; set; }
        public string Descripcion_Material { get; set; }
        public string Unidad_Medida { get; set; }

        // Como se mide este costo unitario?
        // Si es piel, seria el precio por metro?
        public decimal Costo_Unitario { get; set; }
        // Como sería el consumo por par?
        // Un zapato no gastaría 2 metros de piel
        // Sería un valor menor que 1 que multiplique al costo unitario?
        public decimal Consumo_Par { get; set; }

        [Required]
        public virtual Costo Costo { get; set; }
    }

    public partial class Material
    {
        public decimal Costo_Total
        {
            get
            {
                return this.Costo_Unitario * this.Costo_Total;
            }
        }
    }
}