namespace Agence.Shared;

public class Request
{
    public DateTime Initial { get; set; }
    public DateTime Final { get; set; }
    public List<string>? Consultants { get; set; }

}