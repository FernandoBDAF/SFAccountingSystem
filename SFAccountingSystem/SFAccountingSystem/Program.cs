using Microsoft.EntityFrameworkCore;
using SFAccountingSystem.Core;
using SFAccountingSystem.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

builder.Services.AddScoped<OFXService>();
builder.Services.AddScoped<RecordOFXService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<RecordOFXSubGroupService>();
builder.Services.AddScoped<BalanceSheetService>();
builder.Services.AddScoped<IntermediationsService>();
builder.Services.AddScoped<InvoicesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
