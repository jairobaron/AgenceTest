using Agence.Shared;
using Agence.Client.Models;

namespace Agence.Client.Services;

public interface IRelatorioService
{
    Task<ResponseAPI<List<ConsultoresDto>>?> GetConsultores();
    Task<ResponseAPI<List<ResponseRelatorioDto>>?> GetRelatorios(Request request);
}
