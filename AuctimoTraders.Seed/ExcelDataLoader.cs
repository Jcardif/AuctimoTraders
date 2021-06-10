using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using AuctimoTraders.Shared.Helpers;
using AuctimoTraders.Shared.Models;
using Syncfusion.XlsIO;

namespace AuctimoTraders.Seed
{
    public static class ExcelDataLoader
    {
        private static IWorkbook workbook;
        private static ExcelEngine excelEngine;
        private static IApplication application;



        public static List<UserDTO> LoadUsers(Stream stream)
        {
            Init(stream);

            var usersSheet = LoadFaultTrackerSheet();
            var users = LoadSalesPeopleData(usersSheet);
            CloseWorkBook();
            return users;
        }

        
        private static List<UserDTO> LoadSalesPeopleData(IWorksheet faultTrackerSheet)
        {
            //Access the used range of the Excel file
            var usedRange = faultTrackerSheet.UsedRange;
            var lastRow = usedRange.LastRow;

            var users = new List<UserDTO>();

            //Iterate the cells in the used range and print the cell values
            for (var row = 1; row <= lastRow; row++)
            {
                if (row == 1)
                    continue;

                var userDTO = new UserDTO()
                {
                    FirstName = faultTrackerSheet[row, 1].DisplayText.Trim(),
                    LastName = faultTrackerSheet[row, 2].DisplayText.Trim(),
                    Gender = (Gender) Enum.Parse(typeof(Gender), faultTrackerSheet[row, 3].DisplayText.Trim()),
                    Email = faultTrackerSheet[row, 4].DisplayText.Trim(),
                    DOB = DateTime.Parse(faultTrackerSheet[row, 5].DisplayText.Trim()),
                    Weight = double.Parse(faultTrackerSheet[row, 6].DisplayText.Trim().ToUpper()),
                    JoiningQuarter = (Quarter) Enum.Parse(typeof(Quarter), faultTrackerSheet[row, 7].DisplayText.Trim()),
                    JoiningYear = Convert.ToInt32(faultTrackerSheet[row, 8].DisplayText.Trim()),
                    JoiningMonth = Convert.ToInt32(faultTrackerSheet[row, 9].DisplayText.Trim()),
                    JoiningMonthName = faultTrackerSheet[row, 10].DisplayText.Trim(),
                    JoiningDay = Convert.ToInt32(faultTrackerSheet[row, 11].DisplayText.Trim()),
                    Salary = float.Parse(faultTrackerSheet[row, 12].DisplayText.Trim()),
                    PhoneNumber = faultTrackerSheet[row, 13].DisplayText.Trim(),
                    Serial = Convert.ToInt32(faultTrackerSheet[row, 14].DisplayText.Trim()),
                };

                users.Add(userDTO);
            }

            return users;
        }


        //Accessing via sheet Name
        private static IWorksheet LoadFaultTrackerSheet() => workbook.Worksheets["SalesPeople"];


        private static void CloseWorkBook()
        {
            //Close the instance of IWorkbook
            workbook.Close();

            //Dispose the instance of ExcelEngine
            excelEngine.Dispose();
        }

        private static void Init(Stream inputStream)
        {
            //Creates a new instance for ExcelEngine
            excelEngine = new ExcelEngine();

            application = excelEngine.Excel;
            application.DefaultVersion = ExcelVersion.Xlsx;

            //Loads or open an existing workbook through Open method of IWorkbooks
            workbook = excelEngine.Excel.Workbooks.Open(inputStream);
        }
    }
}