using Agence.Server.Models;
using Agence.Shared;

namespace Agence.Server.Services;

public interface IRelatorioService
{
    Task<List<ConsultoresDto>?> GetConsultores();
    Task<List<Fatura>?> GetFaturas(string consultor, DateTime start, DateTime end);
    Task<List<ResponseRelatorioDto>?> GetReceitas(Request request);
}
