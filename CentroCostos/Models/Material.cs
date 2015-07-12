﻿using System;
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

        public decimal Costo_Unitario { get; set; }
        public decimal Consumo_Par { get; set; }

        [Required]
        public virtual Costo Costo { get; set; }
        [Required]
        public virtual CategoriaMaterial Categoria_Material { get; set; }
    }
}