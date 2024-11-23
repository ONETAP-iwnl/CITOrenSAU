using WebCIT.Api.TicketService;
using WebCIT.Controllers;

namespace WebCIT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run("https://0.0.0.0:7111");
            app.Run();

            app.MapControllerRoute(
                name: "admin",
                pattern: "Admin/{action=AdminDashboard}/{id?}",
                defaults: new { controller = "Admin", action = "AdminDashboard" });
        }
    }
}
