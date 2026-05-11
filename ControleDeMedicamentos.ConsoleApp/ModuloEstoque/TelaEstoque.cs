using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

public class TelaEstoque : TelaBase<Estoque>
{
    public TelaEstoque(string nomeEntidade, RepositorioBase<Estoque> repositorio) : base(nomeEntidade, repositorio)
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
        foreach (var estoque in lista)
            Console.WriteLine(estoque);

        Console.ReadLine();
    }

    protected override Estoque ObterDadosCadastrais()
    {
        Medicamento Medicamento = new Medicamento();

        var listaMedicamentos = new RepositorioMedicamento("Medicamentoes.json").SelecionarTodos();

        if (listaMedicamentos.Count > 0)
        {
            var telaMedicamento = new TelaMedicamento("Medicamento", new RepositorioMedicamento("Medicamentos.json"));
            telaMedicamento.VisualizarTodos(true);

            int numeroLista = 0;

            try
            {
                Console.Write("Digite o número da lista de Medicamentos para a qual deseja associar o estoque: ");
                numeroLista = int.Parse(Console.ReadLine() ?? "0");

                if (numeroLista > 0 && numeroLista <= listaMedicamentos.Count)
                {
                    Medicamento = listaMedicamentos[numeroLista - 1];
                }
                else
                {
                    Console.WriteLine("Número inválido. Tente Novamente!");
                    Thread.Sleep(3000);
                    while (Console.KeyAvailable) Console.ReadKey(true);
                    Console.Clear();
                    return null;
                }
            }
            catch (System.Exception)
            {
                Console.WriteLine("Número inválido. Tente Novamente!");
                Thread.Sleep(3000);
                while (Console.KeyAvailable) Console.ReadKey(true);
                Console.Clear();
                return null;
            }
        }
        else
        {
            Console.WriteLine("Nenhum Medicamento cadastrado. Por favor, cadastre um Medicamento antes de cadastrar um estoque.");
            Thread.Sleep(3000);
            while (Console.KeyAvailable) Console.ReadKey(true);
            Console.Clear();
            return null;
        }

        Console.Write("Digite quantos medicamentos são requisitados: ");

        Console.Write("");

        return new Estoque();
    }

    protected override bool PodeExcluir => false;
    protected override bool PodeEditar => false;
}