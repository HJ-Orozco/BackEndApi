using BakEndApi.Models;

namespace BakEndApi.Services.Contrato
{
    public interface IDepartamentoService
    {
        Task<List<Departamento>> GetList();
    }
}
