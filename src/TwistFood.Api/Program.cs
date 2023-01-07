using CarShop.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using TwistFood.Api.DbContexts;
using TwistFood.Api.Middlewares;
using TwistFood.DataAccess.Common.Interfaces;
using TwistFood.DataAccess.Interfaces;
using TwistFood.DataAccess.Repositories;
using TwistFood.Service.Interfaces.Accounts;
using TwistFood.Service.Security;
using TwistFood.Service.Services.Accounts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();


string connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ISendToPhoneNumberService, SendToPhoneNumberService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IVerifyEmailService, VerifyEmailService>();



var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
