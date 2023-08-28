namespace Agence.Shared;

public class RelatorioDto
{
    public DateTime? Periodo { get; set; }
    public decimal ReceitaLiquida { get; set; }
    public decimal CustoFixo { get; set; }
    public decimal Comissao { get; set; }
    public decimal Lucro { get; set; }
}