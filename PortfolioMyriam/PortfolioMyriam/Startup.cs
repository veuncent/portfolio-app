using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortfolioMyriam.Data;
using PortfolioMyriam.Models;
using PortfolioMyriam.Services;
using PortfolioMyriam.Models.HelperClasses;

namespace PortfolioMyriam
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
            var defaultConnectionString = Environment.ExpandEnvironmentVariables(Configuration.GetConnectionString("DefaultConnection"));

            services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(defaultConnectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            services.AddScoped<IConfigurationService, ConfigurationService>();

            services.AddScoped<IStringHelperService, StringHelperService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateRoles(serviceProvider);
            CreateAdmin(serviceProvider);
            CreateGuest(serviceProvider);
        }


        private static void CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "Guest" };

            foreach (var roleName in roleNames)
            {
                CreateRole(roleManager, roleName);
            }
        }


        private static void CreateRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var roleExists = roleManager.RoleExistsAsync(roleName);
            roleExists.Wait();

            if (!roleExists.Result)
            {
                Task<IdentityResult> roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
                roleResult.Wait();
            }
        }


        private void CreateAdmin(IServiceProvider serviceProvider)
        {
            var adminPassword = Environment.ExpandEnvironmentVariables(Configuration["AppSettings:AdminPassword"]);
            var adminEmail = Environment.ExpandEnvironmentVariables(Configuration["AppSettings:AdminEmail"]);
            var adminuserName = "Admin";
            var adminRoleName = Roles.Admin;

            CreateUser(serviceProvider, adminRoleName, adminuserName, adminPassword, adminEmail);
        }

        private void CreateGuest(IServiceProvider serviceProvider)
        {
            var guestPassword = Environment.ExpandEnvironmentVariables(Configuration["AppSettings:GuestPassword"]);
            var guestuserName = "Guest";
            var guestRoleName = Roles.Guest;

            CreateUser(serviceProvider, guestRoleName, guestuserName, guestPassword);
        }
        

        private void CreateUser(IServiceProvider serviceProvider, string roleName, string userName, string userPassword, string userEmail = null)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var existingUser = userManager.FindByNameAsync(userName);
            existingUser.Wait();

            if (existingUser.Result == null)
            {
                var newUser = new ApplicationUser
                {
                    UserName = userName,
                    Email = userEmail
                };

                var createUserTask = userManager.CreateAsync(newUser, userPassword);
                createUserTask.Wait();

                if (createUserTask.Result.Succeeded)
                {
                    var addRoleToUserTask = userManager.AddToRoleAsync(newUser, roleName);
                    addRoleToUserTask.Wait();
                }
                else
                {
                    var exceptionMessages = createUserTask.Result.Errors.Aggregate($"Failed to create user {newUser.UserName}. Errors were: \n\r\n\r", (current, error) => current + ($" - {error} - {error.Code} - {error.Description} \n\r"));
                    throw new Exception(exceptionMessages);
                }
            }

        }
    }
}
