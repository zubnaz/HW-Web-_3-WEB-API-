using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataProject;
using BusinessLogic.Data.Entitys;
using HW_Web__3_WEB_API_.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using BusinessLogic.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BusinessLogic.Seeders;
using BusinessLogic.ApiModels.Autos;
using FluentValidation.AspNetCore;
using FluentValidation;

string path,pathAzure;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//path = builder.Configuration.GetConnectionString("Connect")!;
pathAzure = builder.Configuration.GetConnectionString("ConnectAzure")!;
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddIdentity<User,IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = true;
}).AddEntityFrameworkStores<AutoDbContext>().AddDefaultTokenProviders();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<AutoDbContext>(opts => opts.UseSqlServer(pathAzure));
builder.Services.AddScoped<IJwtServices, JwtServices>();
builder.Services.AddScoped<IAutosServices, AutoServices>();
builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddScoped(typeof(IDataServices<>),typeof(WorkWithData<>));
//builder.Services.AddScoped<IValidator<CreateAutoModel>, AutoCreateValidator>();
//builder.Services.AddScoped<IValidator<EditAutoModel>, AutoEditValidator>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



var jwtOpt = builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOpt.Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpt.Key)),
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var DB = scope.ServiceProvider.GetRequiredService<AutoDbContext>();
    DB.Database.Migrate();
}

using (IServiceScope scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    SeedExtensions.SeedRoles(serviceProvider).Wait();

    SeedExtensions.SeedAdmin(serviceProvider).Wait();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
