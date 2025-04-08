using Microsoft.AspNetCore.Mvc;
using TelemedicinaApi.Services;
using TelemedicinaAPI.Models;

namespace TelemedicinaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicoController : ControllerBase
{
    private readonly MedicoService _service;

    public MedicoController(MedicoService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get(string? nome, string? crm)
        => Ok(_service.Get(nome, crm));

    [HttpPost]
    public IActionResult Add(Medico medico)
        => Ok(_service.Add(medico));

    [HttpPut("{id}")]
    public IActionResult Update(int id, Medico medico)
        => Ok(_service.Update(id, medico));

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
        => Ok(_service.Delete(id));
}
