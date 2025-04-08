using TelemedicinaApi.DTOs;
using TelemedicinaApi.Enum;
using TelemedicinaApi.ExternalServices;
using TelemedicinaAPI.Data;
using TelemedicinaAPI.Models;

namespace TelemedicinaApi.Services;

public class ConsultaService
{
    private readonly ApplicationDbContext _context;
    private readonly DistanceService _distanceService;

    public ConsultaService(ApplicationDbContext context, DistanceService distanceService)
    {
        _context = context;
        _distanceService = distanceService;
    }

    public async Task<List<HorariosDtos>> ValidarHorarios(int pacienteId, Especialidade especialidade)
    {
        var paciente = _context.Pacientes.FirstOrDefault(p => p.Id == pacienteId)
            ?? throw new Exception("Paciente não cadastrado");

        var medicosProximos = new List<Medico>();

        foreach (var medico in _context.Medicos.Where(m => m.Especialidade == especialidade))
        {
            var distancia = await _distanceService.ObterDistanciaAsync(medico.CEP, paciente.CEP);
            if (distancia.HasValue && distancia.Value <= 100)
            {
                medicosProximos.Add(medico);
            }
        }

        var horariosDisponiveis = new List<HorariosDtos>();

        foreach (var medico in medicosProximos)
        {
            var consultasAgendadas = _context.Consultas
                .Where(c => c.MedicoId == medico.Id)
                .Select(c => new { c.DataConsultaInicio, c.DataConsultaFim })
                .ToList();

            DateTime inicioJornada = DateTime.Today.AddHours(8);
            DateTime fimJornada = DateTime.Today.AddHours(18);

            DateTime horarioAtual = inicioJornada;

            while (horarioAtual < fimJornada)
            {
                DateTime horarioFim = horarioAtual.AddMinutes(30);

                bool estaOcupado = consultasAgendadas.Any(c =>
                    (horarioAtual >= c.DataConsultaInicio && horarioAtual < c.DataConsultaFim) ||
                    (horarioFim > c.DataConsultaInicio && horarioFim <= c.DataConsultaFim));

                if (!estaOcupado)
                {
                    horariosDisponiveis.Add(new HorariosDtos
                    {
                        MedicoId = medico.Id,
                        DataConsultaInicio = horarioAtual,
                        DataConsultaFim = horarioFim
                    });
                }

                horarioAtual = horarioFim;
            }
        }

        return horariosDisponiveis;
    }

    public async Task<bool> AgendarConsulta(int pacienteId, int medicoId, DateTime dataConsultaInicio)
    {
        var paciente = _context.Pacientes.FirstOrDefault(p => p.Id == pacienteId)
            ?? throw new Exception("Paciente não cadastrado");

        var medico = _context.Medicos.FirstOrDefault(m => m.Id == medicoId)
            ?? throw new Exception("Médico não encontrado");

        bool horarioOcupado = _context.Consultas.Any(c =>
            c.MedicoId == medicoId &&
            ((dataConsultaInicio >= c.DataConsultaInicio && dataConsultaInicio < c.DataConsultaFim) ||
            (dataConsultaInicio.AddMinutes(30) > c.DataConsultaInicio && dataConsultaInicio.AddMinutes(30) <= c.DataConsultaFim)));

        if (horarioOcupado)
            throw new Exception("O horário selecionado já está ocupado.");

        var consulta = new Consulta()
        {
            PacienteId = pacienteId,
            MedicoId = medicoId,
            Especialidade = medico.Especialidade,
            DataConsultaCadastro = DateTime.Now,
            DataConsultaInicio = dataConsultaInicio,
            DataConsultaFim = dataConsultaInicio.AddMinutes(30)
        };

        _context.Consultas.Add(consulta);
        await _context.SaveChangesAsync();

        return true;
    }
}