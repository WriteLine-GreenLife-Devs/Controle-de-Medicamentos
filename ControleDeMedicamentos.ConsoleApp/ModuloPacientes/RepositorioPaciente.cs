using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class RepositorioPaciente : RepositorioBase<Paciente>
{
    public RepositorioPaciente() : base("Pacientes") { }
}