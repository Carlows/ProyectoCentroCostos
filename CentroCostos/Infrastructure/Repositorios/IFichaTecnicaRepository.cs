using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroCostos.Infrastructure.Repositorios
{
    public interface IFichaTecnicaRepository : IRepository<FichaTecnica, int>
    {
        void UpdateFichasWithDepartamento(DepartamentoProduccion departamento);
        decimal CalculateTotalMaterialCost(FichaTecnica ficha, bool costoDirecto);
    }
}
