using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_MiniProject.Data;
using MVC_MiniProject.Models;
using MVC_MiniProject.Services;
using MVC_MiniProject.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// ── Identity (Email Təsdiqi Tələbi ilə) ────────────────────────────────────
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;

    // Email təsdiqlənməyənə qədər login olmağa icazə vermir:
    options.SignIn.RequireConfirmedEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/Account/Login";
    opt.AccessDeniedPath = "/Account/AccessDenied";
});
// ─────────────────────────────────────────────────────────────────────────

// ── Servislər ────────────────────────────────────────────────────────────
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<IAboutLeftService, AboutLeftService>();
builder.Services.AddScoped<IAboutRightService, AboutRightService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IDetailService, DetailService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// ── Seed: Rollar və Default İstifadəçilər ─────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    db.Database.Migrate();

    string[] roles = { "SuperAdmin", "Admin", "Member" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

    // SuperAdmin
    var superAdminEmail = "huseyn.super@gmail.com";
    if (await userManager.FindByEmailAsync(superAdminEmail) == null)
    {
        var user = new AppUser
        {
            FullName = "Huseyn Valiyev",
            UserName = "huseyn92",
            Email = superAdminEmail,
            EmailConfirmed = true
        };
        await userManager.CreateAsync(user, "Huseyn12345!");
        await userManager.AddToRoleAsync(user, "SuperAdmin");
    }

    // Admin
    var adminEmail = "nicat.admin@gmail.com";
    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var user = new AppUser
        {
            FullName = "Nicat Aliyev",
            UserName = "NicatAdmin22",
            Email = adminEmail,
            EmailConfirmed = true
        };
        await userManager.CreateAsync(user, "Nicat12345!");
        await userManager.AddToRoleAsync(user, "Admin");
    }

    // Member
    var memberEmail = "tural.user@gmail.com";
    if (await userManager.FindByEmailAsync(memberEmail) == null)
    {
        var user = new AppUser
        {
            FullName = "Tural Hasanov",
            UserName = "Tural123",
            Email = memberEmail,
            EmailConfirmed = true
        };
        await userManager.CreateAsync(user, "Tural12345!");
        await userManager.AddToRoleAsync(user, "Member");
    }

    // Seed Settings
    if (!db.Settings.Any())
    {
        db.Settings.AddRange(
            new Setting { Key = "AboutTitle", Value = "About Us" },
            new Setting { Key = "Courses", Value = "Our Courses" },
            new Setting { Key = "CoursesDesc", Value = "Browse our available courses below." },
            new Setting { Key = "HomeTitle", Value = "Welcome to eLearning" },
            new Setting { Key = "HomeDesc", Value = "The best place to learn online." },
            new Setting { Key = "HomeButtonText", Value = "Learn More" },
            new Setting { Key = "Logo", Value = "logo.png" }
        );
        await db.SaveChangesAsync();
    }

    if (!db.AboutRights.Any())
    {
        db.AboutRights.Add(new AboutRight
        {
            Title = "Who We Are",
            Description = "We are an online learning platform dedicated to providing quality education.",
            RightImage = "about_1.jpg"
        });
        await db.SaveChangesAsync();
    }

    if (!db.AboutLefts.Any())
    {
        db.AboutLefts.Add(new AboutLeft
        {
            Title = "Our Mission",
            Description = "Our mission is to make learning accessible and effective for all.",
            LeftImage = "about_1.jpg"
        });
        await db.SaveChangesAsync();
    }
}
// ─────────────────────────────────────────────────────────────────────────

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();