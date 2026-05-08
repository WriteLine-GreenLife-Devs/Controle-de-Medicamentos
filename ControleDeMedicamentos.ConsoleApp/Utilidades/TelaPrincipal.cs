using ControleDeMedicamentos.ConsoleApp.Compartilhado;

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

                string? subOpcao = telaSelecionada.ObterOpcaoMenu();

                switch (subOpcao)
                {
                    case "1": telaSelecionada.Cadastrar(); break;
                    case "2": telaSelecionada.Editar(); break;
                    case "3": telaSelecionada.Excluir(); break;
                    case "4": telaSelecionada.VisualizarTodos(true); break;
                }
            }
        }
    }
}