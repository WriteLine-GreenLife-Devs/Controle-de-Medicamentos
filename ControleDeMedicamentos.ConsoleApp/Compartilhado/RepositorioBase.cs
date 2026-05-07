using System.Text.Json;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

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

    public void Cadastrar(T entidade)
    {
        registros.Add(entidade);
        Salvar();
    }

    public virtual bool Editar(string idSelecionado, T entidadeAtualizada)
    {
        T? registroSelecionado = SelecionarPorId(idSelecionado);

        if (registroSelecionado == null)
            return false;

        registroSelecionado.AtualizarDados(entidadeAtualizada);
        Salvar();

        return true;
    }

    public virtual bool Excluir(string idSelecionado, Func<T, bool>? possuiVinculos = null)
    {
        var entidade = SelecionarPorId(idSelecionado);

        if (entidade == null)
            return false;

        if (possuiVinculos != null && possuiVinculos(entidade))
            return false;

        registros.Remove(entidade);
        Salvar();
        return true;
    }

    public virtual T? SelecionarPorId(string idSelecionado) => registros.FirstOrDefault(reg => reg.Id == idSelecionado);

    private void Salvar()
    {
        var json = JsonSerializer.Serialize(registros, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(caminhoArquivo, json);
    }

    private List<T> Carregar()
    {
        if (!File.Exists(caminhoArquivo))
            return new List<T>();

        var json = File.ReadAllText(caminhoArquivo);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }

    public List<T> SelecionarTodos() => registros;
}