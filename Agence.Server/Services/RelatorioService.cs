using Agence.Server.Models;
using Agence.Server.Repository;
using Agence.Shared;
using System.Runtime.CompilerServices;

namespace Agence.Server.Services;

public class RelatorioService : IRelatorioService
{
    private readonly IRelatorioRepository _repository;
    public RelatorioService(IRelatorioRepository repository) 
    {
        _repository = repository;
    }

    public async Task<List<ConsultoresDto>?> GetConsultores()
    {
        return await _repository.GetConsultores();
    }
    
    public async Task<List<ResponseRelatorioDto>?> GetReceitas(Request request)
    {
        if (request == null || request.Consultants == null)
            return null;

        var result = new List<ResponseRelatorioDto>();
        foreach (var consultor in request.Consultants)
        {
            var nome = await _repository.GetNomeConsultor(consultor);            
            var faturas = await _repository.GetFaturas(consultor, request.Initial, request.Final);
            var custoFixoConsultor = await _repository.GetCustoFixoConsultor(consultor);
            if (faturas == null)
                continue;

            var periodos = (from fatura in faturas
                            group fatura by new
                            {
                                fatura.DataEmissao.Month,
                                fatura.DataEmissao.Year
                            } into f
                            select new
                            {
                                data = string.Format("{0}-{1}-01", f.Key.Year, f.Key.Month),
                                receitaLiquida = f.Sum(x => x.Valor - (x.Valor * x.TotalImpInc / 100)),
                                custoFixo = custoFixoConsultor,
                                comissao = f.Sum(x => (x.Valor - (x.Valor * x.TotalImpInc / 100)) * x.ComissaoCn / 100)
                            });
            var relatorios = new List<RelatorioDto>();
            foreach (var periodo in periodos)
            {
                relatorios.Add(new RelatorioDto()
                {
                    Periodo = DateTime.Parse(periodo.data),
                    ReceitaLiquida = periodo.receitaLiquida,
                    CustoFixo = periodo.custoFixo,
                    Comissao = periodo.comissao,
                    Lucro = periodo.receitaLiquida - (periodo.custoFixo + periodo.comissao)
                });
            }
            result.Add(new ResponseRelatorioDto() { 
                NoUsuario = nome ?? string.Empty,
                RelatorioList = relatorios
            });
        }
        return result;
    }

    public async Task<List<Fatura>?> GetFaturas(string consultor, DateTime start, DateTime end)
    {
        return await _repository.GetFaturas(consultor, start, end);
    }
}
