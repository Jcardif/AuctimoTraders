using System;
using System.Collections.Generic;
using System.IO;
using AuctimoTraders.Shared.Helpers;
using AuctimoTraders.Shared.Models;
using Syncfusion.XlsIO;

namespace AuctimoTraders.Seed.Helpers
{
    public static class ExcelDataLoader
    {
        private static IWorkbook workbook;
        private static ExcelEngine excelEngine;
        private static IApplication application;

        
        public static List<UserDTO> LoadUsers(Stream stream, UserRole role)
        {
            Init(stream);

            List<UserDTO> users = new List<UserDTO>();

            switch (role)
            {
                case UserRole.SalesPerson:
                    var usersWorksheet = LoadSalesPeopleSheet();
                    users = LoadSalesPeopleData(usersWorksheet, role);
                    break;

                case UserRole.CountryManager:
                    var countryManagersSheet = LoadCountryManagersSheet();
                    users = LoadSalesPeopleData(countryManagersSheet, role);
                    break;

                case UserRole.RegionalManager:
                    var regionalManagersSheet = LoadRegionalManagersSheet();
                    users = LoadSalesPeopleData(regionalManagersSheet, role);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(role), role, null);
            }


            CloseWorkBook();
            return users;
        }

        public static List<SeedSale> LoadSales(Stream stream)
        {
            Init(stream);

            var salesWorkSheet = LoadSalesSheet();
            var sales = LoadSalesData(salesWorkSheet);
            CloseWorkBook();
            return sales;
        }

        private static List<SeedSale> LoadSalesData(IWorksheet salesWorkSheet)
        {
            //Access the used range of the Excel file
            var usedRange = salesWorkSheet.UsedRange;
            var lastRow = usedRange.LastRow;

            var sales = new List<SeedSale>();

            //Iterate the cells in the used range and print the cell values
            for (var row = 1; row <= lastRow; row++)
            {
                if (row == 1)
                    continue;

                var sale = new SeedSale
                {
                    Region = salesWorkSheet[row, 1].DisplayText.Trim(),
                    Country = salesWorkSheet[row, 2].DisplayText.Trim(),
                    ItemType = salesWorkSheet[row, 3].DisplayText.Trim(),
                    SalesChannel = salesWorkSheet[row, 4].DisplayText.Trim(),
                    OrderPriority = salesWorkSheet[row, 5].DisplayText.Trim(),
                    OrderDate = DateTime.Parse(salesWorkSheet[row, 6].DisplayText.Trim().ToUpper()),
                    OrderId = salesWorkSheet[row, 7].DisplayText.Trim(),
                    ShipDate = DateTime.Parse(salesWorkSheet[row, 8].DisplayText.Trim()),
                    UnitsSold = Convert.ToInt32(salesWorkSheet[row, 9].DisplayText.Trim()),
                    UnitPrice = float.Parse(salesWorkSheet[row, 10].DisplayText.Trim()),
                    UnitCost = float.Parse(salesWorkSheet[row, 11].DisplayText.Trim()),
                    Serial = Convert.ToInt32(salesWorkSheet[row, 15].DisplayText.Trim())
                };

                sales.Add(sale);
            }

            return sales;
        }

        
        private static List<UserDTO> LoadSalesPeopleData(IWorksheet usersWorksheet, UserRole role)
        {
            //Access the used range of the Excel file
            var usedRange = usersWorksheet.UsedRange;
            var lastRow = usedRange.LastRow;

            var users = new List<UserDTO>();

            //Iterate the cells in the used range and print the cell values
            for (var row = 1; row <= lastRow; row++)
            {
                if (row == 1)
                    continue;

                var userDTO = new UserDTO()
                {
                    FirstName = usersWorksheet[row, 1].DisplayText.Trim(),
                    LastName = usersWorksheet[row, 2].DisplayText.Trim(),
                    Gender =  usersWorksheet[row, 3].DisplayText.Trim(),
                    Email = usersWorksheet[row, 4].DisplayText.Trim(),
                    DOB = DateTime.Parse(usersWorksheet[row, 5].DisplayText.Trim()),
                    Weight = double.Parse(usersWorksheet[row, 6].DisplayText.Trim().ToUpper()),
                    JoiningQuarter = usersWorksheet[row, 7].DisplayText.Trim(),
                    JoiningYear = Convert.ToInt32(usersWorksheet[row, 8].DisplayText.Trim()),
                    JoiningMonth = Convert.ToInt32(usersWorksheet[row, 9].DisplayText.Trim()),
                    JoiningMonthName = usersWorksheet[row, 10].DisplayText.Trim(),
                    JoiningDay = Convert.ToInt32(usersWorksheet[row, 11].DisplayText.Trim()),
                    Salary = float.Parse(usersWorksheet[row, 12].DisplayText.Trim()),
                    PhoneNumber = usersWorksheet[row, 13].DisplayText.Trim(),
                };

                switch (role)
                {
                    case UserRole.SalesPerson:
                        userDTO.Serial = Convert.ToInt32(usersWorksheet[row, 14].DisplayText.Trim());
                        break;
                    case UserRole.CountryManager:
                        userDTO.ManagerialRoleAssignment = usersWorksheet[row, 14].DisplayText.Trim();
                        break;
                    case UserRole.RegionalManager:
                        userDTO.ManagerialRoleAssignment = usersWorksheet[row, 14].DisplayText.Trim();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(role), role, null);
                }

                users.Add(userDTO);
            }

            return users;
        }


        //Accessing via sheet Name
        private static IWorksheet LoadSalesPeopleSheet() => workbook.Worksheets["SalesPeople"];
        private static IWorksheet LoadSalesSheet() => workbook.Worksheets["Sales"];
        private static IWorksheet LoadRegionalManagersSheet() => workbook.Worksheets["RegionManagers"];
        private static IWorksheet LoadCountryManagersSheet() => workbook.Worksheets["CountryManagers"];


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