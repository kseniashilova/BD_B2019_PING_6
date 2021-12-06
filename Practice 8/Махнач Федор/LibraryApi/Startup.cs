using LibraryApi.DataAccess.ConnectionFactories;
using LibraryApi.DataAccess.Options;
using LibraryApi.DataAccess.Repositories;
using LibraryApi.Domain.Repositories;
using LibraryApi.Domain.Services;
using LibraryApi.Infrastructure;
using LibraryApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace LibraryApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Database connection
            services
                .ConfigureNpgsqlDatabase<LibraryDbConnectionOptions>()
                .AddSingleton<ILibraryDbConnectionFactory, LibraryDbConnectionFactory>();
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            // Services
            services
                .AddScoped<IBooksService, BooksService>()
                .AddScoped<IBookCopiesService, BookCopiesService>()
                .AddScoped<IReadersService, ReadersService>()
                .AddScoped<IBorrowingsService, BorrowingsService>()
                .AddScoped<IGeneratorService, GeneratorService>()
                .AddScoped<IFakerService, FakerService>();

            // Repositories
            services
                .AddScoped<IBooksRepository, BooksRepository>()
                .AddScoped<IBookCopiesRepository, BookCopiesRepository>()
                .AddScoped<IBorrowingsRepository, BorrowingsRepository>()
                .AddScoped<IReadersRepository, ReadersRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "LibraryApi", Version = "v1" }); });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LibraryApi v1"));

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}