using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CentroCostos.Helpers
{
    public interface IImageManager
    {
        string UploadImageToServer(HttpPostedFileBase file, string pathToCreate, string serverPath, string storagePath, string fileName);
    }
}
