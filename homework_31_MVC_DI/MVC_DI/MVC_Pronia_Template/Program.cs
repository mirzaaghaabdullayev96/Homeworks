using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MVC_Pronia_Template.DAL;
using System.Data;

namespace MVC_Pronia_Template
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddTransient<IDbConnection>(sp =>
                new SqlConnection(builder.Configuration.GetConnectionString("Default")));

            var app = builder.Build();

            app.UseStaticFiles();


            app.MapControllerRoute(

              name: "admin",
              pattern: "{area:exists}/{controller=home}/{action=index}/{id?}"
              );

            app.MapControllerRoute(

                name: "default",
                pattern: "{controller=home}/{action=index}/{id?}"
                );

            app.Run();
        }
    }
}
