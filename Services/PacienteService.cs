using TelemedicinaAPI.Data;
using TelemedicinaAPI.Models;

namespace TelemedicinaApi.Services;

public class PacienteService
{
    private readonly ApplicationDbContext _context;

    public PacienteService(ApplicationDbContext medicos)
    {
        _context = medicos;
    }

    public IEnumerable<Paciente> Get(string? nome, string? cpf)
    {
        return _context.Pacientes
            .Where(m => (string.IsNullOrEmpty(nome) || m.Nome.Contains(nome)) &&
                        (string.IsNullOrEmpty(cpf) || m.CPF.Contains(cpf)))
            .ToList();
    }

    public Paciente Add(Paciente paciente)
    {
        _context.Pacientes.Add(paciente);
        return paciente;
    }

    public Paciente? Update(int id, Paciente paciente)
    {
        var existingPaciente = _context.Pacientes.FirstOrDefault(m => m.Id == id);
        if (existingPaciente == null) return null;

        existingPaciente.Nome = paciente.Nome;
        existingPaciente.CPF = paciente.CPF;
        existingPaciente.CEP = paciente.CEP;

        return existingPaciente;
    }

    public bool Delete(int id)
    {
        var Paciente = _context.Pacientes.FirstOrDefault(m => m.Id == id);
        if (Paciente == null) return false;

        _context.Pacientes.Remove(Paciente);
        return true;
    }
}
