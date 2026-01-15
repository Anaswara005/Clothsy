using Clothsy.Data;
using Clothsy.Models.Web.Auth;
using Clothsy.Services.AuthServices.Signup_Services;

public static class WebUserSeeder
{
    public static void Seed(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var passwordService = scope.ServiceProvider.GetRequiredService<IPasswordService>();

        // Prevent duplicate seeding
        if (context.WebUsers.Any())
            return;

        // 🔴 ADMIN ACCOUNT
        context.WebUsers.Add(new WebUser
        {
            Email = "admin@clothsy.in",
            PhoneNumber = "9999999999",
            PasswordHash = passwordService.HashPassword("Admin@123"),
            Role = "ADMIN",
            IsActive = true
        });

        // 🟢 HUB ACCOUNTS (KERALA)
        var hubs = new List<(string Email, string Phone, string Code)>
        {
            ("clothsy.trivandrum@clothsy.in", "+919000000000", "TVM0000"),
            ("clothsy.kollam@clothsy.in", "+919000000001", "KLM0001"),
            ("clothsy.pathanamthitta@clothsy.in", "+919000000002", "PTA0002"),
            ("clothsy.alappuzha@clothsy.in", "+919000000003", "ALP0003"),
            ("clothsy.kottayam@clothsy.in", "+919000000004", "KTM0004"),
            ("clothsy.idukki@clothsy.in", "+919000000005", "IDK0005"),
            ("clothsy.ernakulam@clothsy.in", "+919000000006", "EKM0006"),
            ("clothsy.thrissur@clothsy.in", "+919000000007", "TSR0007"),
            ("clothsy.palakkad@clothsy.in", "+919000000008", "PKD0008"),
            ("clothsy.malappuram@clothsy.in", "+919000000009", "MLP0009"),
            ("clothsy.kozhikode@clothsy.in", "+919000000010", "CLT0010"),
            ("clothsy.wayanad@clothsy.in", "+919000000011", "WND0011"),
            ("clothsy.kannur@clothsy.in", "+919000000012", "KNR0012"),
            ("clothsy.kasaragod@clothsy.in", "+919000000013", "KSD0013")
        };

        foreach (var hub in hubs)
        {
            var rawPassword = $"Hub@{hub.Code}";

            context.WebUsers.Add(new WebUser
            {
                Email = hub.Email,
                PhoneNumber = hub.Phone,
                PasswordHash = passwordService.HashPassword(rawPassword),
                Role = "HUB",
                IsActive = true,
                HubCode = hub.Code
            });

        }

        context.SaveChanges();
    }
}
