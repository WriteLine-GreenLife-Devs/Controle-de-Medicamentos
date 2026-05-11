using System.Dynamic;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

public class Estoque : EntidadeBase
{
    public DateTime dateTime = DateTime.Now;
    public int QuantidadeEstoque { get; set; } = 0;
    public int QuantidadeEntrada { get; set; } = 0;
    public int QuantidadeSaida { get; set; } = 0;
    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        var EstoqueAtualizado = (Estoque)entidadeAtualizada;

        this.QuantidadeEstoque = EstoqueAtualizado.GetQuantidadeEstoque();
    }
    public int GetQuantidadeEstoque()
    {
        return QuantidadeEstoque;
    }
}
