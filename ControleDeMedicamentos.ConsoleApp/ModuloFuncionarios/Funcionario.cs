using ControleDeMedicamentos.ConsoleApp.Compartilhado;

public class Funcionario : EntidadeBase
{
    public string Nome = string.Empty;
    public string Telefone = string.Empty;
    public string CPF = string.Empty;

    #region Getters e Setters

    public string GetNome()
    {
        return Nome;
    }
    public void SetNome(string nome)
    {
        Nome = nome;
    }
    public string GetTelefone()
    {
        return Telefone;
    }
    public void SetTelefone(string telefone)
    {
        Telefone = telefone;
    }
    public string GetCPF()
    {
        return CPF;
    }
    public void SetCPF(string cpf)
    {
        CPF = cpf;
    }

    #endregion

    #region Métodos
    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        var funcionarioAtualizado = (Funcionario)entidadeAtualizada;

        this.Nome = funcionarioAtualizado.GetNome();
        this.Telefone = funcionarioAtualizado.GetTelefone();
        this.CPF = funcionarioAtualizado.GetCPF();
    }

    public override string ToString() => $"{Id} : {Nome} - {CPF} - {Telefone}";

    #endregion
}