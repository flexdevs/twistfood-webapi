
using TwistFood.Api.Configurations;
using Microsoft.EntityFrameworkCore;
using TwistFood.Api.DbContexts;
using TwistFood.Api.Middlewares;
using TwistFood.DataAccess.Interfaces;
using TwistFood.DataAccess.Repositories;
using TwistFood.Domain.Entities.Products;
using TwistFood.Service.Interfaces;
using TwistFood.Service.Interfaces.Accounts;
using TwistFood.Service.Interfaces.Admins;
using TwistFood.Service.Interfaces.Categories;
using TwistFood.Service.Interfaces.Common;
using TwistFood.Service.Interfaces.Delivers;
using TwistFood.Service.Interfaces.Discounts;
using TwistFood.Service.Interfaces.Operators;
using TwistFood.Service.Interfaces.Orders;
using TwistFood.Service.Interfaces.Products;
using TwistFood.Service.Security;
using TwistFood.Service.Services;
using TwistFood.Service.Services.Accounts;
using TwistFood.Service.Services.Admins;
using TwistFood.Service.Services.Categories;
using TwistFood.Service.Services.Common;
using TwistFood.Service.Services.Delivers;
using TwistFood.Service.Services.Discounts;
using TwistFood.Service.Services.Operators;
using TwistFood.Service.Services.Orders;
using TwistFood.Service.Services.Products;
using TwistFood.Service.Common.Helpers;
using System.Drawing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

builder.ConfigureAuth();
builder.Services.ConfigureSwaggerAuthorize();

string connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IDeliverRegisterService, DeliverRegisterService>();
builder.Services.AddScoped<IVerifyPhoneNumberService, VerifyPhoneNumberService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IVerifyEmailService, VerifyEmailService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOperatorService, OperatorService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPaginatorService, PaginatorService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDeteilsService, OrderDeteilService>();
builder.Services.AddScoped<IAdminService, AdminService>();


var app = builder.Build();

/*app.Urls.Add("http://185.217.131.186:5055");
app.Urls.Add("http://localhost:5055");*/

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Services.GetService<IHttpContextAccessor>() != null)
    HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();

app.UseCors("corspolicy");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
