using CentroCostos.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CentroCostos.Infrastructure.Repositorios
{
    public class ModeloRepository : BaseRepository<Modelo, int>, IModeloRepository
    {
        public ModeloRepository(ApplicationContext dbContext)
            : base(dbContext)
        {
        }

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
    }
}