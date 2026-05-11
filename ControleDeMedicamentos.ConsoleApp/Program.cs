using ControleDeMedicamentos.ConsoleApp.Compartilhado;

class Program
{
    static void Main(string[] args)
    {

        var telas = new List<TelaBase<EntidadeBase>>()
        {

        };

        var telaPrincipal = new TelaPrincipal(telas);
        telaPrincipal.ExibirMenu();
    }
}