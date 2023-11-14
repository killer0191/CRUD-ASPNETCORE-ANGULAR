using Microsoft.EntityFrameworkCore;
using TiendaCRUD.Business.Services;
using TiendaCRUD.Data.DataContext;
using TiendaCRUD.Data.Repositories;
using TiendaCRUD.Entitys;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<TiendaCrudContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});
builder.Services.AddScoped<IGenericRepository<Cliente>, ClienteRepository>();
builder.Services.AddScoped<IClienteServices, ClienteServices>();
builder.Services.AddScoped<IGenericRepository<Articulo>, ArticuloRepository>();
builder.Services.AddScoped<IArticuloServices, ArticuloServices>();
builder.Services.AddScoped<IGenericRepository<Carrito>, CarritoRepository>();
builder.Services.AddScoped<ICarritoServices, CarritoServices>();
builder.Services.AddScoped<IGenericRepository<Compra>, CompraRepository>();
builder.Services.AddScoped<ICompraServices, CompraServices>();
builder.Services.AddScoped<IGenericRepository<Tiendum>, TiendumRepository>();
builder.Services.AddScoped<ITiendumServices, TiendumServices>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NuevaPolitica");
app.UseAuthorization();

app.MapControllers();

app.Run();
