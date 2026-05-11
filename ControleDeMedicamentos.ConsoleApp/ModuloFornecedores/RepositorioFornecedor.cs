using ControleDeMedicamentos.ConsoleApp.Compartilhado;

class RepositorioFornecedor : RepositorioBase<Fornecedor>
{
    public RepositorioFornecedor(string nomeArquivo) : base(nomeArquivo)
    {
    }
}