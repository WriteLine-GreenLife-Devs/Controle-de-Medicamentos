using ControleDeMedicamentos.ConsoleApp.Compartilhado;

class Fornecedor : EntidadeBase
{
    public string Nome = string.Empty;
    public string Telefone = string.Empty;
    public string CNPJ = string.Empty;

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
    public string GetCNPJ()
    {
        return CNPJ;
    }
    public void SetCNPJ(string cnpj)
    {
        CNPJ = cnpj;
    }

    #endregion

    #region Métodos
    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        var fornecedorAtualizado = (Fornecedor)entidadeAtualizada;

        this.Nome = fornecedorAtualizado.GetNome();
        this.Telefone = fornecedorAtualizado.GetTelefone();
        this.CNPJ = fornecedorAtualizado.GetCNPJ();
    }

    public override string ToString() => $"{Id} : {Nome} - {CNPJ} - {Telefone}";

    #endregion
}