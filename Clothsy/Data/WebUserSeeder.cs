using Clothsy.Data;
using Clothsy.Models.Donation;
using Clothsy.Models.Web.Auth;
using Clothsy.Services.AuthServices.Signup_Services;

public static class WebUserSeeder
{
    public static void Seed(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var passwordService = scope.ServiceProvider.GetRequiredService<IPasswordService>();
        var districtOrder = new Dictionary<string, int>
{
    { "Trivandrum", 1 },
    { "Kollam", 2 },
    { "Pathanamthitta", 3 },
    { "Alappuzha", 4 },
    { "Kottayam", 5 },
    { "Idukki", 6 },
    { "Ernakulam", 7 },
    { "Thrissur", 8 },
    { "Palakkad", 9 },
    { "Malappuram", 10 },
    { "Kozhikode", 11 },
    { "Wayanad", 12 },
    { "Kannur", 13 },
    { "Kasaragod", 14 }
};

        // Hub master data
        var hubList = new List<(string Name, string Email, string Phone, string Code, string District)>
        {
            ("Clothsy Central Hub - Trivandrum", "clothsy.trivandrum@clothsy.in", "+919000000000", "TVM0000", "Trivandrum"),
            ("Clothsy Central Hub - Kollam", "clothsy.kollam@clothsy.in", "+919000000001", "KLM0001", "Kollam"),
            ("Clothsy Central Hub - Pathanamthitta", "clothsy.pathanamthitta@clothsy.in", "+919000000002", "PTA0002", "Pathanamthitta"),
            ("Clothsy Central Hub - Alappuzha", "clothsy.alappuzha@clothsy.in", "+919000000003", "ALP0003", "Alappuzha"),
            ("Clothsy Central Hub - Kottayam", "clothsy.kottayam@clothsy.in", "+919000000004", "KTM0004", "Kottayam"),
            ("Clothsy Central Hub - Idukki", "clothsy.idukki@clothsy.in", "+919000000005", "IDK0005", "Idukki"),
            ("Clothsy Central Hub - Ernakulam", "clothsy.ernakulam@clothsy.in", "+919000000006", "EKM0006", "Ernakulam"),
            ("Clothsy Central Hub - Thrissur", "clothsy.thrissur@clothsy.in", "+919000000007", "TSR0007", "Thrissur"),
            ("Clothsy Central Hub - Palakkad", "clothsy.palakkad@clothsy.in", "+919000000008", "PKD0008", "Palakkad"),
            ("Clothsy Central Hub - Malappuram", "clothsy.malappuram@clothsy.in", "+919000000009", "MLP0009", "Malappuram"),
            ("Clothsy Central Hub - Kozhikode", "clothsy.kozhikode@clothsy.in", "+919000000010", "CLT0010", "Kozhikode"),
            ("Clothsy Central Hub - Wayanad", "clothsy.wayanad@clothsy.in", "+919000000011", "WND0011", "Wayanad"),
            ("Clothsy Central Hub - Kannur", "clothsy.kannur@clothsy.in", "+919000000012", "KNR0012", "Kannur"),
            ("Clothsy Central Hub - Kasaragod", "clothsy.kasaragod@clothsy.in", "+919000000013", "KSD0013", "Kasaragod")
        };

        // Create Hubs if they don't exist
        if (!context.Hubs.Any())
        {
            Console.WriteLine("🟢 Creating Hubs...");

            context.Hubs.AddRange(
                hubList.Select(h => new Hub
                {
                    Name = h.Name,
                    Address = h.District,
                    District = h.District,

                    // ✅ IMPORTANT
                    DistrictId = districtOrder[h.District],

                    Email = h.Email,
                    Phone = h.Phone,
                    HubCode = h.Code,
                    IsActive = true
                })
            );

            context.SaveChanges();
            Console.WriteLine("✅ Hubs created successfully!");
        }
        var hubsWithoutDistrictId = context.Hubs
            .Where(h => h.DistrictId == 0)
            .ToList();

        if (hubsWithoutDistrictId.Any())
        {
            Console.WriteLine("🔧 Fixing DistrictId for existing hubs...");

            foreach (var hub in hubsWithoutDistrictId)
            {
                if (districtOrder.TryGetValue(hub.District, out var districtId))
                {
                    hub.DistrictId = districtId;
                    Console.WriteLine($"   ✅ {hub.District} → {districtId}");
                }
            }

            context.SaveChanges();
            Console.WriteLine("✅ DistrictId fixed for hubs!");
        }


        // Create Admin if doesn't exist
        if (!context.WebUsers.Any(u => u.Email == "admin@clothsy.in"))
        {
            Console.WriteLine("🔴 Creating admin user...");
            context.WebUsers.Add(new WebUser
            {
                Email = "admin@clothsy.in",
                PhoneNumber = "9999999999",
                PasswordHash = passwordService.HashPassword("Admin@123"),
                Role = "ADMIN",
                IsActive = true
            });
            context.SaveChanges();
            Console.WriteLine("✅ Admin user created!");
        }

        // Create Hub Users if they don't exist
        foreach (var hub in hubList)
        {
            if (!context.WebUsers.Any(u => u.Email == hub.Email))
            {
                Console.WriteLine($"🟢 Creating hub user: {hub.Email}");
                context.WebUsers.Add(new WebUser
                {
                    Email = hub.Email,
                    PhoneNumber = hub.Phone,
                    PasswordHash = passwordService.HashPassword($"Hub@{hub.Code}"),
                    Role = "HUB",
                    IsActive = true,
                    HubCode = hub.Code
                });
            }
        }
        context.SaveChanges();

        // Fix existing hub users without HubCode
        var hubUsers = context.WebUsers
            .Where(u => u.Role == "HUB" && string.IsNullOrEmpty(u.HubCode))
            .ToList();

        if (hubUsers.Any())
        {
            Console.WriteLine($"🔧 Fixing {hubUsers.Count} hub users without HubCode...");

            var hubCodeMapping = hubList.ToDictionary(h => h.Email, h => h.Code);

            foreach (var user in hubUsers)
            {
                if (hubCodeMapping.TryGetValue(user.Email, out var hubCode))
                {
                    user.HubCode = hubCode;
                    Console.WriteLine($"   ✅ Updated {user.Email} → {hubCode}");
                }
            }

            context.SaveChanges();
            Console.WriteLine("✅ HubCode updates complete!");
        }

        Console.WriteLine("✅ Seeder complete!");
    }
}