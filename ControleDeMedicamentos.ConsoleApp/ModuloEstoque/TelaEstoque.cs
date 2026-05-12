using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Operacoes;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

public class TelaEstoque : TelaBase<Estoque>
{
    public TelaEstoque(string nomeEntidade, RepositorioBase<Estoque> repositorio) : base(nomeEntidade, repositorio)
    {
    }
    private DateTime ObterData()
    {
        while (true)
        {
            Console.Write("Digite a data (dd/MM/yyyy): ");
            string dataStr = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrEmpty(dataStr))
            {
                ValidarMSG.Aviso("Data obrigatória. Tente novamente.");
                continue;
            }

            var formatos = new[] { "dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "d-M-yyyy" };
            if (DateTime.TryParseExact(dataStr, formatos, System.Globalization.CultureInfo.CreateSpecificCulture("pt-BR"), System.Globalization.DateTimeStyles.None, out DateTime dataParsed))
                return dataParsed;

            if (DateTime.TryParse(dataStr, System.Globalization.CultureInfo.CreateSpecificCulture("pt-BR"), System.Globalization.DateTimeStyles.None, out dataParsed))
                return dataParsed;

            ValidarMSG.Aviso("Data inválida. Digite no formato dd/MM/yyyy.");
        }
    }

    private Medicamento? SelecionarMedicamento(IEnumerable<string>? idsExcluidos = null, bool somenteDisponiveis = false)
    {
        var repositorioMedicamentos = new RepositorioMedicamento("medicamentos");
        var listaMedicamentos = repositorioMedicamentos.SelecionarTodos();

        if (idsExcluidos != null)
            listaMedicamentos = listaMedicamentos.Where(m => !idsExcluidos.Contains(m.Id)).ToList();

        if (somenteDisponiveis)
            listaMedicamentos = listaMedicamentos.Where(m => m.GetQuantidadeEstoque() > 0).ToList();

        if (listaMedicamentos.Count == 0)
        {
            ValidarMSG.Aviso("Nenhum medicamento disponível para seleção. Verifique se todos já foram escolhidos ou se o estoque está zerado.");
            return null;
        }

        Console.Clear();
        Console.WriteLine("Medicamentos Disponíveis:");
        Console.WriteLine("-------------------------------------------");

        int posicao = 1;
        foreach (var medicamento in listaMedicamentos)
        {
            Console.WriteLine($"{posicao} - {medicamento}");
            posicao++;
        }

        try
        {
            Console.Write("Digite o número do medicamento: ");
            int numeroLista = int.Parse(Console.ReadLine() ?? "0");

            if (numeroLista > 0 && numeroLista <= listaMedicamentos.Count)
            {
                return listaMedicamentos[numeroLista - 1];
            }
            else
            {
                ValidarMSG.Aviso("Número inválido. Escolha um medicamento listado.");
                return null;
            }
        }
        catch
        {
            ValidarMSG.Aviso("Entrada inválida. Informe um número válido.");
            return null;
        }
    }

    private Funcionario? SelecionarFuncionario()
    {
        var repositorioFuncionarios = new RepositorioFuncionario("funcionarios");
        var listaFuncionarios = repositorioFuncionarios.SelecionarTodos();

        if (listaFuncionarios.Count == 0)
        {
            ValidarMSG.Aviso("Nenhum funcionário cadastrado");
            return null;
        }

        Console.Clear();
        Console.WriteLine("Funcionários Disponíveis:");
        Console.WriteLine("-------------------------------------------");

        int posicao = 1;
        foreach (var funcionario in listaFuncionarios)
        {
            Console.WriteLine($"{posicao} - {funcionario}");
            posicao++;
        }

        try
        {
            Console.Write("Digite o número do funcionário: ");
            int numeroLista = int.Parse(Console.ReadLine() ?? "0");

            if (numeroLista > 0 && numeroLista <= listaFuncionarios.Count)
            {
                return listaFuncionarios[numeroLista - 1];
            }
            else
            {
                ValidarMSG.Aviso("Número inválido");
                return null;
            }
        }
        catch
        {
            ValidarMSG.Aviso("Entrada inválida");
            return null;
        }
    }

    private Paciente? SelecionarPaciente()
    {
        var repositorioPacientes = new RepositorioPaciente("pacientes");
        var listaPacientes = repositorioPacientes.SelecionarTodos();

        if (listaPacientes.Count == 0)
        {
            ValidarMSG.Aviso("Nenhum paciente cadastrado");
            return null;
        }

        Console.Clear();
        Console.WriteLine("Pacientes Disponíveis:");
        Console.WriteLine("-------------------------------------------");

        int posicao = 1;
        foreach (var paciente in listaPacientes)
        {
            Console.WriteLine($"{posicao} - {paciente}");
            posicao++;
        }

        try
        {
            Console.Write("Digite o número do paciente: ");
            int numeroLista = int.Parse(Console.ReadLine() ?? "0");

            if (numeroLista > 0 && numeroLista <= listaPacientes.Count)
            {
                return listaPacientes[numeroLista - 1];
            }
            else
            {
                ValidarMSG.Aviso("Número inválido");
                return null;
            }
        }
        catch
        {
            ValidarMSG.Aviso("Entrada inválida");
            return null;
        }
    }

