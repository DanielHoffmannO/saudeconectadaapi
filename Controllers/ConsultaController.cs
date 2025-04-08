using Microsoft.AspNetCore.Mvc;
using TelemedicinaApi.Enum;
using TelemedicinaApi.Services;
using TelemedicinaAPI.Models;

namespace TelemedicinaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConsultaController : ControllerBase
{
    private readonly ConsultaService _consultaService;

    public ConsultaController(ConsultaService consultaService)
    {
        _consultaService = consultaService;
    }

    [HttpGet("validar-horarios/{pacienteId}/{especialidade}")]
    public async Task<IActionResult> ValidarHorarios(int pacienteId, Especialidade especialidade)
    {
        try
        {
            var horarios = await _consultaService.ValidarHorarios(pacienteId, especialidade);
            return Ok(horarios);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpPost("agendar-consulta")]
    public async Task<IActionResult> AgendarConsulta([FromBody] Consulta agendarConsultaDto)
    {
        try
        {
            var resultado = await _consultaService.AgendarConsulta(
                agendarConsultaDto.PacienteId,
                agendarConsultaDto.MedicoId,
                agendarConsultaDto.DataConsultaInicio
            );
            return Ok(new { mensagem = "Consulta agendada com sucesso." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
}