using TelemedicinaAPI.Data;
using TelemedicinaAPI.Models;

namespace TelemedicinaApi.Services;

public class MedicoService
{
    private readonly ApplicationDbContext _context;

    public MedicoService(ApplicationDbContext medicos)
    {
        _context = medicos;
    }

    public IEnumerable<Medico> Get(string? nome, string? crm)
    {
        return _context.Medicos
            .Where(m => (string.IsNullOrEmpty(nome) || m.Nome.Contains(nome)) &&
                        (string.IsNullOrEmpty(crm) || m.CRM.Contains(crm)))
            .ToList();
    }


    public Medico Add(Medico medico)
    {
        _context.Medicos.Add(medico);
        return medico;
    }

    public Medico? Update(int id, Medico medico)
    {
        var existingMedico = _context.Medicos.FirstOrDefault(m => m.Id == id);
        if (existingMedico == null) return null;

        existingMedico.Nome = medico.Nome;
        existingMedico.CRM = medico.CRM;
        existingMedico.CEP = medico.CEP;

        return existingMedico;
    }

    public bool Delete(int id)
    {
        var Medico = _context.Medicos.FirstOrDefault(m => m.Id == id);
        if (Medico == null) return false;

        _context.Medicos.Remove(Medico);
        return true;
    }
}
