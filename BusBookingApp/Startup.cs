using System;
using System.Text;
using BusBookingApp.Data;
using BusBookingApp.PayStackApi.Repositories;
using BusBookingApp.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace BusBookingApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IGeneralRepository, GeneralRepository>();
            services.AddScoped<IBusRepository, BusRepository>();
            services.AddScoped<IBusTicketRepository, BusTicketRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            //For Entity FrameWork
            var connection = Configuration.GetConnectionString("BusBooking");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

            //For Identity
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                        .AddEntityFrameworkStores<ApplicationDbContext>()
                        .AddDefaultTokenProviders();

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

            //services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(builder =>
            //    {
            //        builder.AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader();
            //    });
            //});
            services.AddHttpContextAccessor();
            //services.AddHttpClient("paystack", c =>
            //{
            //    c.BaseAddress = new Uri("https://api.paystack.co/transaction");
            //    c.DefaultRequestHeaders.Add("Accept", "application/json");
            //    c.DefaultRequestHeaders.Add("Authorization", $"Bearer {Configuration.GetSection("PayStackService").GetSection("SecretKey").Value}");
            //});

            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
