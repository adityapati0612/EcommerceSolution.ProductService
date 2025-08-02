using EcommerceSolution.ProductService.DataAccessLayer;
using EcommerceSolution.ProductService.BuisnessLogicLayer;
using FluentValidation.AspNetCore;
using Ecommerce.ProductMicroService.API.Middleware;
using Swashbuckle.AspNetCore.SwaggerGen;
using Ecommerce.ProductMicroService.API.APIEndpoints;


var builder = WebApplication.CreateBuilder(args);

//Add DAL and BLL services
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBuisnessLogicLayer();

builder.Services.AddControllers();

//Fluent Validations
builder.Services.AddFluentValidationAutoValidation();

//add the swagger 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngular", policy =>
//    {
//        policy.WithOrigins("http://localhost:4200")
//              .AllowAnyMethod()
//              .WithHeaders("Content-Type", "Authorization") // add any other custom headers here
//              .AllowCredentials();
//    });
//});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyMethod();
    });
});
var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseRouting();

app.UseCors();

app.UseSwagger(); //Adds endpoint that can serve the swagger json files
app.UseSwaggerUI();
//Auth
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapProductAPIEndpoints();

app.Run();
