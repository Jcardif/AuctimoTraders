using System;
using System.IO;
using System.Reflection;

namespace AuctimoTraders.Seed
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                throw new InvalidOperationException(),
                @"Assets\Auctimo Traders.xlsx");

            var inputStream = File.Open(path, FileMode.Open);
            var x = ExcelDataLoader.LoadUsers(inputStream);

            foreach (var userDTO in x)
            {
                Console.WriteLine(userDTO.FirstName);
            }

        }
    }
}
