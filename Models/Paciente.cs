using System.ComponentModel.DataAnnotations;

namespace TelemedicinaAPI.Models;

public class Paciente
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string CEP { get; set; } = string.Empty;
}