using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

class RepositorioPaciente : RepositorioBase<Paciente>
{
    public RepositorioPaciente(string nomeArquivo) : base(nomeArquivo)
    {
    }
}