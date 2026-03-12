using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.Repos.CompanyRepo;
using MarketAnalysisWebApi.Repos.FileStorageRepo;
using MarketAnalysisWebApi.Repos.InnerHelperRepo;
using MarketAnalysisWebApi.Repos.JwtRepo;
using MarketAnalysisWebApi.Repos.KnsConfiGRepo;
using MarketAnalysisWebApi.Repos.OffersRepo;
using MarketAnalysisWebApi.Repos.ProjectRequestRepo;
using MarketAnalysisWebApi.Repos.UserRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;               
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("MarketAnalysisDb")));
builder.Services.AddTransient<IJwtRepo, JwtRepo>();
builder.Services.AddTransient<IUserRepo, UserRepo>();
builder.Services.AddTransient<IInnerHelperRepo, InnerHelperRepo>();
builder.Services.AddTransient<IKnsConfigRepo, KnsConfigRepo>();
builder.Services.AddTransient<ICompanyRepo, CompanyRepo>();
builder.Services.AddTransient<IProjectRequestRepo, ProjectRequestRepo>();
builder.Services.AddTransient<IOfferRepo, OfferRepo>();
builder.Services.AddTransient<IFileStorageRepo, FileStorageRepo>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
//builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };

    // Добавляем поддержку получения токена из cookie
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["accessToken"];
            return Task.CompletedTask;
        }
    };
});
//builder.Services.AddAuthorizationBuilder()
//    .AddPolicy("AdminOnly", policy => policy.RequireRole("SuperAdmin"))
//    .AddPolicy("Moderator", policy => policy.RequireRole("Moderator"))
//    .AddPolicy("Guest", policy => policy.RequireRole("Guest"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    option.AddSecurityRequirement(x => new OpenApiSecurityRequirement());
  
});
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
//    options.AddPolicy("LibrarianOrAdmin", policy =>
//        policy.RequireRole("Admin", "Librarian"));
//});
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
