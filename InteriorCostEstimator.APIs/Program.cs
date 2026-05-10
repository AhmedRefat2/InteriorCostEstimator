using InteriorCostEstimator.Application.Features.AuthFeature.Services;
using InteriorCostEstimator.Application.Features.CategoryFeature.Services;
using InteriorCostEstimator.Application.Features.ExcelFeature.Services;
using InteriorCostEstimator.Application.Features.ProductFeature.Services;
using InteriorCostEstimator.Application.Features.ProjectFeature.Services;
using InteriorCostEstimator.Application.Features.ProposalFeature.Services;
using InteriorCostEstimator.Application.Features.VendorFeature.Services;
using InteriorCostEstimator.Domain.Entities;
using InteriorCostEstimator.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InteriorCostEstimator.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddOpenApi();


            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
                .AddJwtBearer("JwtBearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                    };
                });

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IVendorService, VendorService>();
            builder.Services.AddScoped<ExcelSeederService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped< IProposalService, ProposalService>();
            builder.Services.AddScoped<IFileService, FileService>();

            builder.Services.AddHttpClient<IAiService, AiService>(
    client =>
    {
        client.BaseAddress =
            new Uri(
        builder.Configuration["AISettings:BaseUrl"]!);
    });
            builder.Services.AddScoped<IProjectService, ProjectService>();


            builder.Services.AddCors(options =>
            {
                //options.AddPolicy("AllowFrontend",
                //    policy =>
                //    {
                //        policy.WithOrigins(
                //            "http://localhost:5174"
                //        )
                //        .AllowAnyHeader()
                //        .AllowAnyMethod();
                //    });

                options.AddPolicy("AllowAll",
                     policy =>
                     {
                         policy.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                     });
            });

            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string[] roles = { "Designer", "Vendor" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }

                var services = scope.ServiceProvider;

                var context =
                    services.GetRequiredService<AppDbContext>();

                // لو فيه products already
                if (!context.Products.Any())
                {
                    var seeder =
                        services.GetRequiredService<ExcelSeederService>();

                    await seeder.SeedCategoriesAndProducts(
                        "Data/IKEA_Egypt_Furniture.xlsx");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            //app.UseCors("AllowFrontend");
            app.UseCors("AllowAll");

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet("/", () => Results.Redirect("/swagger"));
            app.MapControllers();

            app.Run();
        }
    }
}
