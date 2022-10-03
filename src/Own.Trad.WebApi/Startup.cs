using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Own.Trad.DataAccess.Data;
using Own.Trad.DataAccess.Repositories;
using Own.Trad.Framework.Authentication;
using Own.Trad.Framework.Services;
using Own.Trad.Services.Interfaces;
using Own.Trad.WebApi.Errors;
using Own.Trad.WebApi.Mapping;
using Serilog;

namespace Own.Trad.WebApi
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

            // Swagger
            services.AddSwaggerGen(c =>
            {
                // swagger doc
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Own API",
                    Version = "v1",
                    Description = "This is My Own API!",
                    Contact = new OpenApiContact
                    {
                        Name = "guuggle",
                        Email = "kandejianer@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Apache 2.0",
                        Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
                    }
                });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Own API", Version = "v2" });

                // xml comments
                var filePath = Path.Combine(AppContext.BaseDirectory, "Own.Trad.WebApi.xml");
                c.IncludeXmlComments(filePath);

                // security
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                        },
                        new string[] {}
                    }
                });
            });

            // Custom problem error
            services.AddSingleton<ProblemDetailsFactory, OwnProblemDetailsFactory>();

            // Mapping
            services.AddMapping();

            // Dbcontext
            services.AddDbContext<OwnDbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"));
            });
            // Authentication
            services.AddAuth(Configuration);

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // under dev env, return detailed messages including stack trace
                // and check request header [Accept] to display return value in different formats.
                // if its not enabled, then just 500 error.
                app.UseDeveloperExceptionPage();
                app.UseSwagger(c =>
                {
                    // documentName要和SwaggerDoc.name一致
                    c.RouteTemplate = "api-docs/{documentName}/swagger.json";
                });
                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = "api-docs";
                    c.DocumentTitle = "Own api-docs";
                    c.SwaggerEndpoint("/api-docs/v1/swagger.json", "Own API V1");
                    c.SwaggerEndpoint("/api-docs/v2/swagger.json", "Own API V2");
                    c.IndexStream = () => GetType().Assembly.GetManifestResourceStream("Own.Trad.WebApi.Pages.swaggerindex.html");
                    c.DisplayRequestDuration();
                });
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
