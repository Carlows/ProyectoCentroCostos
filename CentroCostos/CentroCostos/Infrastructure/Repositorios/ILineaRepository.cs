using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroCostos.Infrastructure.Repositorios
{
    interface ILineaRepository
    {
        Linea Find(string linea);
        IEnumerable<Modelo> GetModelos(int linea);
    }
}
