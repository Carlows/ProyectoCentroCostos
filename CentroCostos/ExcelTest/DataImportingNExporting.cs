using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentroCostos.Infrastructure;
using CentroCostos.Helpers;

namespace ExcelTest
{
    class DataImportingNExporting
    {
        static void Main(string[] args)
        {
            string path = @"C:\ExcelTest2.xlsx";
            IExcelData manager = new ExcelData(path);

            var hojasDeCalculo = manager.getWorksheetNames();

            Console.WriteLine("Hojas de calculo en el documento");
            foreach (string hoja in hojasDeCalculo)
                Console.WriteLine(hoja);
            Console.WriteLine("------------------------");

            var data = manager.getData("Prueba1", true);

            Console.WriteLine("Datos");
            foreach (var row in data)
            {
                Console.WriteLine("{0} {1}", row["Nombre"].ToString(), row["Apellido"].ToString());
            }

            Console.ReadKey();
        }
    }
}
