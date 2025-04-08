using TelemedicinaApi.Enum;

namespace TelemedicinaAPI.Models;

public class Consulta
{
    public int Id { get; set; }
    public int MedicoId { get; set; }
    public int PacienteId { get; set; }
    public Especialidade Especialidade { get; set; }
    public DateTime DataConsultaCadastro { get; set; }
    public DateTime DataConsultaInicio { get; set; }
    public DateTime DataConsultaFim { get; set; }
}