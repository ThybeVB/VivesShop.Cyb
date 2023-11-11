using Microsoft.EntityFrameworkCore;
using VivesShop.Cyb.Ui.Mvc.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddSingleton<VivesShopDbContext>();
builder.Services.AddDbContext<VivesShopDbContext>(options =>
{
    options.UseInMemoryDatabase(nameof(VivesShopDbContext));
    //options.UseSqlServer("Server=.\\SqlExpress;Database=VivesShop;Trusted_Connection=True;TrustServerCertificate=true");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    using var scope = app.Services.CreateScope();
    
    var dbContext = scope.ServiceProvider.GetRequiredService<VivesShopDbContext>();
    if (dbContext.Database.IsInMemory())
    {
        dbContext.Seed();
    }
    
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
