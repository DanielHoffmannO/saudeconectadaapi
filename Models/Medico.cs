using System.ComponentModel.DataAnnotations;
using TelemedicinaApi.Enum;

namespace TelemedicinaAPI.Models;

public class Medico
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public Especialidade Especialidade { get; set; }
    public string CRM { get; set; } = string.Empty;
    public string CEP { get; set; } = string.Empty;
}
