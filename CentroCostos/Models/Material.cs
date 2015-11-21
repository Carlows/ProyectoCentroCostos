using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentroCostos.Models
{
    // Representa un material o un insumo
    [Table("Material")]
    public partial class Material
    {
        public int Id { get; set; }

        public string Codigo { get; set; }
        public string Descripcion_Material { get; set; }
        public string Unidad_Medida { get; set; }
        public bool esMaterialDirecto { get; set; }

        public decimal Costo_Unitario { get; set; }

        // Este consumo se debe añadir cuando el material se asigna a un modelo especifico
        // so wtf are you going to do with this
        // tal vez una entidad nueva: Consumo_Modelo_Material
        public decimal Consumo_Par { get; set; }

        [Required]
        public virtual CategoriaMaterial Categoria_Material { get; set; }
    }
}