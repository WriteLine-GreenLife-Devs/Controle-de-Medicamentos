using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Utilidades;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

class Program
{
    static void Main(string[] args)
    {

        var telas = new List<ITela>()
        {
            new TelaFornecedor("Fornecedores", new RepositorioFornecedor("fornecedores")),
            new TelaMedicamento("Medicamentos", new RepositorioMedicamento("medicamentos")),
            new TelaFuncionario("Funcionários", new RepositorioFuncionario("funcionarios")),
            new TelaPaciente("Pacientes", new RepositorioPaciente())
        };

        var telaPrincipal = new TelaPrincipal(telas);
        telaPrincipal.ExibirMenu();
    }
}