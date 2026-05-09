using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

class Program
{
    static void Main(string[] args)
    {

        var telas = new List<ITela>()
        {

        };

        var telaPrincipal = new TelaPrincipal(telas);
        telaPrincipal.ExibirMenu();
    }
}