using ImprentaAPI.BusinessLogicLayer.Services;
using ImprentaAPI.BusinessLogicLayer.Services.Interfaces;
using ImprentaAPI.Data;
using ImprentaAPI.Data.Repositories;
using ImprentaAPI.DataAccessLayer.Data.Repositories;
using ImprentaAPI.DataAccessLayer.Data.Repositories.Interfaces;
using ImprentaAPI.Services;
using ImprentaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ImprentaAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var postgreSQLConecctionConfiguration = new PostgreSQLConfiguration(Configuration.GetConnectionString("PostgreSQLConnection"));
            services.AddSingleton(postgreSQLConecctionConfiguration);
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<ISelloService, SelloService>();
            services.AddScoped<IAficheService, AficheService>();
            services.AddScoped<IEmpastadoService, EmpastadoService>();
            services.AddScoped<IReservaService, ReservaService>();
            services.AddScoped<IPrecioService, PrecioService>();
            services.AddScoped<IPrecioAcabadoService, PrecioAcabadoService>();

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<ISelloRepository, SelloRepository>();
            services.AddScoped<IAficheRepository, AficheRepository>();
            services.AddScoped<IEmpastadoRepository, EmpastadoRepository>();
            services.AddScoped<IReservaRepository, ReservaRepository>();
            services.AddScoped<IPrecioRepository, PrecioRepository>();
            services.AddScoped<IPrecioAcabadoRepository, PrecioAcabadoRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            //Agregando configuracion para uso de Token
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token"])),
                    ValidateIssuer=false,
                    ValidateAudience=false
                };
            });

            //automapper configuration
            services.AddAutoMapper(typeof(Startup));

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => { options.AllowAnyOrigin(); options.AllowAnyMethod(); options.AllowAnyHeader(); });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kairos API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(options => { options.AllowAnyOrigin(); options.AllowAnyMethod(); options.AllowAnyHeader(); });

            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //Enablemiddleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            //Enabl middleware to serve swagger-ui (HTML,JS,CSS, etc.)
            //Especificying the Swagger JSON endpoint
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kairos API V1");
            });


        }
    }
}
