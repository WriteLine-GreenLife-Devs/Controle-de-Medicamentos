namespace ControleDeMedicamentos.ConsoleApp.Utilidades;
public class Validacao
{
    public string Assunto { get; }
    public string Mensagem { get; }
    public string GrauSeveridade { get; }

    public Validacao(string assunto, string mensagem, string grauSeveridade = "Erro")
    {
        Assunto = assunto;
        Mensagem = mensagem;
        GrauSeveridade = grauSeveridade;
    }

    public override string ToString() => $"[{GrauSeveridade}] {Assunto} : {Mensagem}";
}