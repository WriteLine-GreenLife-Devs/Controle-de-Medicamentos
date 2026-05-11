using ControleDeMedicamentos.ConsoleApp.Compartilhado;

class TelaMedicamento : TelaBase<Medicamento>
{
    #region Construtor
    public TelaMedicamento(string nomeEntidade, RepositorioBase<Medicamento> repositorio) : base(nomeEntidade, repositorio)
    {
    }

    #endregion

    #region Métodos
    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho == true)
        {
            Console.Clear();
            Console.WriteLine($"Visualização de {nomeEntidade}");
            Console.WriteLine("-------------------------------------------");

            var listaMedicamentos = repositorio.SelecionarTodos();

            if (listaMedicamentos.Count > 0)
            {
                int posicao = 1;

                foreach (var medicamento in listaMedicamentos)
                {

                    if (medicamento.GetQuantidadeEstoque() < 20)
                    {
                        Console.WriteLine($"{posicao} - {medicamento.GetNome()} - Em Falta! Estoque: {medicamento.GetQuantidadeEstoque()}");
                        posicao++;
                    }
                    else
                    {
                        Console.WriteLine($"{posicao} - {medicamento.GetNome()} - Estoque: {medicamento.GetQuantidadeEstoque()}");
                        posicao++;
                    }


                }
            }
            else
            {
                Console.WriteLine("Nenhum medicamento cadastrado.");
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey(true);
            Console.Clear();

        }
    }

    public bool ValidarTamanhoString(string texto, int tamanhoMinimo, int tamanhoMaximo)
    {
        if (string.IsNullOrEmpty(texto))
        {
            return false;
        }

        int tamanho = texto.Length;

        if (tamanho >= tamanhoMinimo && tamanho <= tamanhoMaximo)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected override Medicamento ObterDadosCadastrais()
    {
        string Nome = "";
        string Descricao = "";
        int QuantidadeEstoque = 0;
        Fornecedor Fornecedor = new Fornecedor();

        bool verifica = false;

        try
        {
            Console.Write("Digite o nome do Medicamento: ");
            Nome = Console.ReadLine() ?? "";

            verifica = ValidarTamanhoString(Nome, 3, 100);

            if (verifica == false)
            {
                Console.WriteLine("O nome deve conter entre 3 e 100 caracteres. Por favor, tente novamente.");
                Thread.Sleep(3000);
                while (Console.KeyAvailable) Console.ReadKey(true);
                Console.Clear();
                return null;
            }

            Console.Write("Digite a descrição do Medicamento: ");
            Descricao = Console.ReadLine() ?? "";

            verifica = ValidarTamanhoString(Descricao, 5, 255);

            if (verifica == false)
            {
                Console.WriteLine("A descrição deve conter entre 5 e 255 caracteres. Por favor, tente novamente.");
                Thread.Sleep(3000);
                while (Console.KeyAvailable) Console.ReadKey(true);
                Console.Clear();
            }

            Console.Write("Digite a quantidade em estoque do Medicamento: ");
            QuantidadeEstoque = int.Parse(Console.ReadLine() ?? "0");

            var listaFornecedores = new RepositorioFornecedor("fornecedores.json").SelecionarTodos();

            if (listaFornecedores.Count > 0)
            {
                var telaFornecedor = new TelaFornecedor("Fornecedor", new RepositorioFornecedor("fornecedores.json"));
                telaFornecedor.VisualizarTodos(true);

                int numeroLista = 0;

                try
                {
                    Console.Write("Digite o número da lista de Fornecedores para a qual deseja associar o medicamento: ");
                    numeroLista = int.Parse(Console.ReadLine() ?? "0");

                    if (numeroLista > 0 && numeroLista <= listaFornecedores.Count)
                    {
                        Fornecedor = listaFornecedores[numeroLista - 1];
                    }
                    else
                    {
                        Console.WriteLine("Número inválido. Tente Novamente!");
                        return null;
                    }
                }
                catch (System.Exception)
                {
                    Console.WriteLine("Número inválido. Tente Novamente!");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Nenhum fornecedor cadastrado. Por favor, cadastre um fornecedor antes de cadastrar um medicamento.");
                Thread.Sleep(3000);
                while (Console.KeyAvailable) Console.ReadKey(true);
                Console.Clear();
                return null;
            }

        }
        catch (System.Exception)
        {
            Console.WriteLine("Erro, verifique se os parêmetros foram passados corretamente!");
            Thread.Sleep(3000);
            while (Console.KeyAvailable) Console.ReadKey(true);
            Console.Clear();
            return null;
        }

        if (Nome != "" && QuantidadeEstoque >= 0)
        {
            return new Medicamento(Nome, Descricao, QuantidadeEstoque, Fornecedor);
        }
        else
        {
            Console.WriteLine("Erro, verifique se os parêmetros foram passados corretamente!");
            Thread.Sleep(3000);
            while (Console.KeyAvailable) Console.ReadKey(true);
            Console.Clear();
            return null;
        }

    }

    #endregion
}