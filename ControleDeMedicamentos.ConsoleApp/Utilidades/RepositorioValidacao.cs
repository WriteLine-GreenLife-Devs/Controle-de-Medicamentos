using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.Utilidades;

public static class RepositorioValidacao
{
    public static void Confirmacao(string mensagem, string assunto = "Sistema")
    {
        var registro = new Validacao(assunto, mensagem, "Confirmação");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(registro.ToString());
        Console.ResetColor();
    }

    public static bool IdValido<T>(string? id, List<T> lista, string assunto = "Validação") where T : EntidadeBase
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            ValidarMSG.Aviso("Operação cancelada.", assunto);
            return false;
        }

        if (!lista.Any(entidade => entidade.Id == id))
        {
            ValidarMSG.Erro("ID inválido. Digite um ID válido ou pressione ENTER para cancelar.", assunto);
            return false;
        }

        return true;
    }
}