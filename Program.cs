using Microsoft.AspNetCore.Cors.Infrastructure;
using Producor_Web_API.Controllers;
using Producor_Web_API.Interface;
using Producor_Web_API.Model;
using Producor_Web_API.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
builder.Services.AddScoped<IRabitMQProducer, RabbitMQService>();
builder.Services.AddSwaggerGen();
var corsBuilder = new CorsPolicyBuilder();
corsBuilder.AllowAnyHeader();
corsBuilder.AllowAnyMethod();
corsBuilder.AllowAnyOrigin(); // For anyone access.
corsBuilder.WithOrigins("http://localhost:4200"); // for a specific url. Don't add a forward slash on the end!
//corsBuilder.WithOrigins("http://ouepfrontend.applewoodofficial.com")
//                     .SetIsOriginAllowedToAllowWildcardSubdomains()
//                .AllowAnyHeader()
//                .AllowAnyMethod();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorePolicy", corsBuilder.Build());
});
builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());
//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorePolicy");
app.UseAuthentication();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
