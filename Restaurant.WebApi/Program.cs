using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.WebApi.Models.Context;
using Restaurant.WebApi.Models.Entities;
using Restaurant.WebApi.Validations;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IValidator<Category>, CategoryValidation>();
builder.Services.AddScoped<IValidator<Product>, ProductValidation>();
builder.Services.AddScoped<IValidator<Message>, MessageValidation>();
builder.Services.AddScoped<IValidator<Feature>,FeatureValidation>();
builder.Services.AddScoped<IValidator<Contact>, ContactValidation>();
builder.Services.AddScoped<IValidator<Chef>, ChefValidation>();
builder.Services.AddScoped<IValidator<Service>, ServiceValidation>();
builder.Services.AddScoped<IValidator<Testimonial>, TestimonialValidation>();
builder.Services.AddScoped<IValidator<Event>, EventValidation>();
builder.Services.AddScoped<IValidator<Notification>, NotificationValidation>();
builder.Services.AddScoped<IValidator<About>, AboutValidation>();
builder.Services.AddScoped<IValidator<Reservation>, ReservationValidation>();
builder.Services.AddScoped<IValidator<Photo>, PhotoValidation>();





var conString = builder.Configuration.GetConnectionString("Default") ??
     throw new InvalidOperationException("Connection string 'Default'" +
    " not found.");
builder.Services.AddDbContext<ApiContext>(options =>
    options.UseSqlServer(conString));

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
