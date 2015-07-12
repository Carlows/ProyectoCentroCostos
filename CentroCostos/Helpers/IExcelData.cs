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
        IExcelDataReader getExcelReader();
        IEnumerable<string> getWorksheetNames();
        IEnumerable<DataRow> getData(string sheet, bool firstRowIsColumnNames);
    }
}