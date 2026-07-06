using APIlogin.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models; // Fix 1: Added .Models

var builder = WebApplication.CreateBuilder(args);

// 1. Setup Entity Framework to use your SQLEXPRESS connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Enable Authorization Services
builder.Services.AddAuthorization();

// 3. Activate Identity API Endpoints (This does all the heavy lifting for login/register)
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<AppDbContext>();

// 4. Add Swagger (Since you are using the MVC template, we must add this manually)
builder.Services.AddControllersWithViews(); // Keeps your existing MVC setup intact
builder.Services.AddEndpointsApiExplorer();

// 4. Add Swagger (Configured for Bearer Tokens)
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: 'Bearer eyJhbGciOiJIUzI1Ni...'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Fix 2: This line is required to actually build the app before you configure it!
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Enable Swagger only in Development mode
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 5. Map the pre-built Identity Endpoints (/login, /register, etc.)
app.MapIdentityApi<IdentityUser>();

// Maps your MVC routes (since you picked the MVC template)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// A test endpoint that REQUIRES the user to be logged in
app.MapGet("/secret-data", () => "Congratulations! Your token worked and you are authenticated!").RequireAuthorization();

app.Run();