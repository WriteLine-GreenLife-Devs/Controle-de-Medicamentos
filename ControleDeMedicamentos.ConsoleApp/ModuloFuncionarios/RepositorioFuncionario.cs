using ControleDeMedicamentos.ConsoleApp.Compartilhado;

class RepositorioFuncionario : RepositorioBase<Funcionario>
{
    public RepositorioFuncionario(string nomeArquivo) : base(nomeArquivo)
    {
    }
}