using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroCostos.Models.ViewModels
{
    public class AgregarMaterialDepartamentoViewModel
    {
        public int departamentoId { get; set; }
        public string codigoMaterial { get; set; }
        public decimal consumoMaterial { get; set; }
    }
}
