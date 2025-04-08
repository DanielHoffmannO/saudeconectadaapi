using TelemedicinaAPI.Models;
using TelemedicinaApi.Enum;

namespace TelemedicinaAPI.Data;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext dbContext)
    {
        dbContext.Database.EnsureCreated();

        if (!dbContext.Medicos.Any())
        {
            dbContext.Medicos.Add(new Medico
            {
                Nome = "Dr. João",
                Especialidade = Especialidade.Cardiologia,
                CRM = "123456",
                CEP = "12345-678"
            });
            dbContext.Medicos.Add(new Medico
            {
                Nome = "Dr. Ana",
                Especialidade = Especialidade.Dermatologia,
                CRM = "654321",
                CEP = "87654-321"
            });

            dbContext.SaveChanges();
        }

        if (!dbContext.Pacientes.Any())
        {
            dbContext.Pacientes.Add(new Paciente
            {
                Nome = "Maria Silva",
                CPF = "123.456.789-00",
                CEP = "12345-678"
            });
            dbContext.Pacientes.Add(new Paciente
            {
                Nome = "Carlos Souza",
                CPF = "987.654.321-00",
                CEP = "87654-321"
            });

            dbContext.SaveChanges();
        }

        if (!dbContext.Consultas.Any())
        {
            dbContext.Consultas.Add(new Consulta
            {
                MedicoId = 1,
                PacienteId = 1,
                Especialidade = Especialidade.Cardiologia,
                DataConsultaCadastro = DateTime.Now,
                DataConsultaInicio = DateTime.Now.AddHours(2),
                DataConsultaFim = DateTime.Now.AddHours(3)
            });
            dbContext.Consultas.Add(new Consulta
            {
                MedicoId = 2,
                PacienteId = 2,
                Especialidade = Especialidade.Dermatologia,
                DataConsultaCadastro = DateTime.Now,
                DataConsultaInicio = DateTime.Now.AddHours(4),
                DataConsultaFim = DateTime.Now.AddHours(5)
            });

            dbContext.SaveChanges();
        }
    }
}
