using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CityInfo.API.Contexts;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace CityInfo.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddMvcOptions(o =>
            {
                o.InputFormatters.Add(new XmlDataContractSerializerInputFormatter()); // Allows Xml as a input with the content-type header
                o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()); // Allows Xml as an output depending on the accept header
            });

            services.AddScoped<ICityInfoRepository, CityInfoRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif
            string connectionString = _configuration["connectionString:cityInfoDBConnectionString"];
            services.AddDbContext<CityInfoContext>(o =>
            {
                o.UseMySQL(connectionString);
            });
            //    .AddJsonOptions(o =>
            //{
            //    // This is in case with dotn want to use camelCase, this will return the properties of an object with 
            //    // their property names as they are, upperCase or not.
            //    if (o.SerializerSettings.ContractResolver != null)
            //    {
            //        var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;

            //        castedResolver.NamingStrategy = null;
            //    }
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseStatusCodePages();

            app.UseMvc();

        }
    }
}
