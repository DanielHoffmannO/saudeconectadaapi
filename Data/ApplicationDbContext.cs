using Microsoft.EntityFrameworkCore;
using TelemedicinaAPI.Models;

namespace TelemedicinaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
    }
}
