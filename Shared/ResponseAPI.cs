namespace Agence.Shared;

public class ResponseAPI<T>
{
    public bool Success { get; set; }
    public T? Value { get; set; }
    public string? Message { get; set; }
}
