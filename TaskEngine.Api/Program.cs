using Microsoft.EntityFrameworkCore;
using TaskEngine.Domain.Interfaces;
using TaskEngine.Infrastructure.Persistence;
using TaskEngine.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

//Registro el context de la base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
{
    //options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}
);
//Registro las interfaces y sus implementaciones en el contenedor de dependencias
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITaskRepository,TaskRepository>();

//Registro MediatR para manejar los comandos y consultas
// Usamos una clase cualquiera que esté dentro del proyecto Application para darle la ubicación
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(typeof(TaskEngine.Application.Categories.Commands.CreateCategory.CreateCategoryCommand).Assembly));

// Esta es la forma correcta para la versión 13.0.1
builder.Services.AddAutoMapper(typeof(TaskEngine.Application.Mappings.MappingProfile).Assembly);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



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
