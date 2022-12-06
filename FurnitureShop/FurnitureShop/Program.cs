using FurnitureShop.Data;
using FurnitureShop.Entities;
using FurnitureShop.Middlewares;
using FurnitureShop.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("database")).UseSnakeCaseNamingConvention();
});

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseErrorHandlerMiddleware();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
