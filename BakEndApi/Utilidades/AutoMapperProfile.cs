using AutoMapper;
using BakEndApi.DTOs;
using BakEndApi.Models;
using System.Globalization;

namespace BakEndApi.Utilidades
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            #region Departamento
            CreateMap<Departamento, DepartamentoDTO>().ReverseMap();
            #endregion

            #region Empleado
            /*
             origen = Emplado
             destino = EmplendoDTO
             */
            CreateMap<Empleado, EmplendoDTO>().
                ForMember(destino => 
                destino.NombreDepartamento, opt => opt.MapFrom(origen => origen.IdDepartamentoNavigation.Nombre)).
                ForMember(destino =>
                destino.FechaContrato, opt => opt.MapFrom(origen => origen.FechaContrato.Value.ToString("dd/MM/yyyy")));
            /*
             origen = EmplendoDTO
             destino = Emplendo
             */
            CreateMap<EmplendoDTO, Empleado>().
                ForMember(destino => destino.IdDepartamentoNavigation, opt =>opt.Ignore()).
                ForMember(destino => destino.FechaContrato,
                opt => opt.MapFrom(orige => DateTime.ParseExact(orige.FechaContrato,"dd/MM/yyyy",CultureInfo.InvariantCulture)));
            #endregion
        }
    }
}
