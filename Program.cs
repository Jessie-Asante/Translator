using Microsoft.EntityFrameworkCore;
using StringConverter.Data;
using Microsoft.AspNetCore.Identity;
using StringConverter.Data.Interfaces;
using StringConverter.Data.Repositories;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<StringConverterDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StringConverterConnectionString"),options=>options.MigrationsAssembly(typeof(StringConverterDbContext).Assembly.GetName().Name)));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<StringConverterDbContext>();



// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.ApplicationServices();
builder.Services.AddScoped<ITranslateText,TranslateTexts>();
builder.Services.AddHttpClient("StringHttpClient");
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

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CRUD}/{action=GetText}/{id?}");

app.Run();
