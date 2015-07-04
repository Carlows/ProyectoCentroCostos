using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCostos.Models
{
    public partial class Material
    {
        public decimal Costo_Total
        {
            get
            {
                return Costo_Unitario * Consumo_Par;
            }
        }
    }
}