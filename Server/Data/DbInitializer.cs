using EquipmentsSystem.Server.Services.AuthService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using System.Data;
using System.Xml.Linq;

namespace EquipmentsSystem.Server.Data
{
    public class DbInitializer
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public DbInitializer(IAuthService authService, DataContext context)
        {
            _authService = authService;
            _context = context;

        }
        public async Task Initialize()
        {
            //migrations if they are not applied
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during migration: {ex.Message}");
            }

            if (await _context.Users.CountAsync() == 0)
            {
                try
                {
                    var superAdmin = new User
                    {
                        Email = "3abdEl3aty",
                        DisplayName = "عبدالعاطى وليد عبدالعاطى",
                        Role = "SuperAdmin",
                        Rank = "رقيب"
                    };
                    await _authService.Register(superAdmin, "1111");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Error during addSuperAdmin: {ex.Message}");
                }

            }
        }

    }
}
