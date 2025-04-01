using Asp.Versioning;
using CartonCaps.Api.Contexts;
using CartonCaps.Api.Interfaces;
using CartonCaps.Api.OpenApi;
using CartonCaps.Api.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    
}).AddMvc();
builder.Services.AddOpenApi(options =>
{
    options.UseJwtBearerAuthentication();
});

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options => {
    options.IncludeErrorDetails = true;
});

builder.Services.AddAuthorization();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("User", policy => policy.RequireRole("user"));

builder.Services.AddDbContext<ReferralDb>(options => options.UseSqlite("Data Source=referral.db"));
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IReferralService, ReferralService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    
    // seed database with mock data
    app.InitializeDatabase();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();