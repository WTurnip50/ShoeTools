using Dapper.Contrib.Extensions;
using ShoeTools.Api.DataAccess;
using ShoeTools.Api.DataAccess.Interfaces;
using ShoeTools.Api.Repositories;
using ShoeTools.Api.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductDetailsRepository, ProductDetailsRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
builder.Services.AddScoped<IDBContext, DBContext>();

SqlMapperExtensions.TableNameMapper = entityType =>
{
    var name = entityType.Name;
    if (name.Contains("ShoeTools.Core.Entities"))
        name = name.Replace("ShoeTools.Core.Entities", "");
    var letters = name.ToCharArray();
    letters[0] = char.ToUpper(letters[0]);
    return new string(letters);
};

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();