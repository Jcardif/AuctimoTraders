using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctimoTraders.Helpers;
using AuctimoTraders.Seed.Helpers;
using AuctimoTraders.Shared.Helpers;
using AuctimoTraders.Shared.Models;
using Microsoft.Extensions.Logging;

namespace AuctimoTraders.Functions.SeedDatabase
{
    public static class SeedDatabase
    {
        public static async Task Seed(List<UserDTO> regionalManagers, List<UserDTO> countryManagers,
            List<UserDTO> salesPersons, List<SeedSale> sales, ILogger logger, string baseUrl)
        {
            var httpDataService = new HttpDataService(baseUrl);
            foreach (var regionalManager in regionalManagers)
            {
                var res = await httpDataService.PostAsJsonAsync($"users/{UserRole.RegionalManager}",
                    regionalManager);

                if (res is UserDTO user)
                {
                    logger.LogInformation(
                        $"{regionalManagers.IndexOf(regionalManager)}. Created {user.FirstName} {user.LastName} {user.Id}");
                }
                else
                {
                    logger.LogError(await Json.StringifyAsync(res));
                }
            }

            foreach (var countryManager in countryManagers)
            {
                var res = await httpDataService.PostAsJsonAsync($"users/{UserRole.CountryManager}",
                    countryManager);
                if (res is UserDTO user)
                    logger.LogInformation(
                        $"{countryManagers.IndexOf(countryManager)}. Created {user.FirstName} {user.LastName} {user.Id}");
                else
                {
                    logger.LogError(await Json.StringifyAsync(res));
                }
            }

            foreach (var salesPerson in salesPersons)
            {
                var res = await httpDataService.PostAsJsonAsync($"users/{UserRole.SalesPerson}",
                    salesPerson);

                if (res is UserDTO user)
                    logger.LogInformation(
                        $"{salesPersons.IndexOf(salesPerson)}. Created {user.FirstName} {user.LastName} {user.Id}");
                else
                {
                    logger.LogError(await Json.StringifyAsync(res));
                }
            }

            foreach (var sale in sales)
            {
                var res = await httpDataService.PostAsJsonAsync($"Sales", sale);
                try
                {
                    var saleDTO = (SaleDTO) res;
                    logger.LogInformation($"{sales.IndexOf(sale)}. {saleDTO.Id}\t{saleDTO.UnitCost}\t{saleDTO.UnitsSold}");

                }
                catch (Exception e)
                {
                    try
                    {
                        var response = (ApiResponseMessage) res;
                        logger.LogInformation($"{sales.IndexOf(sale)}. {response.Message}");
                    }
                    catch (Exception exception)
                    {
                        logger.LogError($"Error in sale {sales.IndexOf(sale)}. {exception.Message}");
                    }
                }

            }
        }
    }
}
