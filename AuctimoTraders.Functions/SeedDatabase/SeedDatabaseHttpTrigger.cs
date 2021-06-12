using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AuctimoTraders.Seed.Helpers;
using AuctimoTraders.Shared.Helpers;
using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AuctimoTraders.Functions.SeedDatabase
{
    public static class SeedDatabaseHttpTrigger
    {
        [Function("SeedDatabaseHttpTrigger")]
        public static async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("SeedDatabaseHttpTrigger");
            logger.LogInformation("C# Seed Database HTTP trigger function processed a request.");


            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration["AzureWebJobsStorage"];
            var containerName = "seed";
            var endpoint = "https://adrianfiber.projects.legytt.com";
            //var endpoint = "https://localhost:5001";

            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();

            var blobName = "Auctimo Traders.xlsx";

            var blobClient = containerClient.GetBlobClient(blobName);

            if (!await blobClient.ExistsAsync())
                return ReturnResponse("Seed Data File Not Found", req, HttpStatusCode.OK);
            else
            {
                var inputStream = await blobClient.OpenReadAsync();
                logger.LogInformation($"File {blobClient.Name} found with length {inputStream.Length}\n");

                var salesPersons = ExcelDataLoader.LoadUsers(inputStream, UserRole.SalesPerson);
                inputStream.Position = 0;

                var regionalManagers = ExcelDataLoader.LoadUsers(inputStream, UserRole.RegionalManager);
                inputStream.Position = 0;

                var countryManagers = ExcelDataLoader.LoadUsers(inputStream, UserRole.CountryManager);
                inputStream.Position = 0;

                var sales = ExcelDataLoader.LoadSales(inputStream);
                inputStream.Position = 0;

                logger.LogInformation($"Loaded {salesPersons.Count} sales persons, " +
                                      $"{regionalManagers.Count} regional Managers, " +
                                      $"{countryManagers.Count} country managers" +
                                      $"and {sales.Count} sales");

                var baseUrl = "https://auctimotradersapi.azurewebsites.net/api";
                await SeedDatabase.Seed(regionalManagers, countryManagers, salesPersons, sales, logger, baseUrl);

                return ReturnResponse("File found", req, HttpStatusCode.OK);
            }
        }

        private static HttpResponseData ReturnResponse(string responseMessage, HttpRequestData req, HttpStatusCode statusCode)
        {
            var response = req.CreateResponse(statusCode);
            response.Headers.Add("Content-Type", "application/json");

            response.WriteString(JsonConvert.SerializeObject(new { ResponseMessage = responseMessage }));

            return response;
        }
    }
}
