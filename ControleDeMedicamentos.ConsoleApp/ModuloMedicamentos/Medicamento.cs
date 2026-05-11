using ControleDeMedicamentos.ConsoleApp.Compartilhado;

class Medicamento : EntidadeBase
{
    public string Nome = string.Empty;
    public string Descricao = string.Empty;
    public int QuantidadeEstoque = 0;
    public Fornecedor Fornecedor = new Fornecedor();

    #region Construtores
    public Medicamento(string nome, string descricao, int quantidadeEstoque, Fornecedor fornecedor)
    {
        Nome = nome;
        Descricao = descricao;
        QuantidadeEstoque = quantidadeEstoque;
        Fornecedor = fornecedor;
    }
    public Medicamento()
    {
    }

    #endregion

    #region Getters e Setters
    public string GetNome()
    {
        return Nome;
    }
    public void SetNome(string nome)
    {
        Nome = nome;
    }
    public string GetDescricao()
    {
        return Descricao;
    }
    public void SetDescricao(string descricao)
    {
        Descricao = descricao;
    }
    public int GetQuantidadeEstoque()
    {
        return QuantidadeEstoque;
    }
    public void SetQuantidadeEstoque(int quantidadeEstoque)
    {
        QuantidadeEstoque = quantidadeEstoque;
    }
    public Fornecedor GetFornecedor()
    {
        return Fornecedor;
    }
    public void SetFornecedor(Fornecedor fornecedor)
    {
        Fornecedor = fornecedor;
    }

    #endregion

    #region Métodos
    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        var medicamentoAtualizado = (Medicamento)entidadeAtualizada;

        this.Nome = medicamentoAtualizado.GetNome();
        this.Descricao = medicamentoAtualizado.GetDescricao();
        this.QuantidadeEstoque = medicamentoAtualizado.GetQuantidadeEstoque();
        this.Fornecedor = medicamentoAtualizado.GetFornecedor();
    }
    public override string ToString() => $"{Id} : {Nome} - {Descricao} - Quantidade em Estoque: {QuantidadeEstoque} - Fornecedor: {Fornecedor.GetNome()}";

    #endregion
}