using System.Net;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public abstract class TelaBase<T> : ITela where T : EntidadeBase
{
    public string nomeEntidade = string.Empty;
    protected RepositorioBase<T> repositorio;

    protected TelaBase(string nomeEntidade, RepositorioBase<T> repositorio)
    {
        this.nomeEntidade = nomeEntidade;
        this.repositorio = repositorio;
    }

    public virtual string? ObterOpcaoMenu()
    {
        string nomeMinusculo = nomeEntidade.ToLower();

        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de {nomeEntidade}");
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"1 - Cadastro de {nomeMinusculo}");
        Console.WriteLine($"2 - Edição de {nomeMinusculo}");
        Console.WriteLine($"3 - Exclusão de {nomeMinusculo}");
        Console.WriteLine($"4 - Visualização de {nomeMinusculo}");
        Console.WriteLine("S - Voltar para o início");
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

        repositorio.Cadastrar(novaEntidade);
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

        var parametroAtual = repositorio.SelecionarPorId(idSelecionado!);

        T novaEntidade;

        do
        {
            novaEntidade = ObterDadosCadastrais();

            if (!ValidarEntidade(novaEntidade))
            {
                ValidarMSG.MensagemContinuar();
                continue;
            }

            break;
        } while (true);

        bool conseguiuEditar = repositorio.Editar(idSelecionado!, novaEntidade);

        if (!conseguiuEditar)
            return;
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

        bool conseguiuExcluir = repositorio.Excluir(idSelecionado!, ValidarVinculos);

        if (!conseguiuExcluir)
        {
            ValidarMSG.Erro("Não foi possível excluir o registro requisitado.", nomeEntidade);
            ValidarMSG.MensagemContinuar();
            return;
        }
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
    protected virtual bool ValidarEntidade(T entidade) => true;
}