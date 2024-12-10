using Microsoft.EntityFrameworkCore;
using TicketManager.Context;
using TicketManager.Interface;
using TicketManager.Interface.Department;
using TicketManager.Interface.RequestType;
using TicketManager.Model;
using TicketManager.Model.Department;
using TicketManager.Model.RequestType;
using TicketManager.Service;
using TicketManager.Service.Department;
using TicketManager.Service.RequestType;

namespace TicketManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<TicketContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

           builder.Services.AddDbContext<DepartmentContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

           builder.Services.AddDbContext<RequestTypeContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ITicketRepository, TicketRepository>();
            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddSingleton<IServiceFactory, ServiceFactory>();

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            builder.Services.AddScoped<IRequestTypeRepository, RequestTypeRepository>();
            builder.Services.AddScoped<IRequestTypeService, RequestTypeService>();

            builder.Services.AddControllers();
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

            app.Run("https://0.0.0.0:7215");

            app.Run();
        }
    }
}
