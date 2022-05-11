using BLL.Interfaces;
using BLL.Services;
using DAL.EF;
using DAL.Interfaces;
using DAL.Repositories;
using DAL.UoW;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hotel
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                    builder.SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });



            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();


            services.AddDbContext<HotelDbContext>();

            services.AddCors();

            services.AddControllers();
            services.AddSwaggerGen();



            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IBookingService, BookingService>();





        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI();



            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();



            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"Hello World! {DateTime.Now}");
                });
            });
        }
    }
}