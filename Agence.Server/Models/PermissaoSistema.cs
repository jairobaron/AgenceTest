namespace Agence.Server.Models;

public class PermissaoSistema
{
    public string CoUsuario { get; set; } = string.Empty;
    public long CoTipoUsuario { get; set; }
    public long CoSistema { get; set; }
    public string InAtivo { get; set; } = "S";
    public string? CoUsuarioAtualizacao { get; set; }
    public DateTime DtAtualizacao { get; set; } = DateTime.Now;
}
