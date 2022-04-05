
using ApiWithAzure.Authentication;
using ApiWithAzure.Data.Model;
using ApiWithAzure.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithAzure
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //Add the database with the deault connection string
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("Default"));
            });
            // Adding the idintity (login/register)
            services.AddIdentity<ApplicationUser, IdentityRole>()
                // Store it in this database
                .AddEntityFrameworkStores<ApplicationDbContext>()
            // We are going to use token for authorization
             .AddDefaultTokenProviders();

            // Adding token authenrication
            services.AddTokenAuthentication(_configuration);
            // Identity options
            services.Configure<IdentityOptions>(options =>
            {
                // Very weak passward
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

                // No dublicate emails
                options.User.RequireUniqueEmail = true;
            });

            // Adding swagger to our server
            services.AddSwaggerGen(options =>
            {
                // Adding swagger document
                options.SwaggerDoc("v1.0", new OpenApiInfo() { Title = "Main API v1.0", Version = "v1.0" });

                // To use unique names with the requests and responses
                options.CustomSchemaIds(x => x.FullName);

                // Include the comments that we wrote in the documentation
                options.IncludeXmlComments("ApiWithAzure.xml");

                // Defining the security schema
                var securitySchema = new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                // Adding the bearer token authentaction option to the ui
                options.AddSecurityDefinition("Bearer", securitySchema);

                // use the token provided with the endpoints call
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                });

            });
            services.AddControllers();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            // Use authentication
            app.UseAuthentication();

            app.UseAuthorization();


            // If is in development
            if (true || env.IsDevelopment() || env.IsProduction())
            {
                // Use swagger
                app.UseSwagger();

                // Add swagger UI
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Versioned API v1.0");

                    c.DocExpansion(DocExpansion.None);
                });
            }
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
