using MongoDb;
using MongoDb.Services;
using MongoDb.Settings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoDbConnection"));

var mongoSettings = builder.Configuration.GetSection("MongoDbConnection").Get<MongoSettings>();
builder.Services.AddDbContext<MongoDbContext>(options =>
    options.UseMongoDB(mongoSettings.ConnectionString, mongoSettings.DatabaseName)
);

builder.Services.AddScoped<UserService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    _ = app.MapOpenApi();
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
