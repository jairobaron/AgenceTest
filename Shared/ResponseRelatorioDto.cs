namespace Agence.Shared;

public class ResponseRelatorioDto
{
    public string NoUsuario { get; set; } = string.Empty;
    public List<RelatorioDto>? RelatorioList { get; set; }
}