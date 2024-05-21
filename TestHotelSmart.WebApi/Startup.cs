using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Globalization;
using TestHotelSmart.WebApi.Application;
using TestHotelSmart.WebApi.Application.Contracts;
using TestHotelSmart.WebApi.DataAccess.Repositories;
using TestHotelSmart.WebApi.DataAccess.Repositories.Contractos;
using TestHotelSmart.WebApi.Helpers;
using TestHotelSmart.WebApi.Model;

namespace TestHotelSmart.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("es-ES");
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("es-ES");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<TestHotelContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("PruebaSmart")));


            #region DI DataAccess
            services.AddTransient<IHotelRepository, HotelRepository>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<IGuestRepository, GuestRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            #endregion

            #region DI Application
            services.AddTransient<IAdminApplication, AdminApplication>();
            services.AddTransient<IClientApplication, ClientApplication>();

            #endregion

            // Configuracion Automapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutomapperConfig());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestHotelSmart.WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestHotelSmart.WebApi v1"));
            }

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            //app.UseHttpsRedirection();
            app.UseWebSockets();
            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

