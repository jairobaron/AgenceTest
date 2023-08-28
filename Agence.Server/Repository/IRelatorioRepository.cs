using Agence.Server.Models;
using Agence.Shared;

namespace Agence.Server.Repository;

public interface IRelatorioRepository
{
    Task<List<ConsultoresDto>?> GetConsultores();
    Task<string?> GetNomeConsultor(string coUsuario);
    Task<List<Fatura>?> GetFaturas(string consultor, DateTime start, DateTime end);
    Task<decimal> GetCustoFixoConsultor(string coUsuario);
}
