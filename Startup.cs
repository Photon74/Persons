using AutoMapper;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Persons.DAL;
using Persons.DAL.Repositories;
using Persons.DAL.Repositories.Intrefaces;
using Persons.Mapper;
using Persons.Services.Interfaces;

namespace Persons
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
            var connection = Configuration["ConnectionStrings:PostgresSQL"];
            services.AddDbContext<PersonDbContext>(options => options.UseNpgsql(connection));

            // регистрация маппера
            var mapperConfiguration = new MapperConfiguration(mp
                => mp.AddProfile(new MapperProfiler()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            // регистрация контекста
            //services.AddSingleton<PersonDbContext>();

            // регистрация интерфейсов
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IPersonService, PersonService>();

            // регистрация контроллеров
            services.AddControllers();

            // регистрация сваггера
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Persons", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Persons v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
