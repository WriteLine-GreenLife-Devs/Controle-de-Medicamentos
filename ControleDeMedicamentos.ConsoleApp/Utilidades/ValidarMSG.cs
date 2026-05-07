namespace ControleDeMedicamentos.ConsoleApp.Utilidades;
public static class ValidarMSG
{
    public static void Erro(string mensagem, string assunto = "Sistema")
    {
        var registro = new Validacao(assunto, mensagem, "Erro");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(registro.ToString());
        Console.ResetColor();
    }

    public static void Aviso(string mensagem, string assunto = "Sistema")
    {
        var registro = new Validacao(assunto, mensagem, "Aviso");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(registro.ToString());
        Console.ResetColor();
    }

    public static void Sucesso(string mensagem, string assunto = "Sistema")
    {
        var registro = new Validacao(assunto, mensagem, "Sucesso");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(registro.ToString());
        Console.ResetColor();
    }

    public static void MensagemContinuar()
    {
        Console.WriteLine("---------------------------------");
        Console.Write("Pressione ENTER para continuar...");
        Console.ReadLine();
    }
}