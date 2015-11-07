using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace CentroCostos.Infrastructure.Repositorios
{
    public class ModeloRepository : BaseRepository<Modelo, int>, IModeloRepository
    {
        public ModeloRepository(ApplicationContext dbContext, ILineaRepository lineasRepo)
            : base(dbContext)
        {
            _lineasDb = lineasRepo;
        }

        private readonly ILineaRepository _lineasDb;

        public Modelo Find(string codigo)
        {
            return DbContext.Modelos.Where(m => m.Codigo.StartsWith(codigo)).Single();
        }

        // devuelve la URL de la imagen para ser guardada en la BD
        public string UploadImage(string codigo, HttpPostedFileBase file, string serverPath)
        {
            if (file == null || file.ContentLength == 0)
            {
                throw new ArgumentNullException("file");
            }

            string extension = Path.GetExtension(file.FileName);
            if (extension.Equals(".jpg") || extension.Equals(".jpeg") || extension.Equals(".png"))
            {
                string dirPath = Path.Combine(serverPath, "Imgs");

                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);

                string ext = Path.GetExtension(file.FileName);
                string fileName = string.Format("{0}{1}", codigo, ext);
                string userFolder = Path.Combine("Imgs", fileName);
                string path = Path.Combine(serverPath, userFolder);

                file.SaveAs(path);

                return userFolder;
            }
            else
            {
                throw new ArgumentException("file");
            }
        }


        public void CreateMultipleModelos(IEnumerable<System.Data.DataRow> rows)
        {
            IList<Modelo> modelos = new List<Modelo>();
            foreach (DataRow row in rows)
            {
                string nombreLinea = row["Linea"].ToString();
                var linea = DbContext.Lineas.Where(l => l.Nombre_Linea == nombreLinea).SingleOrDefault();

                if (linea == null)
                {
                    throw new ArgumentException("Linea no existe");
                    return;
                }
                else
                {
                    modelos.Add(new Modelo()
                    {
                        Codigo = row["Codigo"].ToString(),
                        Horma = row["Horma"].ToString(),
                        Planta = row["Planta"].ToString(),
                        Tipo_Suela = row["Suela"].ToString(),
                        Numeracion = new Numeracion
                        {
                            Menor = byte.Parse(row["NumeracionMenor"].ToString()),
                            Mayor = byte.Parse(row["NumeracionMayor"].ToString())
                        },
                        Pieza = int.Parse(row["Pieza"].ToString()),
                        Color = row["Color"].ToString(),
                        Linea = linea
                    });
                }
            }

            this.CreateMultiple(modelos);
        }


        public void CreateMultipleModelos(IEnumerable<DataRow> rows, int lineaId)
        {
            IList<Modelo> modelos = new List<Modelo>();
            var linea = _lineasDb.GetById(lineaId);

            if(linea == null)
            {
                throw new ArgumentException("Linea no existe");
            }
            else
            {
                foreach (DataRow row in rows)
                {
                    modelos.Add(new Modelo()
                    {
                        Codigo = row["Codigo"].ToString(),
                        Horma = row["Horma"].ToString(),
                        Planta = row["Planta"].ToString(),
                        Tipo_Suela = row["Suela"].ToString(),
                        Numeracion = new Numeracion
                        {
                            Menor = byte.Parse(row["NumeracionMenor"].ToString()),
                            Mayor = byte.Parse(row["NumeracionMayor"].ToString())
                        },
                        Pieza = int.Parse(row["Pieza"].ToString()),
                        Color = row["Color"].ToString(),
                        Linea = linea
                    });
                }

                this.CreateMultiple(modelos);
            }            
        }
    }
}