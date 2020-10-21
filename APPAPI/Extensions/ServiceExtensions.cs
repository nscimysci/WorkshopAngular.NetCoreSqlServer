using System;
using System.Text;
using APPAPI.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


namespace APPAPI.Extensions {
    public static class ServiceExtensions {

        public static void ConfigureCors (this IServiceCollection services) {

            services.AddCors (options => {
                options.AddPolicy ("AllowMyOrigin",
                    builder => {
                        builder.WithOrigins ("http://localhost:4200",
                                "https://localhost:5001"
                               )
                            .AllowAnyHeader ()
                            .AllowAnyMethod ()
                            .AllowCredentials ();
                    });
            });
        }

        public static void ConfigureSqlContext (this IServiceCollection services, IConfiguration config) {
            services.AddDbContext<DatabaseContext> (options =>
                options.UseSqlServer (config.GetConnectionString ("DefaultConnection_sql_server")));
        }

        public static void ConfigureJWTuthentication (this IServiceCollection services, IConfiguration config) {
            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme).AddJwtBearer (options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuer = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = config["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero, // disable delay when token is expire
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (config["Jwt:Key"]))
                };
            });
        }


    }
}