using ControleDeMedicamentos.ConsoleApp.Compartilhado;

class TelaFuncionario : TelaBase<Funcionario>
{
    #region Construtor
    public TelaFuncionario(string nomeTela, RepositorioFuncionario repositorio) : base(nomeTela, repositorio)
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

            var listaFuncionarios = repositorio.SelecionarTodos();

            if (listaFuncionarios.Count > 0)
            {
                int posicao = 1;

                foreach (var funcionario in listaFuncionarios)
                {
                    Console.WriteLine($"{posicao} - {funcionario.GetNome()}");
                    posicao++;
                }
            }
            else
            {
                Console.WriteLine("Nenhum funcionário cadastrado.");
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey(true);
        }
    }

    public bool VerificarCPFExistente(string cpf)
    {
        bool verifica = false;

        var listaFuncionarios = repositorio.SelecionarTodos();

        foreach (var funcionario in listaFuncionarios)
        {
            if (funcionario.GetCPF() == cpf)
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

    public string VerificarCPF(string cpf)
    {
        string apenasNumeros = System.Text.RegularExpressions.Regex.Replace(cpf ?? "", @"[^\d]", "");
        int tamanho = apenasNumeros.Length;

        if (tamanho == 11)
        {
            return Convert.ToUInt64(apenasNumeros).ToString(@"000\.000\.000\-00");
        }
        else
        {
            Console.WriteLine("CPF inválido (deve conter 11 dígitos), tente novamente!");
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

    protected override Funcionario ObterDadosCadastrais()
    {
        string Nome = "";
        string Telefone = "";
        string CPF = "";

        bool verifica = false;

        try
        {
            Console.Write("Digite o nome do Funcionário: ");
            Nome = Console.ReadLine() ?? "";

            verifica = ValidarTamanhoNome(Nome, 3, 100);

            if (verifica == false)
            {
                Console.WriteLine("Tamanho do Nome do Funcionário deve conter entre 3 à 100 caracteres! Tente Novamente.");
                Thread.Sleep(3000);
                while (Console.KeyAvailable) Console.ReadKey(true);
                Console.Clear();
                return null;
            }

            Console.Write($"Digite o Telefone do Funcionário {Nome}: ");
            Telefone = Console.ReadLine() ?? "";

            Telefone = VerificarTelefone(Telefone);

            if (Telefone == "")
            {
                Thread.Sleep(3000);
                while (Console.KeyAvailable) Console.ReadKey(true);
                Console.Clear();
                return null;
            }

            Console.Write($"Digite o CPF do Funcionário {Nome}: ");
            CPF = Console.ReadLine() ?? "";

            CPF = VerificarCPF(CPF);

            verifica = VerificarCPFExistente(CPF);

            if (verifica == true)
            {
                Console.WriteLine("CPF já existente, por favor tente novamente!");
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

        if (Nome != "" && CPF != "" && Telefone != "")
        {
            return new Funcionario { Nome = Nome, Telefone = Telefone, CPF = CPF };
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