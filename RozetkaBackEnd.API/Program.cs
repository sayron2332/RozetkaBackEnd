using RozetkaBackEnd.Core;
using RozetkaBackEnd.Infrastructure;

namespace RozetkaBackEnd.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connStr = builder.Configuration.GetConnectionString("DefaultConnection");

            //Add Core Services
            builder.Services.AddCoreServices();

            //Add Infrastructure Services
            builder.Services.AddInfrastructureServices();

            builder.Services.AddDbContext(connStr);

            var app = builder.Build();

            //Get Connection strings
          

            

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
