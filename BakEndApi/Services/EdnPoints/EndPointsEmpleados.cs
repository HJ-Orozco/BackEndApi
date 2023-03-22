using AutoMapper;
using BakEndApi.DTOs;
using BakEndApi.Models;
using BakEndApi.Services.Contrato;
using BakEndApi.Services.Implementacion;

namespace BakEndApi.Services.EdnPoints
{
    public static class EndPointsEmpleados
    {

        public static void MapEndPointsEmpleados(this WebApplication app)
        {
            app.MapGet("/empleado/lista", async (
                IEmpleadoService _empleadoService,
                IMapper _mapper) =>{
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

                    if (_empleado.IdEmpleado != 0)
                        return Results.Ok(_mapper.Map<EmpleadoDTO>(_empleadoCreado));
                    else
                        return Results.StatusCode(StatusCodes.Status500InternalServerError);

                });



            app.MapPut("/empleado/actualizar/{idEmpleado}", async (
                int idEmpleado,
                EmpleadoDTO modelo,
                IEmpleadoService _empleadoService,
                IMapper _mapper) => {

                    var _encontrado = await _empleadoService.Get(idEmpleado);
                    if (_empleadoService is null) return Results.NotFound(idEmpleado);

                    var _empleado = _mapper.Map<Empleado>(modelo);
                    _encontrado.NombreCompleto = _empleado.NombreCompleto;
                    _encontrado.IdDepartamento = _empleado.IdDepartamento;
                    _encontrado.Sueldo = _empleado.Sueldo;
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
        }

       
    }

}
