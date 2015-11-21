using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CentroCostos.Helpers
{
    public class ImageManager : IImageManager
    {
        public ImageManager() { }

        public string UploadImageToServer(HttpPostedFileBase file, string pathToCreate, string serverPath, string storagePath, string fileName)
        {
            string extension = CheckIfExtensionIsValid(file.FileName);
            CheckAndCreateDirectory(pathToCreate);

            string pathWithExtension = String.Format("{0}{1}", storagePath, extension);
            string finalPath = Path.Combine(serverPath, pathWithExtension);

            file.SaveAs(finalPath);

            return pathWithExtension;
        }

        private string CheckIfExtensionIsValid(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            if (extension.Equals(".jpg") || extension.Equals(".jpeg") || extension.Equals(".png"))
            {
                return extension;
            }

            throw new ArgumentException("El archivo no es una imagen");
        }

        private void CheckAndCreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
