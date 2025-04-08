using Microsoft.AspNetCore.Mvc;
using TelemedicinaApi.Services;
using TelemedicinaAPI.Models;

namespace TelemedicinaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PacienteController : ControllerBase
{
    private readonly PacienteService _service;

    public PacienteController(PacienteService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get(string? Nome, string? cpf)
        => Ok(_service.Get(Nome, cpf));

    [HttpPost]
    public IActionResult Add(Paciente paciente)
        => Ok(_service.Add(paciente));

    [HttpPut("{id}")]
    public IActionResult Update(int id, Paciente paciente)
        => Ok(_service.Update(id, paciente));

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
        => Ok(_service.Delete(id));
}