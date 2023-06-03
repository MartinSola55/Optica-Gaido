using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Optica_Gaido.Data.Seeding
{
    public class Seeder : ISeeder
    {
        private readonly ApplicationDbContext _db;
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserManager<IdentityRole> _roleManager;

        public Seeder(ApplicationDbContext db, /*UserManager<ApplicationUser> userManager,*/ UserManager<IdentityRole> roleManager)
        {
            _db = db;
            //_userManager = userManager;
            _roleManager = roleManager;
        }
        public void Seed()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

                throw;
            }

            //if (_db.Roles.Any(x => x.Name == CNT))
            //{

            //}
        }
    }
}
