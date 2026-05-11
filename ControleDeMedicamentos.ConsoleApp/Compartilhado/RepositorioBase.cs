using System.Text.Json;
using System.Text.Json.Serialization;
using ControleDeMedicamentos.ConsoleApp.Compartilhado.Operacoes;
namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public abstract class RepositorioBase<T> where T : EntidadeBase
{
    private readonly string caminhoArquivo;
    private readonly List<T> registros;

    public RepositorioBase(string nomeArquivo)
    {
        string pastaDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        caminhoArquivo = Path.Combine(pastaDocumentos, $"{nomeArquivo}.json");

        registros = Carregar();
    }

    public ResultadoOperacao Cadastrar(T entidade)
    {
        registros.Add(entidade);
        return Salvar();
    }

    public ResultadoOperacao Editar(string idSelecionado, T entidadeAtualizada)
    {
        var registroSelecionado = SelecionarPorId(idSelecionado);

        if (registroSelecionado == null)
            return ResultadoOperacao.NaoEncontrado;

        registroSelecionado.AtualizarDados(entidadeAtualizada);
        return Salvar();
    }

    public virtual ResultadoOperacao Excluir(string idSelecionado, Func<T, bool>? possuiVinculos = null)
    {
        var entidade = SelecionarPorId(idSelecionado);

        if (entidade == null)
            return ResultadoOperacao.NaoEncontrado;

        if (possuiVinculos != null && possuiVinculos(entidade))
            return ResultadoOperacao.PossuiVinculos;

        registros.Remove(entidade);
        Salvar();
        return ResultadoOperacao.Sucesso;
    }

    public virtual T? SelecionarPorId(string idSelecionado) => registros.FirstOrDefault(reg => reg.Id == idSelecionado);

    private ResultadoOperacao Salvar()
    {
        try
        {
            var opcoes = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                PropertyNamingPolicy = null,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never,
                IncludeFields = true
            };
            var json = JsonSerializer.Serialize(registros, opcoes);
            File.WriteAllText(caminhoArquivo, json);
            return ResultadoOperacao.Sucesso;
        }
        catch
        {
            return ResultadoOperacao.ErroValidacao;
        }
    }

    private List<T> Carregar()
    {
        if (!File.Exists(caminhoArquivo))
            return new List<T>();

        var json = File.ReadAllText(caminhoArquivo);
        var opcoes = new JsonSerializerOptions 
        { 
            PropertyNamingPolicy = null,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never,
            IncludeFields = true
        };
        return JsonSerializer.Deserialize<List<T>>(json, opcoes) ?? new List<T>();
    }

    public List<T> SelecionarTodos() => registros;
}