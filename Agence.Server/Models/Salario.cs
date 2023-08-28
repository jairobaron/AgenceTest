namespace Agence.Server.Models;

public class Salario
{
    public string CoUsuario { get; set; } = string.Empty;
    public DateTime DtAlteracao { get; set; } = DateTime.Now;
    public decimal BrutSalario { get; set; } = 0;
    public decimal LiqSalario { get; set; } = 0;
}
