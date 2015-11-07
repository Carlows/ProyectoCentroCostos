using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroCostos.Infrastructure.Repositorios
{
    public interface ILineaRepository : IRepository<Linea, int>
    {
        Linea Find(string linea);
        void CreateMultipleLineas(IEnumerable<DataRow> rows);
    }
}
