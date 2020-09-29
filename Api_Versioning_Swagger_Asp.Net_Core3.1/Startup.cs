using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Api_Versioning_Swagger_Asp.Net_Core3._1
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
            services.AddControllers(options =>
            {
                options.Conventions.Add(new GroupingByNamespaceConvention());
            });
            services.AddApiVersioning(options=> {

                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1,0);
            });

            services.AddSwaggerGen(options =>
            {
                var titleBase = "Movies API";
                var description = "This is a Web API for Movies operations";
                var TermsOfService = new Uri("http://localhost");
                var License = new OpenApiLicense()
                {
                    Name = "MIT"
                };
                var Contact = new OpenApiContact()
                {
                    Name = "Test Name",
                    Email = "test@gmail.com",
                    Url = new Uri("http://localhost")
                };

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = titleBase + " v1",
                    Description = description,
                    TermsOfService = TermsOfService,
                    License = License,
                    Contact = Contact
                });

                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = titleBase + " v2",
                    Description = description,
                    TermsOfService = TermsOfService,
                    License = License,
                    Contact = Contact
                });
            });
        }
        public class GroupingByNamespaceConvention : IControllerModelConvention
        {
            public void Apply(ControllerModel controller)
            {
                var controllerNamespace = controller.ControllerType.Namespace;
                var apiVersion = controllerNamespace.Split(".").Last().ToLower();
                if (!apiVersion.StartsWith("v")) { apiVersion = "v1"; }
                controller.ApiExplorer.GroupName = apiVersion;
            }
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "MoviesAPI v1");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "MoviesAPI v2");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
