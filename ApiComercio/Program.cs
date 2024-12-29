using CapaDatos.Models;
using CapaEntidad.Clases;
using Microsoft.EntityFrameworkCore;
using Application_Layer.DTO_Clases;
using ApplicationLayer.DTO_Clases;
using CapaEntidad.Interfaces;
using ApplicationLayer.Interfaces;
using ApplicationLayer.Clases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configurar Acceso de datos
var conn = builder.Configuration.GetConnectionString("Conn");
builder.Services.AddDbContext<PruebademoContext>(x => x.UseSqlServer(conn));

//Configurar las interfaces para que el controlador las pueda usar
builder.Services.AddScoped<IGeneric<ClienteDTO, Cliente>, LogicaCliente>();
builder.Services.AddScoped<IGeneric<ProductoDTO, Producto>, LogicaProducto>();
builder.Services.AddScoped<IGeneric<VentaDTO, Venta>, LogicaVenta>();
builder.Services.AddScoped<IVentaItem<VentasItemDTO>, LogicaVentaItem>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
