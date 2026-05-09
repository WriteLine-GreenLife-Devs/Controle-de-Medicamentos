namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public interface ITela
{
    string nomeEntidade { get; }
    string? ObterOpcaoMenu();
    void ExibirMenuModulo();
}