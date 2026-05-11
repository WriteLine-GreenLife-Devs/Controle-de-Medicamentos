using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.Utilidades;

public class TelaPrincipal
{
    private readonly List<TelaBase<EntidadeBase>> telas;

    public TelaPrincipal(List<TelaBase<EntidadeBase>> telas)
    {
        this.telas = telas;
    }

    public void ExibirMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Sistema de Controle de Medicamentos");
            Console.WriteLine("-------------------------------------------");

            for (int i = 0; i < telas.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {telas[i].nomeEntidade}");
            }

            Console.WriteLine("S - Sair");
            Console.WriteLine("-------------------------------------------");
            Console.Write("> ");
            string? opcao = Console.ReadLine()?.ToUpper();

            if (opcao == "S")
                break;

            if (int.TryParse(opcao, out int indice) && indice > 0 && indice <= telas.Count)
            {
                var telaSelecionada = telas[indice - 1];
                telaSelecionada.ExibirMenuModulo();
            }
            else
            {
                Console.WriteLine("Opção inválida!");
            }
        }
    }
}