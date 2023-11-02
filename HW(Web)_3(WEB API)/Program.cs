using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataProject;
using DataProject.Data.Entitys;
using HW_Web__3_WEB_API_.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

string path;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
path = builder.Configuration.GetConnectionString("Connect")!;
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<AutoDbContext>().AddDefaultTokenProviders();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AutoDbContext>(opts => opts.UseSqlServer(path));
builder.Services.AddScoped<IAutosServices, AutoServices>();
builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
