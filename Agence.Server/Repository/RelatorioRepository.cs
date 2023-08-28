using Agence.Server.Models;
using Agence.Shared;
using Microsoft.EntityFrameworkCore;

namespace Agence.Server.Repository;

public class RelatorioRepository : IRelatorioRepository
{
    private readonly CaolDbContext _dbContext;
    public RelatorioRepository(CaolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ConsultoresDto>?> GetConsultores()
    {
        return await (from usuario in _dbContext.Usuarios
                      join permissao in _dbContext.PermissaoSistemas on usuario.CoUsuario equals permissao.CoUsuario
                      where permissao.CoSistema == 1 && permissao.InAtivo == "S" && 
                            (permissao.CoTipoUsuario == 1 || permissao.CoTipoUsuario == 2 || permissao.CoTipoUsuario == 3)
                      select new ConsultoresDto()
                      {
                          CoUsuario = usuario.CoUsuario,
                          NoUsuario = usuario.NoUsuario
                      }).ToListAsync();
    }

    public async Task<string?> GetNomeConsultor(string coUsuario)
    {
        return await (from usuario in _dbContext.Usuarios                      
                      where usuario.CoUsuario == coUsuario
                      select usuario.NoUsuario)
                      .FirstOrDefaultAsync();
    }

    public async Task<List<Fatura>?> GetFaturas(string consultor, DateTime start, DateTime end)
    {
        return await (from fatura in _dbContext.Faturas
                      join sistema in _dbContext.Sistemas on fatura.CoSistema equals sistema.CoSistema
                      join usuario in _dbContext.Usuarios on sistema.CoUsuario equals usuario.CoUsuario
                      where usuario.CoUsuario == consultor && fatura.DataEmissao.Date >= start.Date &&
                            fatura.DataEmissao.Date <= end.Date
                      select fatura).ToListAsync();
    }

    public async Task<decimal> GetCustoFixoConsultor(string coUsuario)
    {
        return await (from usuario in _dbContext.Salarios
                      where usuario.CoUsuario == coUsuario
                      select usuario.BrutSalario)
                      .FirstOrDefaultAsync();
    }
}
