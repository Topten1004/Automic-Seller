using Sales.AtomicSeller.Config;
using Sales.AtomicSeller.Data;
using Sales.AtomicSeller.Entities;
using Sales.AtomicSeller.Providers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.AtomicSeller
{
    public class Seeder
    {
        /// <summary>
        /// Generate migrations before running this method.
        /// </summary>
        /// <param name="host"></param>      
        public static async Task EnsureSeedData(IHost host)
        {
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                //await EnsureDatabasesMigrated(services);
                await EnsureSeedIdentityData(services);
            }
        }

        public static async Task MigrateDatabase(IHost host)
        {
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                await EnsureDatabasesMigrated(services);
            }
        }
        private static async Task EnsureDatabasesMigrated(IServiceProvider services)
        {
            /*
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    await context.Database.MigrateAsync();
                }
            }
            */
        }
        #region Identity Seed Data
        private static async Task EnsureSeedIdentityData(IServiceProvider serviceProvider)
        {
            
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                //context.IdentityProvider = IdentityProvider.SystemIdentity;
                var roleManager         = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager         = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var rootConfiguration   = scope.ServiceProvider.GetRequiredService<IRootConfig>();

            //    await EnsureSeedIdentityData(userManager, roleManager, rootConfiguration.IdentityDataConfig);
            //    await EnsureSeedStoreData(context, rootConfiguration.StoreDataConfig);
            }
            
        }

        /// <summary>
        /// Generate default admin user / role
        /// </summary>
        private static async Task EnsureSeedIdentityData(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IdentityDataConfig identityDataConfiguration)
        {

            // adding roles from seed
            foreach (var r in identityDataConfiguration.Roles)
            {
                if (!await roleManager.RoleExistsAsync(r.Name))
                {
                    var role = new IdentityRole
                    {
                        Name = r.Name
                    };

                    var result = await roleManager.CreateAsync(role);

                    if (result.Succeeded)
                    {
                        foreach (var claim in r.Claims)
                        {
                            await roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(claim.Type, claim.Value));
                        }
                    }
                }
            }

            if (!await userManager.Users.AnyAsync())
            {
                // adding users from seed
                foreach (var user in identityDataConfiguration.Users)
                {
                    var identityUser = new ApplicationUser
                    {
                        UserName = user.Username,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        EmailConfirmed = true,
                    };

                    // if there is no password we create user without password
                    // user can reset password later, because accounts have EmailConfirmed set to true
                    var result = !string.IsNullOrEmpty(user.Password)
                        ? await userManager.CreateAsync(identityUser, user.Password)
                        : await userManager.CreateAsync(identityUser);

                    if (result.Succeeded)
                    {
                        foreach (var claim in user.Claims)
                        {
                            await userManager.AddClaimAsync(identityUser, new System.Security.Claims.Claim(claim.Type, claim.Value));
                        }

                        foreach (var role in user.Roles)
                        {
                            await userManager.AddToRoleAsync(identityUser, role);
                        }
                    }
                }
            }
        }
        #endregion

        #region Store Data Seed
        /// <summary>
        /// Generate default licenses modes.
        /// </summary>
        private static async Task EnsureSeedStoreData(ApplicationDbContext context, StoreDataConfig storeDataConfig)
        {
            // adding products modes from seed
            foreach (var AtomicService in storeDataConfig.AtomicServices)
            {
                if (!await context.AtomicServices.AnyAsync(l => l.Id == AtomicService.Id))
                {
                    using (var transaction = await context.Database.BeginTransactionAsync())
                    {
                        await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[AtomicServices] ON");

                        await context.AtomicServices.AddAsync(AtomicService);
                        await context.SaveChangesAsync();

                        await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[AtomicServices] OFF");

                        await transaction.CommitAsync();
                    }
                }
            }

          
           
        }
        #endregion

    }
}
