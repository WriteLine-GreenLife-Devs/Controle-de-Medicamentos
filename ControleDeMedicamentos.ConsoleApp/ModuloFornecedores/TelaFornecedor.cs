using ControleDeMedicamentos.ConsoleApp.Compartilhado;

class TelaFornecedor : TelaBase<Fornecedor>
{

    #region Construtor
    public TelaFornecedor(string nomeEntidade, RepositorioBase<Fornecedor> repositorio) : base(nomeEntidade, repositorio)
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

            var listaFornecedores = repositorio.SelecionarTodos();

            if (listaFornecedores.Count > 0)
            {
                int posicao = 1;

                foreach (var fornecedor in listaFornecedores)
                {
                    Console.WriteLine($"{posicao} - {fornecedor.GetNome()}");
                    posicao++;
                }
            }
            else
            {
                Console.WriteLine("Nenhum fornecedor cadastrado.");
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey(true);
            Console.Clear();

        }
    }

    public bool VerificarCNPJExistente(string cnpj)
    {
        bool verifica = false;

        var listaFornecedores = repositorio.SelecionarTodos();

        foreach (var fornecedor in listaFornecedores)
        {
            if (fornecedor.GetCNPJ() == cnpj)
            {
                verifica = true;
            }
        }
        return verifica;
    }

    public string VerificarTelefone(string telefone)
    {
        string apenasNumeros = System.Text.RegularExpressions.Regex.Replace(telefone ?? "", @"[^\d]", "");

        int tamanho = apenasNumeros.Length;

        if (tamanho == 10)
        {
            return long.Parse(apenasNumeros).ToString(@"(00) 0000-0000");
        }
        else if (tamanho == 11)
        {
            return long.Parse(apenasNumeros).ToString(@"(00) 0 0000-0000");
        }
        else
        {
            Console.WriteLine("Número de Telefone inválido (formato validado: 10-11 dígitos), tente novamente!");
            return "";
        }
    }

    public string VerificarCNPJ(string cnpj)
    {
        string apenasNumeros = System.Text.RegularExpressions.Regex.Replace(cnpj ?? "", @"[^\d]", "");
        int tamanho = apenasNumeros.Length;

        if (tamanho == 14)
        {
            return Convert.ToUInt64(apenasNumeros).ToString(@"00\.000\.000\/0000\-00");
        }
        else
        {
            Console.WriteLine("CNPJ inválido (deve conter 14 dígitos), tente novamente!");
            return "";
        }
    }

    public bool ValidarTamanhoNome(string texto, int tamanhoMinimo, int tamanhoMaximo)
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

    protected override Fornecedor ObterDadosCadastrais()
    {
        string Nome = "";
        string Telefone = "";
        string CNPJ = "";

        bool verificaNome = false;

        try
        {
            Console.Write("Digite o nome do Fornecedor: ");
            Nome = Console.ReadLine() ?? "";

            verificaNome = ValidarTamanhoNome(Nome, 3, 100);

            if (verificaNome == false)
            {
                Console.WriteLine("Tamanho do Nome do Fornecedor deve conter entre 3 à 100 caracteres! Tente Novamente.");
                return null;
            }

            Console.Write($"Digite o Telefone do Fornecedor {Nome}: ");
            Telefone = Console.ReadLine() ?? "";

            Telefone = VerificarTelefone(Telefone);

            Console.Write($"Digite o CNPJ do Fornecedor {Nome}: ");
            CNPJ = Console.ReadLine() ?? "";

            CNPJ = VerificarCNPJ(CNPJ);

        }
        catch (System.Exception)
        {
            Console.WriteLine("Erro, verifique se os parêmetros foram passados corretamente!");
            Thread.Sleep(3000);
            while (Console.KeyAvailable) Console.ReadKey(true);
            Console.Clear();
            return null;
        }

        if (Nome != "" && CNPJ != "" && Telefone != "")
        {
            return new Fornecedor{Nome = Nome, Telefone = Telefone, CNPJ = CNPJ};
        }
        else
        {
            Console.WriteLine("Nome dos campos Vazia, por favor tente novamente!");
            Thread.Sleep(3000);
            while (Console.KeyAvailable) Console.ReadKey(true);
            Console.Clear();
            return null;
        }
    }

    #endregion
}