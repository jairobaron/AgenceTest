namespace Agence.Server.Models;

public class Fatura
{
    public int CoFatura { get; set; }
    public int CoCliente { get; set; }
    public int CoSistema { get; set; }    
    public int CoOs { get; set; }
    public int NumNf { get; set; }
    public decimal Total { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataEmissao { get; set; } = DateTime.Now;
    public string CorpoNf { get; set; } = string.Empty;
    public decimal ComissaoCn { get; set; }
    public decimal TotalImpInc { get; set; }
}
