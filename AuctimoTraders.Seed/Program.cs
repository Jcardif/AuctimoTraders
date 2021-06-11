using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using AuctimoTraders.Shared.Helpers;

namespace AuctimoTraders.Seed
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var path = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                throw new InvalidOperationException(),
                @"Assets\Auctimo Traders.xlsx");

            var inputStream = File.Open(path, FileMode.Open);
            var salesPersons = ExcelDataLoader.LoadUsers(inputStream, UserRole.SalesPerson);
            await inputStream.DisposeAsync();

            inputStream = File.Open(path, FileMode.Open);
            var regionalManagers = ExcelDataLoader.LoadUsers(inputStream, UserRole.RegionalManager);
            await inputStream.DisposeAsync();

            inputStream = File.Open(path, FileMode.Open);
            var countryManagers = ExcelDataLoader.LoadUsers(inputStream, UserRole.CountryManager);
            await inputStream.DisposeAsync();

            inputStream = File.Open(path, FileMode.Open);
            var sales = ExcelDataLoader.LoadSales(inputStream);
            await inputStream.DisposeAsync();

            Console.WriteLine($"Sales Persons : {salesPersons.Count}");
            Console.WriteLine($"Country Managers : {countryManagers.Count}");
            Console.WriteLine($"Regional Managers : {regionalManagers.Count}");
            Console.WriteLine($"Sales : {sales.Count}");

            Console.ReadKey();
        }
    }
}
