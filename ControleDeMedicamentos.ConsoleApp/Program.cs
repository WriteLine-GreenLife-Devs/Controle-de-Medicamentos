using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

class Program
{
    static void Main(string[] args)
    {

        var telas = new List<ITela>()
        {
            new TelaFornecedor("Fornecedores", new RepositorioFornecedor("fornecedores.json")),
            new TelaMedicamento("Medicamentos", new RepositorioMedicamento("medicamentos.json")),
            new TelaFuncionario("Funcionários", new RepositorioFuncionario("funcionarios.json"))
        };

        var telaPrincipal = new TelaPrincipal(telas);
        telaPrincipal.ExibirMenu();
    }
}