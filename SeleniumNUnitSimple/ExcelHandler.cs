using System.IO;
using System.Collections.Generic;
using ClosedXML.Excel;

namespace SeleniumNUnitSimple
{
    class ExcelHandler
    {
        private string plainFileName;
        private string fullFileName;

        public ExcelHandler(string excelfile)
        {
            plainFileName = excelfile;
            string execPath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            string nameSpacePath = Directory.GetParent(Directory.GetParent(execPath).FullName).FullName;
            string testDataPath = nameSpacePath + @"\TestData\";
            fullFileName = testDataPath + plainFileName;
        }


        public List<SearchText> getSearchTxts(string testSheet, SearchText.SearchType searchType)
        {
            List<SearchText> searchObjects = new List<SearchText>();
            XLWorkbook myBook = new XLWorkbook(fullFileName);
            string sheetname = testSheet;
            IXLWorksheet worksheet = myBook.Worksheet(sheetname);
            int firstrow = worksheet.FirstRowUsed().RowNumber();
            int firstDataRow = firstrow + 1; //First row is column names, not used in tests
            int lastrow = worksheet.LastRowUsed().RowNumber();
            for (int i = firstDataRow; i <= lastrow; i++)
            {
                int id = worksheet.Row(i).Cell(1).GetValue<int>();
                string full = worksheet.Row(i).Cell(2).GetValue<string>();
                string wildcard = worksheet.Row(i).Cell(3).GetValue<string>();
                string searchTxt = worksheet.Row(i).Cell(2).GetValue<string>();
                SearchText aSearchTxt = new SearchText(id, full, wildcard);
                searchObjects.Add(aSearchTxt);
            }
            return searchObjects;
        }
    }
}
