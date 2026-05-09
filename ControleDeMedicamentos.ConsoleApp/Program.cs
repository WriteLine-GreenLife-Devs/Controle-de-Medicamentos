using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

class Program
{
    static void Main(string[] args)
    {
        var telas = new List<ITela>()
        {
            new TelaPaciente("Pacientes", new RepositorioPaciente()),
        
        };

        var telaPrincipal = new TelaPrincipal(telas);
        telaPrincipal.ExibirMenu();
    }
}