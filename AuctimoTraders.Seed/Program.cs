using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using AuctimoTraders.Seed.Helpers;
using AuctimoTraders.Shared.Helpers;
using AuctimoTraders.Shared.Models;

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

            var httpDataService = new HttpDataService("https://auctimotraders.azurewebsites.net/api");
            foreach (var regionalManager in regionalManagers)
            {
                var res = await httpDataService.PostAsJsonAsync($"users/{UserRole.RegionalManager}",
                    regionalManager);

                if (res is UserDTO user)
                {
                    Console.WriteLine(
                        $"{regionalManagers.IndexOf(regionalManager)}. Created {user.FirstName} {user.LastName} {user.Id}");
                }
                else
                {
                    Console.WriteLine(Json.StringifyAsync(res));
                }
            }

            foreach (var countryManager in countryManagers)
            {
                var res =  await httpDataService.PostAsJsonAsync($"users/{UserRole.CountryManager}",
                    countryManager);
                if (res is UserDTO user)
                    Console.WriteLine(
                        $"{countryManagers.IndexOf(countryManager)}. Created {user.FirstName} {user.LastName} {user.Id}");
                else
                {
                    Console.WriteLine(Json.StringifyAsync(res));
                }
            }

            foreach (var salesPerson in salesPersons)
            {
                var res = await httpDataService.PostAsJsonAsync($"users/{UserRole.SalesPerson}",
                    salesPerson);

                if (res is UserDTO user)
                    Console.WriteLine(
                        $"{salesPersons.IndexOf(salesPerson)}. Created {user.FirstName} {user.LastName} {user.Id}");
                else
                {
                    Console.WriteLine(Json.StringifyAsync(res));
                }
            }

            foreach (var sale in sales)
            {
                var res=await httpDataService.PostAsJsonAsync($"Sales/seed", sale);
                if (res is SaleDTO saleDTO)
                    Console.WriteLine($"{sales.IndexOf(sale)}. {saleDTO.Id}\t{saleDTO.UnitCost}\t{saleDTO.UnitsSold}");
            }

            
            Console.ReadKey();
        }
    }
}
