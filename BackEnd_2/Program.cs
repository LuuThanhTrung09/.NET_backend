using Game106_API.AppDataContext;
using Game106_API.Services;
using Microsoft.EntityFrameworkCore;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Services
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<IAccountServices, AccountsServices>();
        builder.Services.AddScoped<ICharacterServices, CharacterServices>();
        builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSetting"));
        builder.Services.AddScoped<IEmailServices, EmailServices>();
        builder.Services.AddTransient<IEmailServices, EmailServices>();
        
        builder.Services.AddControllersWithViews()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });

        var app = builder.Build();

        // Swagger
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

       
        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAntiforgery();
        app.UseAuthorization();

        
        app.MapGet("/testdb", (AppDbContext db) =>
        {
            return db.Database.CanConnect() ? "Connected" : "Failed";
        });

        //Route cho API controllers ([ApiController])
        app.MapControllers();

        //Route cho Web Admin (AdminController : Controller)
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}