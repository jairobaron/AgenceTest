using Agence.Server.Services;
using Agence.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Agence.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        private readonly IRelatorioService _relatorioService;

        public RelatorioController(IRelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }

        [HttpGet]
        [Route("consultores")]
        public async Task<IActionResult> Consultores()
        {
            var responseAPi = new ResponseAPI<List<ConsultoresDto>>();            
            try
            {
                var consultoresList = await _relatorioService.GetConsultores() ?? new List<ConsultoresDto>();
                responseAPi.Success = true;
                responseAPi.Value = consultoresList;
            }
            catch (Exception ex)
            {
                responseAPi.Success = false;
                responseAPi.Message = ex.Message;
            }
            return Ok(responseAPi);
        }

        [HttpGet]
        [Route("faturas")]
        public async Task<IActionResult> GetFaturas([FromQuery] string consultor, DateTime start, DateTime end)
        {
            var responseAPi = new ResponseAPI<object>();
            try
            {
                var list = await _relatorioService.GetFaturas(consultor, start, end);
                responseAPi.Success = true;
                responseAPi.Value = list;
            }
            catch (Exception ex)
            {
                responseAPi.Success = false;
                responseAPi.Message = ex.Message;
            }
            return Ok(responseAPi);
        }

        [HttpPost]
        [Route("receitas")]
        public async Task<IActionResult> GetReceitas([FromBody] Request request)
        {
            var responseAPi = new ResponseAPI<object>();
            try
            {
                var list = await _relatorioService.GetReceitas(request);
                if (list == null)
                    throw new Exception("Nenhum relatório encontrado para esta consulta.");

                responseAPi.Success = true;
                responseAPi.Value = list;
            }
            catch (Exception ex)
            {
                responseAPi.Success = false;
                responseAPi.Message = ex.Message;
            }
            return Ok(responseAPi);
        }
    }
}
