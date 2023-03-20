using BakEndApi.Models;
using Microsoft.EntityFrameworkCore;

using BakEndApi.Services.Contrato;
using BakEndApi.Services.Implementacion;

using AutoMapper;
using BakEndApi.DTOs;
using BakEndApi.Utilidades;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DbempleadoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSql"));
});

builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


#region PETICIONES API REST
app.MapGet("/departamento/lista",async(
    IDepartamentoService _departamentoService,
    IMapper _mapper
    ) =>
{
    var listaDepartamento = await _departamentoService.GetList();
    var listaDepartamentoDTO = _mapper.Map<List<DepartamentoDTO>>(listaDepartamento);

    if (listaDepartamentoDTO.Count > 0)
    {
        return Results.Ok(listaDepartamentoDTO);
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapGet("/empleado/lista", async (
    IEmpleadoService _empleadoService,
    IMapper _mapper
    ) =>
{
    var listaEmpleado = await _empleadoService.GetList();
    var listaEmpleadoDTO = _mapper.Map<List<EmpleadoDTO>>(listaEmpleado);

    if (listaEmpleadoDTO.Count > 0)
    {
        return Results.Ok(listaEmpleadoDTO);
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapPost("/empleado/guardar", async (
    EmpleadoDTO modelo,
    IEmpleadoService _empleadoService,
    IMapper _mapper
    ) => {

        var _empleado = _mapper.Map<Empleado>(modelo);
        var _empleadoCreado = await _empleadoService.Add(_empleado);

        if(_empleado.IdEmpleado != 0)
            return Results.Ok(_mapper.Map<EmpleadoDTO>(_empleadoCreado));
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        
});



app.MapPut("/empleado/actualizar/{idEmpleado}", async(
    int idEmpleado,
    EmpleadoDTO modelo,
    IEmpleadoService _empleadoService,
    IMapper _mapper) => {

        var _encontrado = await _empleadoService.Get(idEmpleado);
        if (_empleadoService is null ) return Results.NotFound(idEmpleado);

        var _empleado = _mapper.Map<Empleado>(modelo);
        _encontrado.NombreCompleto = _empleado.NombreCompleto;
        _encontrado.IdDepartamento = _empleado.IdDepartamento;
        _encontrado.Sueldo= _empleado.Sueldo;
        _encontrado.FechaContrato = _empleado.FechaContrato;

        var respuesta = await _empleadoService.Update(_encontrado);

        if (respuesta)
            return Results.Ok(_mapper.Map<EmpleadoDTO>(_encontrado));
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);

    });


app.MapDelete("/empleado/eliminar/{idEmpleado}", async (
    int idEmpleado,
    IEmpleadoService _empleadoService) => {

        var _encontrado = await _empleadoService.Get(idEmpleado);
        if (_empleadoService is null) return Results.NotFound(idEmpleado);

        var respuesta = await _empleadoService.Delete(_encontrado);

        if (respuesta)
            return Results.Ok();
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
    });
#endregion
app.Run();

