using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CentroCostos.Helpers
{
    public interface IExcelData
    {
        void setPath(string path);
        IExcelDataReader getExcelReader();
        IEnumerable<string> getWorksheetNames();
        IEnumerable<DataRow> getData(string sheet, bool firstRowIsColumnNames);
    }
}