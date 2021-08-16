using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web_Api.Model.Context;
using Web_Api.Business;
using Web_Api.Business.Implementation;
using Web_Api.Repository;
using Web_Api.Repository.Implementation;
using Serilog;
using System;
using System.Collections.Generic;

namespace Web_Api
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            //Injecao de dependencia -> Conexao com banco MYSQL
            var connection = Configuration["MysqlConnection:MysqlConnectionString"];
            //var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));
            //services.AddDbContext<MySqlContext>(options => options.UseMySql(connection, serverVersion));
            services.AddDbContext<MySqlContext>(options => options.UseMySql(connection));


            if (Environment.IsDevelopment())
            {
                MigrateDatabase(connection);
            }



            //API Versioning
            services.AddApiVersioning();


            //Injeção de dependencia
            services.AddScoped<IPersonBusiness, IPersonBusinessImplementation>();
            services.AddScoped<IPersonRepository, IPersonRepositoryImplementation>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void MigrateDatabase(string connection)
        {
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true,

                };

                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Migration Failed", ex);
                throw;
            }
        }

    }
}
