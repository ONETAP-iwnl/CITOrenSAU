using Microsoft.EntityFrameworkCore;
using UserService.Context;
using UserService.Interface;
using UserService.Repository;
using UserService.Service;

namespace UserService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<UserContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

           builder.Services.AddDbContext<AuthorsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<ExecutorsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
           
            builder.Services.AddScoped<IUserFactory, UserFactory>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, Service.UserService>();

            builder.Services.AddScoped<IAuthorsRepository, AuthorsRepository>();
            builder.Services.AddScoped<IAuthorsService, AuthorsService>();

            builder.Services.AddScoped<IExecutorsRepository, ExecutorsRepository>();
            builder.Services.AddScoped<IExecutorsService, ExecutorsService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            //app.Run("https://26.240.38.124:5235");
            app.Run("https://0.0.0.0:5235");
            app.Run();
        }
    }
}
