using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using BooksSQL.DataContexts;
using BooksManagement.Repositories;
using BooksManagement.Repositories.Interface;
using BooksManagement.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using BooksMongoDB.DataService;
using BooksMongoDB.DataSettings;

namespace BooksManagement.API
{
    public class Startup
    {
        public IConfiguration _config { get; }

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<UsersDbContext>(
                options => options.UseSqlServer(_config.GetConnectionString("BooksUsersDBConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<UsersDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            });

            services.ConfigureApplicationCookie(o =>
            {
                o.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = (ctx) =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                        }

                        return Task.CompletedTask;
                    },
                    OnRedirectToAccessDenied = (ctx) =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 403;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o => {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _config["Jwt:Issuer"],
                        ValidAudience = _config["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
                    };
                });

            services.AddDbContext<BooksSqlDBContext>(
                options => options.UseSqlServer(_config.GetConnectionString("BooksDBConnection")));

            services.Configure<BooksMongoDBSettings>(
                _config.GetSection(nameof(BooksMongoDBSettings)));

            services.AddSingleton<IBooksMongoDBSettings>(sp =>
                sp.GetRequiredService<IOptions<BooksMongoDBSettings>>().Value);

            services.AddSingleton<BooksMongoDBService>();

            services.AddScoped<IBooksRepository, BooksMockRepository>(); //BooksSqlRepository,BooksMongoDBRepository,BooksMockRepository
            //services.AddScoped(Factories.GetBooksRepository);

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            options.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Books}/{action=Index}/{id?}");
            });
        }

        public static class Factories
        {
            public static IBooksRepository GetBooksRepository(IServiceProvider serviceProvider)
            {
                var env = serviceProvider.GetRequiredService<IWebHostEnvironment>();
                if (env.IsDevelopment())
                {
                    var booksDB = serviceProvider.GetService(typeof(BooksSqlDBContext)) as BooksSqlDBContext;
                    return new BooksSqlRepository(booksDB);
                }
                else
                {
                    var booksMongoDBService = serviceProvider.GetRequiredService(typeof(BooksMongoDBService)) as BooksMongoDBService;
                    return new BooksMongoDBRepository(booksMongoDBService);
                }
            }
        }
    }
}
