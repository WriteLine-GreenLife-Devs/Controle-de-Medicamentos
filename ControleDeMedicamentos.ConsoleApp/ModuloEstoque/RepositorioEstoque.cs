using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

class RepositorioEstoque : RepositorioBase<Estoque>
{
    public RepositorioEstoque(string nomeArquivo) : base(nomeArquivo)
    {
    }
}