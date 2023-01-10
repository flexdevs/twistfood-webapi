using CarShop.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using TwistFood.Api.DbContexts;
using TwistFood.Api.Middlewares;
using TwistFood.DataAccess.Interfaces;
using TwistFood.DataAccess.Repositories;
using TwistFood.Domain.Entities.Products;
using TwistFood.Service.Interfaces;
using TwistFood.Service.Interfaces.Accounts;
using TwistFood.Service.Interfaces.Categories;
using TwistFood.Service.Interfaces.Common;
using TwistFood.Service.Interfaces.Delivers;
using TwistFood.Service.Interfaces.Discounts;
using TwistFood.Service.Interfaces.Operators;
using TwistFood.Service.Interfaces.Products;
using TwistFood.Service.Security;
using TwistFood.Service.Services;
using TwistFood.Service.Services.Accounts;
using TwistFood.Service.Services.Categories;
using TwistFood.Service.Services.Common;
using TwistFood.Service.Services.Delivers;
using TwistFood.Service.Services.Discounts;
using TwistFood.Service.Services.Operators;
using TwistFood.Service.Services.Products;

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
builder.Services.AddScoped<IDeliverRegisterService, DeliverRegisterService>();
builder.Services.AddScoped<ISendToPhoneNumberService, SendToPhoneNumberService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IVerifyEmailService, VerifyEmailService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOperatorService, OperatorService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPaginatorService, PaginatorService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();



var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseStaticFiles();

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