    private int ObterQuantidade()
    {
        try
        {
            Console.Write("Digite a quantidade: ");
            int quantidade = int.Parse(Console.ReadLine() ?? "0");

            if (quantidade > 0)
            {
                return quantidade;
            }
            else
            {
                ValidarMSG.Aviso("A quantidade deve ser maior que zero");
                return -1;
            }
        }
        catch
        {
            ValidarMSG.Aviso("Quantidade inválida");
            return -1;
        }
    }

    private void AtualizarEstoqueEntrada(Medicamento medicamento, int quantidade)
    {
        medicamento.SetQuantidadeEstoque(medicamento.GetQuantidadeEstoque() + quantidade);

        var repositorioMedicamentos = new RepositorioMedicamento("medicamentos");
        repositorioMedicamentos.Editar(medicamento.Id, medicamento);
    }

    private bool ValidarEstoqueDisponivel(Medicamento medicamento, int quantidade)
    {
        if (medicamento.GetQuantidadeEstoque() < quantidade)
        {
            ValidarMSG.Aviso($"Estoque insuficiente. Disponível: {medicamento.GetQuantidadeEstoque()}");
            return false;
        }
        return true;
    }

    private void AtualizarEstoqueSaida(Medicamento medicamento, int quantidade)
    {
        medicamento.SetQuantidadeEstoque(medicamento.GetQuantidadeEstoque() - quantidade);

        var repositorioMedicamentos = new RepositorioMedicamento("medicamentos");
        repositorioMedicamentos.Editar(medicamento.Id, medicamento);
    }

    private Estoque? ObterEntrada()
    {
        DateTime data = ObterData();

        Medicamento? medicamento = SelecionarMedicamento();
        if (medicamento == null)
            return null;

        Funcionario? funcionario = SelecionarFuncionario();
        if (funcionario == null)
            return null;

        int quantidade = ObterQuantidade();
        if (quantidade <= 0)
            return null;

        AtualizarEstoqueEntrada(medicamento, quantidade);

        return new Estoque(data, medicamento, funcionario, quantidade);
    }

    private Estoque? ObterSaida()
    {
        DateTime data = ObterData();

        Paciente? paciente = SelecionarPaciente();
        if (paciente == null)
            return null;

        var medicamentosSaida = new List<MedicamentoSaida>();
        string continuar = "S";

        while (continuar.ToUpper() == "S")
        {
            Medicamento? medicamento = SelecionarMedicamento(medicamentosSaida.Select(m => m.Medicamento?.Id ?? string.Empty), true);
            if (medicamento == null)
            {
                ValidarMSG.Aviso("Medicamento não selecionado ou não disponível para saída");
                break;
            }

            int quantidade = ObterQuantidade();
            if (quantidade <= 0)
            {
                ValidarMSG.Aviso("Quantidade inválida");
                continue;
            }

            if (!ValidarEstoqueDisponivel(medicamento, quantidade))
            {
                continue;
            }

            medicamentosSaida.Add(new MedicamentoSaida(medicamento, quantidade));
            AtualizarEstoqueSaida(medicamento, quantidade);

            if (medicamentosSaida.Count == 0)
                break;

            Console.Write("Deseja adicionar outro medicamento? (S/N): ");
            continuar = Console.ReadLine() ?? "N";
        }

        if (medicamentosSaida.Count == 0)
        {
            ValidarMSG.Aviso("Nenhum medicamento foi selecionado");
            return null;
        }

        return new Estoque(data, paciente, medicamentosSaida);
    }

    public override void ProcessarOpcaoMenu(string? opcao)
    {
        switch (opcao)
        {
            case "1":
                var entrada = ObterEntrada();
                if (entrada != null)
                {
                    var resultado = repositorio.Cadastrar(entrada);
                    if (resultado == ResultadoOperacao.Sucesso)
                        ValidarMSG.Sucesso("Entrada registrada com sucesso!");
                    else
                        ValidarMSG.Erro("Erro ao registrar entrada");
                }
                ValidarMSG.MensagemContinuar();
                break;
            case "2":
                var saida = ObterSaida();
                if (saida != null)
                {
                    var resultado = repositorio.Cadastrar(saida);
                    if (resultado == ResultadoOperacao.Sucesso)
                        ValidarMSG.Sucesso("Saída registrada com sucesso!");
                    else
                        ValidarMSG.Erro("Erro ao registrar saída");
                }
                ValidarMSG.MensagemContinuar();
                break;
            case "3":
                VisualizarTodos(true);
                break;
            default:
                ValidarMSG.Aviso("Opção inválida. Tente novamente.");
                ValidarMSG.MensagemContinuar();
                break;
        }
    }

    public override string? ObterOpcaoMenu()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de {nomeEntidade}");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Registrar entrada");
        Console.WriteLine("2 - Registrar saída");
        Console.WriteLine("3 - Visualizar todas as operações");
        Console.WriteLine("S - Voltar");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        return Console.ReadLine()?.ToUpper();
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

        foreach (var operacao in lista)
            Console.WriteLine(operacao);

        Console.ReadLine();
    }

    protected override Estoque ObterDadosCadastrais()
    {
        return new Estoque();
    }

    protected override bool PodeEditar => false;
    protected override bool PodeExcluir => false;
    protected override bool PodeCadastrar => false;
}