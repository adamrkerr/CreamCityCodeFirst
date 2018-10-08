using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CreamCityCodeFirst.Context;
using CreamCityCodeFirst.Mapping;
using CreamCityCodeFirst.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreamCityCodeFirst.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterUniversityDb(this IServiceCollection serviceCollection, UniversityDbArguments arguments)
        {
            // Adds services required for using options.
            serviceCollection.AddOptions();

            serviceCollection.AddDbContext<UniversityDbContext>(options => options
            .UseSqlServer(arguments.ConnectionString));

            serviceCollection.AddDbContext<UniversityDbContextReadonly>(options => options
            .UseSqlServer(arguments.ConnectionString));

            //register arguments so we can get them in UseUniversityDb()
            serviceCollection.AddSingleton(arguments);

            serviceCollection.AddTransient<Profile, MappingProfile>();

            serviceCollection.AddScoped<ICourseRepository, CourseRepository>();
            serviceCollection.AddScoped<ICourseChangeHandler, CourseChangeHandler>();

            return serviceCollection;
        }

        public static IApplicationBuilder UseUniversityDb(this IApplicationBuilder app)
        {
            var creationArgs = app.ApplicationServices.GetService<UniversityDbArguments>();

            if (creationArgs != null && creationArgs.CreateDbIfNotFound)
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<UniversityDbContext>();
                    context.Database.Migrate();
                    context.EnsureSeedData().Wait();
                }
            }

            return app;
        }
        
    }
}
