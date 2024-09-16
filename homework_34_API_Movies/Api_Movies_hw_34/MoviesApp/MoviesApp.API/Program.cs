
using FluentValidation.AspNetCore;
using MoviesApp.DAL;
using MoviesApp.Business.MappingProfiles;
using MoviesApp.Business;
using MoviesApp.Business.DTOs.MovieDtos;
using MoviesApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using MoviesApp.DAL.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MoviesApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssembly(typeof(MovieCreateDtoValidator).Assembly);
            });

            builder.Services.AddAutoMapper(opt =>
            {
                opt.AddProfile<MapProfile>();
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>();


            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,

                    ValidIssuer = "http://localhost:5267/",
                    ValidAudience = "http://localhost:5267/",
                    ValidateLifetime = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("25e75b38-ff37-42c4-a8a2-bdbacd380945")),
                    ClockSkew=TimeSpan.Zero
                };
            });


            builder.Services.AddRepositories(builder.Configuration.GetConnectionString("Default"));
            builder.Services.AddServices();
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
            app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
