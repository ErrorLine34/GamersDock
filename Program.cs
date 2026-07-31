using GamersDock.Data;
using GamersDock.Endpoints;
using GamersDock.Entities;
using GamersDock.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add standard services to the container
builder.Services.AddOpenApi();
builder.Services.AddDefaultIdentity<Users>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<GamersDockContext>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    "Data Source=gamersdock.db";

builder.Services.AddDbContext<GamersDockContext>(options =>
    options.UseSqlite(connectionString));

// Repositories
builder.Services.AddScoped<IUserProfileRepository, ProfilesRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IInstanceSettingsRepository, InstanceSettingsRepository>();
builder.Services.AddScoped<IAccountLinkRepository, AccountLinkRepository>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

// AllowCredentials is needed because Identity auth uses cookies
const string frontendCorsPolicy = "Frontend";
builder.Services.AddCors(options =>
{
    options.AddPolicy(frontendCorsPolicy, policy =>
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});

// Build the application (No builder.Services calls allowed after this point)
var app = builder.Build();

// Apply any pending EF Core migrations automatically
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GamersDockContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    app.MapScalarApiReference();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors(frontendCorsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapGameEndpoints();
app.MapAuthEndpoints();
app.MapProfileEndpoints();
app.MapUserEndpoints();
app.MapInstanceSettingsEndpoints();

app.Run();