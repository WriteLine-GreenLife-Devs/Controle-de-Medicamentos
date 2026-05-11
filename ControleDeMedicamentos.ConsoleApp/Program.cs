using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Utilidades;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

class Program
{
    static void Main(string[] args)
    {

        var telas = new List<ITela>()
        {
            new TelaFornecedor("Fornecedores", new RepositorioFornecedor("fornecedores")),
            new TelaMedicamento("Medicamentos", new RepositorioMedicamento("medicamentos")),
            new TelaFuncionario("Funcionários", new RepositorioFuncionario("funcionarios")),
            new TelaPaciente("Pacientes", new RepositorioPaciente("pacientes")),
            new TelaEstoque("Estoque", new RepositorioEstoque("estoque"))
        };

        var telaPrincipal = new TelaPrincipal(telas);
        telaPrincipal.ExibirMenu();
    }
}