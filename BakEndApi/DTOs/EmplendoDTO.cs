﻿namespace BakEndApi.DTOs
{
    public class EmplendoDTO
    {
        public int IdEmpleado { get; set; }

        public string? NombreCompleto { get; set; }

        public int? IdDepartamento { get; set; }
        public string? NombreDepartamento { get; set; }

        public int? Sueldo { get; set; }

        public string? FechaContrato { get; set; }
    }
}