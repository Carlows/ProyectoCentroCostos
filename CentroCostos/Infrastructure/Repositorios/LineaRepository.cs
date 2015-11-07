using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CentroCostos.Infrastructure.Repositorios
{
    public class LineaRepository : BaseRepository<Linea, int>, ILineaRepository
    {
        public LineaRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public Linea Find(string linea)
        {
            return DbContext.Lineas.Where(l => l.Nombre_Linea.StartsWith(linea)).Single();
        }
        
        public void CreateMultipleLineas(IEnumerable<DataRow> rows)
        {
            IList<Linea> lineas = new List<Linea>();
            foreach(DataRow row in rows)
            {
                lineas.Add(new Linea()
                {
                    Nombre_Linea = row["Codigo"].ToString()
                });
            }

            this.CreateMultiple(lineas);
        }
    }
}