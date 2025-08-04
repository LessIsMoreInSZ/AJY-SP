using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.ExcelHelper
{
    public class ExcelReaderClass
    {
        public XLWorkbook workbook;
        public ExcelReaderClass(string filepath)
        {
            try
            {
                workbook = new XLWorkbook(filepath);
            }
            catch (Exception ex)
            {
                throw new Exception("ExcelReaderClass::" + ex.Message);
            }
        }
        public List<string> GetSheetNames()
        {
           return workbook.Worksheets.Select(m => m.Name).ToList();
        }
        public List<ErrorReportModel> GetErrorReportModels(string sheetName, int startRow = 2)
        {
            var results = new List<ErrorReportModel>();
            IXLWorksheet worksheet;
            var flag = workbook.TryGetWorksheet(sheetName, out worksheet);
            if (!flag) { throw new Exception($"Excel sheet {sheetName} not found"); }
            var rowcount = worksheet.LastRowUsed().RowNumber();
            for (int i = startRow; i <= rowcount; i++)
            {
                var model = new ErrorReportModel
                {
                    Order = i - startRow + 1,
                    AlarmCode = GetCellValue(worksheet, i, 3),
                    AlarmInfo = GetCellValue(worksheet, i, 5),
                    AlarmLevel = GetCellValue(worksheet, i, 4)
                };
                results.Add(model);
            }
            return results;
        }
        public List<IOReportModel> GetIOReportModels(string sheetName, int startCol = 2, int startRow = 2, bool dce = false)
        {
            var results = new List<IOReportModel>();
            IXLWorksheet worksheet;
            var flag = workbook.TryGetWorksheet(sheetName, out worksheet);
            if (!flag) { throw new Exception($"Excel sheet {sheetName} not found"); }
            var rowcount = worksheet.LastRowUsed().RowNumber();
            for (int i = startRow; i <= rowcount; i++)
            {
                var ioname = GetCellValue(worksheet, i, startCol);
                if (dce)
                    ioname = CovertIoName2(ioname);
                else
                    ioname = CovertIoName(ioname);
                var model = new IOReportModel
                {
                    IoName = ioname,
                    IoText = GetCellValue(worksheet, i, startCol + 1)
                };
                results.Add(model);
            }
            return results;
        }
        private string CovertIoName(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return "";
            }
            else
            {
                var splits = input.Split('_').ToList();
                var s = splits[splits.Count - 1];
                s = s.Trim().Replace("[", ".");
                s = s.Replace("]", "");
                return s;
            }
        }
        private string CovertIoName2(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return "";
            }
            else
            {
                var splits = input.Split('.').ToList();
                var s = splits[splits.Count - 1];
                s = s.Trim().Replace("[", ".");
                s = s.Replace("]", "");
                return s;
            }
        }
        public string GetCellValue(IXLWorksheet worksheet, int row, int col)
        {
            var cell = CommonFunction.GetExcelCell(row, col);
            return worksheet.Cell(cell).Value.ToString();

        }

    }
    public class ErrorReportModel
    {
        public int Order { set; get; }
        public string AlarmCode { set; get; }
        public string AlarmInfo { set; get; }
        public string AlarmLevel{ set; get; }
    }

    public class IOReportModel
    {
        public string IoName { set; get; }
        public string IoText { set; get; }
    }
}
