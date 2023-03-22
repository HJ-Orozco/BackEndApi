using AutoMapper;
using BakEndApi.DTOs;
using BakEndApi.Services.Contrato;
using BakEndApi.Services.Implementacion;

namespace BakEndApi.Services.EdnPoints
{
    public static class EndPointsDepartamentos
    {

        public static void MapEndPointsDepartamentos( this IEndpointRouteBuilder app)
        {
            
            app.MapGet("/departamento/lista", async (
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
        }
      
    }

}
