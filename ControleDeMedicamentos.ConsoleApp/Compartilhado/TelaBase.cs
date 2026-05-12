using ControleDeMedicamentos.ConsoleApp.Compartilhado.Operacoes;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public abstract class TelaBase<T> : ITela where T : EntidadeBase
{
    public string nomeEntidade { get; set; } = string.Empty;
    protected RepositorioBase<T> repositorio;
    protected virtual bool PodeCadastrar => true;
    protected virtual bool PodeEditar => true;
    protected virtual bool PodeExcluir => true;
    protected virtual bool PodeVisualizar => true;

    protected TelaBase(string nomeEntidade, RepositorioBase<T> repositorio)
    {
        this.nomeEntidade = nomeEntidade;
        this.repositorio = repositorio;
    }

    public void ExibirMenuModulo()
    {
        while (true)
        {
            string? opcao = ObterOpcaoMenu();

            if (opcao == "S")
                break;

            ProcessarOpcaoMenu(opcao);
        }
    }

    public virtual void ProcessarOpcaoMenu(string? opcao)
    {
        switch (opcao)
        {
            case "1" when PodeCadastrar:
                Cadastrar();
                break;
            case "2" when PodeEditar:
                Editar();
                break;
            case "3" when PodeExcluir:
                Excluir();
                break;
            case "4" when PodeVisualizar:
                VisualizarTodos(true);
                break;
            default:
                ValidarMSG.Aviso("Opção inválida. Tente novamente.");
                ValidarMSG.MensagemContinuar();
                break;
        }
    }

    public virtual string? ObterOpcaoMenu()
    {
        return ObterOpcaoMenuInterno(PodeCadastrar, PodeEditar, PodeExcluir, PodeVisualizar);
    }

    protected virtual string? ObterOpcaoMenuInterno(bool podeCadastrar, bool podeEditar, bool podeExcluir, bool podeVisualizar)
    {
        string nomeMinusculo = nomeEntidade.ToLower();

        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de {nomeEntidade}");
        Console.WriteLine("---------------------------------");

        if (podeCadastrar)
            Console.WriteLine($"1 - Cadastro de {nomeMinusculo}");

        if (podeEditar)
            Console.WriteLine($"2 - Edição de {nomeMinusculo}");

        if (podeExcluir)
            Console.WriteLine($"3 - Exclusão de {nomeMinusculo}");

        if (podeVisualizar)
            Console.WriteLine($"4 - Visualização de {nomeMinusculo}");

        Console.WriteLine("S - Voltar");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        return Console.ReadLine()?.ToUpper();
    }

    public void Cadastrar()
    {
        ExibirCabecalho($"Cadastro de {nomeEntidade}");

        T novaEntidade;

        do
        {
            novaEntidade = ObterDadosCadastrais();

            if (novaEntidade == null)
                return;

            if (!ValidarEntidade(novaEntidade))
            {
                ValidarMSG.MensagemContinuar();
                continue;
            }

            break;
        } while (true);

        var resultado = repositorio.Cadastrar(novaEntidade);
        MsgOperacao.Exibir(resultado, novaEntidade.Id, nomeEntidade, "cadastrado");
    }

    public void Editar()
    {
        ExibirCabecalho($"Edição de {nomeEntidade}");
        VisualizarTodos(deveExibirCabecalho: false);

        Console.WriteLine("---------------------------------");

        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID do registro que deseja editar: ");
            idSelecionado = Console.ReadLine()?.ToUpper();

            if (string.IsNullOrWhiteSpace(idSelecionado))
            {
                ValidarMSG.Aviso("Operação cancelada.", nomeEntidade);
                ValidarMSG.MensagemContinuar();
                return;
            }

            if (RepositorioValidacao.IdValido(idSelecionado, repositorio.SelecionarTodos(), nomeEntidade))
                break;
        } while (true);

        Console.WriteLine("---------------------------------");

        T novaEntidade;

        do
        {
            novaEntidade = ObterDadosCadastrais();

            if (novaEntidade == null)
                return;

            if (!ValidarEntidade(novaEntidade, idSelecionado))
            {
                ValidarMSG.MensagemContinuar();
                continue;
            }

            break;
        } while (true);

        var resultado = repositorio.Editar(idSelecionado!, novaEntidade);
        MsgOperacao.Exibir(resultado, idSelecionado!, nomeEntidade, "editado");
    }

    public void Excluir()
    {
        ExibirCabecalho($"Exclusão de {nomeEntidade}");
        VisualizarTodos(deveExibirCabecalho: false);

        Console.WriteLine("---------------------------------");

        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID do registro que deseja excluir: ");
            idSelecionado = Console.ReadLine()?.ToUpper();

            if (string.IsNullOrWhiteSpace(idSelecionado))
            {
                ValidarMSG.Aviso("Operação cancelada.", nomeEntidade);
                ValidarMSG.MensagemContinuar();
                return;
            }

            if (RepositorioValidacao.IdValido(idSelecionado, repositorio.SelecionarTodos(), nomeEntidade))
                break;
        } while (true);

        var entidade = repositorio.SelecionarPorId(idSelecionado!);

        if (ValidarVinculos != null && entidade != null && ValidarVinculos(entidade))
        {
            ValidarMSG.Erro($"\"{entidade}\" não pode ser excluído pois possui vínculos.", "Registro");
            ValidarMSG.MensagemContinuar();
        }

        var resultado = repositorio.Excluir(idSelecionado!, ValidarVinculos);
        MsgOperacao.Exibir(resultado, idSelecionado!, nomeEntidade, "excluído");
    }

    public abstract void VisualizarTodos(bool deveExibirCabecalho);

    protected void ExibirCabecalho(string titulo)
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de {nomeEntidade}");
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"{titulo}");
        Console.WriteLine("---------------------------------");
    }

    protected abstract T ObterDadosCadastrais();
    protected virtual Func<T, bool>? ValidarVinculos => null;
    protected virtual bool ValidarEntidade(T entidade, string? idAtual = null) => true;
}