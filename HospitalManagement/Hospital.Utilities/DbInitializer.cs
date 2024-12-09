using Hospital.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.Utilities;
using Azure.Identity;
using Hospital.Models;
using Microsoft.AspNetCore.Identity;




namespace Hospital.Utilities
{
    public class DbInitializer : IDbInitializer
    {

        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void Initialize()
        {
            try 
            
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                
                }
            
            
            }
            catch (Exception ex)
            
            { }

            if (!_roleManager.RoleExistsAsync(WebSiteRoles.WebSite_Admin).GetAwaiter().GetResult())
            {

                _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Patient)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Doctor)).GetAwaiter().GetResult();


                 _userManager.CreateAsync(new ApplicationUser
                {

                    UserName = "Harkesh",
                    Email = "Harkesh@xyz.com"

                }, "Harkesh@123").GetAwaiter().GetResult();

                var Appuser= _context.ApplicationUsers.FirstOrDefault(x=>x.Email== "Harkesh@xyz.com");

            }
        }
    }
}
