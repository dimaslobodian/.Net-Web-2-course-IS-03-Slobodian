using BLL.Interfaces;
using BLL.Services;
using DAL.EF;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using DAL.UoW;


namespace Hotel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Choose, if you want to run LAB_3 or LAB_5 usinng "console" for lab_3 or "web" for lab_5


            var typeOfLab = "console";
            //var typeOfLab = "web";

            switch (typeOfLab)
            {
                case "console":
                    CreateHostBuilder(args).Build().Run();
                    break;
                case "web":
                    CreateHostBuilderWeb(args).Build().Run();
                    break;
                default:
                    break;
            }

        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<IRoomService, RoomService>();
                    services.AddScoped<IBookingService, BookingService>();

                    services.AddDbContext<HotelDbContext>();
                    services.AddScoped<IRepository<Room>, Repository<Room>>();
                    services.AddScoped<IRepository<Booking>, Repository<Booking>>();
                    services.AddScoped<IUnitOfWork, UnitOfWork>();


                    services.AddHostedService<Worker>();
                });
        public static IHostBuilder CreateHostBuilderWeb(string[] args) =>
                    Host.CreateDefaultBuilder(args)
                        .ConfigureWebHostDefaults(webBuilder =>
                        {
                            webBuilder.UseStartup<Startup>();
                        });
    }

}