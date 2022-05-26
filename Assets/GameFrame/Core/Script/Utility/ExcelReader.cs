using System;
using System.Data;
using System.IO;
using Excel;

namespace GameFrame.Core
{
    public class ExcelReader
    {
        public static void ReadExcel(string filePath,Action<DataRow> on)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = excelReader.AsDataSet(); 
            stream.Close();
            for (int i = 1; i < result.Tables[0].Rows.Count; i++)
            {
                on(result.Tables[0].Rows[i]);
            }
        }
    }
}