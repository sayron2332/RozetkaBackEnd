using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RozetkaBackEnd.Core;
using RozetkaBackEnd.Infrastructure;
using System.Text;

namespace RozetkaBackEnd.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Secret"]);
            var tokenValidationParemeters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
                ValidAudience = builder.Configuration["JwtConfig:Audience"]
            };

            builder.Services.AddSingleton(tokenValidationParemeters);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = tokenValidationParemeters;
                jwt.RequireHttpsMetadata = false;
            });

            string connStr = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext(connStr);

            //Add Infrastructure Services
            builder.Services.AddInfrastructureServices();

            builder.Services.AddRepositories();

        
            //Add Core Services
            builder.Services.AddCoreServices();

           

  

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Rozetka API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"

                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {    
                   {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                   }
                }); 
            }); 

            var app = builder.Build();

            //Get Connection strings



            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(options => options
                .SetIsOriginAllowed(origin => true)
                .AllowAnyHeader()
                .AllowCredentials()
                .AllowAnyMethod()
            );


            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();


            app.Run();
        }
    }
}
