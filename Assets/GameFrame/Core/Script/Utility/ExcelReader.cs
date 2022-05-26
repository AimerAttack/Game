using System.Data;
using System.IO;
using Excel;

namespace GameFrame.Core
{
    public class ExcelReader
    {
        static DataRowCollection ReadExcel(string filePath, ref int columnnum, ref int rownum)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = excelReader.AsDataSet(); 
            //Tables[0] 下标0表示excel文件中第一张表的数据
            columnnum = result.Tables[0].Columns.Count;
            rownum = result.Tables[0].Rows.Count;
            stream.Close();
            return result.Tables[0].Rows; 
        }
    }
}