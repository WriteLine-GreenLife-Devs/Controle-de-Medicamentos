using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class TelaPaciente : TelaBase<Paciente>
{
    public TelaPaciente(string nomeEntidade, RepositorioBase<Paciente> repositorio) : base(nomeEntidade, repositorio)
    {
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho($"Visualização de {nomeEntidade}");

        var lista = repositorio.SelecionarTodos();
        if (lista.Count == 0)
        {
            ValidarMSG.Aviso("Nenhum registro encontrado", nomeEntidade);
            return;
        }
        foreach(var paciente in lista)
            Console.WriteLine(paciente);

        Console.ReadLine();
    }

    protected override Paciente ObterDadosCadastrais()
    {
        Console.Write($"Digite o nome do {nomeEntidade}: ");
        string nome = Console.ReadLine()?.ToUpper() ?? string.Empty;

        Console.Write($"Digite o CPF do {nomeEntidade}: ");
        string cpf = Console.ReadLine() ?? string.Empty;

        Console.Write($"Digite o número do cartão do SUS do {nomeEntidade}: ");
        string cns = Console.ReadLine() ?? string.Empty;

        Console.Write($"Digite o telefone de contato do {nomeEntidade}: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        return new Paciente {Nome = nome, CPF = cpf, CNS = cns, Telefone = telefone};
    }
    
}