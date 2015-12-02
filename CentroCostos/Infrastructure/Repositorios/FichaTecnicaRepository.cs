using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroCostos.Infrastructure.Repositorios
{
    public class FichaTecnicaRepository : BaseRepository<FichaTecnica, int>, IFichaTecnicaRepository
    {
        public FichaTecnicaRepository(ApplicationContext DbContext) : base(DbContext) { } 

        public void UpdateFichasWithDepartamento(DepartamentoProduccion departamento)
        {
            var fichas = this.FindAll();

            foreach(FichaTecnica ficha in fichas)
            {
                var materiales = ficha.MaterialesDepartamento;

                materiales.Add(new MaterialesDepartamentoProduccion()
                {
                    Departamento = departamento,
                    Ficha = ficha
                });

                Update(ficha);
            }
        }

        public decimal CalculateTotalMaterialCost(FichaTecnica ficha, bool costoDirecto) 
        {
            return ficha.MaterialesDepartamento
                .SelectMany(d => d.Materiales)
                .Where(m => m.Material.esMaterialDirecto == costoDirecto)
                .Sum(m => m.Costo_Total);
        }
    }
}
