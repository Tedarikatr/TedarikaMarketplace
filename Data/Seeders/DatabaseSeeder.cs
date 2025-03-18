using Data.Databases;
using Entity.Auths;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Seeders
{
    public class DatabaseSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DatabaseSeeder> _logger;

        public DatabaseSeeder(ApplicationDbContext context, ILogger<DatabaseSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            try
            {
                _logger.LogInformation("Database seeding başladı.");

                // SuperAdmin kontrolü
                if (!await _context.AdminUsers.AnyAsync(u => u.IsSuperAdmin))
                {
                    var superAdminSalt = BCrypt.Net.BCrypt.GenerateSalt();
                    var superAdmin = new AdminUser
                    {
                        Name = "Sistem",
                        LastName = "Yöneticisi",
                        Email = "superadmin@tedarika.com",
                        Phone = "5555555555",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("SuperAdmin123", superAdminSalt),
                        PasswordSalt = superAdminSalt,
                        IsSuperAdmin = true,
                        UserType = UserType.Admin,
                        Role = AdminRole.SuperAdmin
                    };

                    await _context.AdminUsers.AddAsync(superAdmin);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("SuperAdmin başarıyla oluşturuldu. Email: {Email}", superAdmin.Email);
                }
                else
                {
                    _logger.LogInformation("SuperAdmin zaten mevcut, ekleme yapılmadı.");
                }

                // Standart Admin kontrolü
                if (!await _context.AdminUsers.AnyAsync(u => !u.IsSuperAdmin))
                {
                    var adminSalt = BCrypt.Net.BCrypt.GenerateSalt();
                    var admin = new AdminUser
                    {
                        Name = "Standart",
                        LastName = "Admin",
                        Email = "admin@tedarika.com",
                        Phone = "5444444444",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123", adminSalt),
                        PasswordSalt = adminSalt,
                        IsSuperAdmin = false,
                        UserType = UserType.Admin,
                        Role = AdminRole.StandardAdmin
                    };

                    await _context.AdminUsers.AddAsync(admin);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Standart Admin başarıyla oluşturuldu. Email: {Email}", admin.Email);
                }
                else
                {
                    _logger.LogInformation("Standart Admin zaten mevcut, ekleme yapılmadı.");
                }

                _logger.LogInformation("Database seeding tamamlandı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database seeding sırasında bir hata oluştu.");
            }
        }
    }
}
