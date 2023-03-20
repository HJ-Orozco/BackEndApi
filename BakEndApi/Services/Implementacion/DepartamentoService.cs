using Microsoft.EntityFrameworkCore;
using BakEndApi.Models;
using BakEndApi.Services.Contrato;

namespace BakEndApi.Services.Implementacion
{
    
    public class DepartamentoService: IDepartamentoService
    {
        private DbempleadoContext _dbContext;

        public DepartamentoService(DbempleadoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Departamento>> GetList()
        {
            try
            {
                List<Departamento> lista = new List<Departamento>();
                lista = await _dbContext.Departamentos.ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
