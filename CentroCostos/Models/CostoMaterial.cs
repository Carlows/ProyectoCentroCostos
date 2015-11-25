﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroCostos.Models
{
    [Table("CostoMaterial")]
    public class CostoMaterial
    {
        public int Id { get; set; }

        public decimal Consumo_Par { get; set; }
        public decimal Costo_Total { get; set; }

        public virtual Material Material { get; set; }
    }
}
