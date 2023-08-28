using Agence.Shared;
using System.Net.Http.Json;
using System.Text.Json;

namespace Agence.Client.Services;

public class RelatorioService : IRelatorioService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _optionsJson;

    public RelatorioService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _optionsJson = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<ResponseAPI<List<ConsultoresDto>>?> GetConsultores()
    {
        var response = await _httpClient.GetAsync("/api/Relatorio/consultores");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        return JsonSerializer.Deserialize<ResponseAPI<List<ConsultoresDto>>>(content, _optionsJson);
    }
    
    public async Task<ResponseAPI<List<ResponseRelatorioDto>>?> GetRelatorios(Request request)
    {
        var response = await _httpClient.PostAsync("/api/Relatorio/receitas", JsonContent.Create(request));
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        return JsonSerializer.Deserialize<ResponseAPI<List<ResponseRelatorioDto>>>(content, _optionsJson);
    }
}
