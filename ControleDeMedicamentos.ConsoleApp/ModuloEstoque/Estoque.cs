using System.Dynamic;
using System.Linq;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

namespace ControleDeMedicamentos.ConsoleApp.ModuloEstoque;

public class Estoque : EntidadeBase
{
    public DateTime Data { get; set; } = DateTime.Now;
    public Medicamento? MedicamentoEntrada { get; set; } = null;
    public Funcionario? FuncionarioEntrada { get; set; } = null;
    public int QuantidadeEntrada { get; set; } = 0;
    public string TipoOperacao { get; set; } = "";
    public Paciente? PacienteSaida { get; set; } = null;
    public List<MedicamentoSaida> MedicamentosSaida { get; set; } = new List<MedicamentoSaida>();
    public int QuantidadeSaida { get; set; } = 0;
    public Estoque()
    {
    }

    public Estoque(DateTime data, Medicamento medicamento, Funcionario funcionario, int quantidade)
    {
        Data = data;
        MedicamentoEntrada = medicamento;
        FuncionarioEntrada = funcionario;
        QuantidadeEntrada = quantidade;
        TipoOperacao = "Entrada";
    }

    public Estoque(DateTime data, Paciente paciente, List<MedicamentoSaida> medicamentos)
    {
        Data = data;
        PacienteSaida = paciente;
        MedicamentosSaida = medicamentos;
        TipoOperacao = "Saída";
        QuantidadeSaida = medicamentos.Sum(m => m.Quantidade);
    }

    public DateTime GetData()
    {
        return Data;
    }

    public void SetData(DateTime data)
    {
        Data = data;
    }

    public string GetTipoOperacao()
    {
        return TipoOperacao;
    }

    public void SetTipoOperacao(string tipo)
    {
        TipoOperacao = tipo;
    }

    public Medicamento? GetMedicamentoEntrada()
    {
        return MedicamentoEntrada;
    }

    public void SetMedicamentoEntrada(Medicamento medicamento)
    {
        MedicamentoEntrada = medicamento;
    }

    public Funcionario? GetFuncionarioEntrada()
    {
        return FuncionarioEntrada;
    }

    public void SetFuncionarioEntrada(Funcionario funcionario)
    {
        FuncionarioEntrada = funcionario;
    }

    public int GetQuantidadeEntrada()
    {
        return QuantidadeEntrada;
    }

    public void SetQuantidadeEntrada(int quantidade)
    {
        QuantidadeEntrada = quantidade;
    }

    public Paciente? GetPacienteSaida()
    {
        return PacienteSaida;
    }

    public void SetPacienteSaida(Paciente paciente)
    {
        PacienteSaida = paciente;
    }

    public List<MedicamentoSaida> GetMedicamentosSaida()
    {
        return MedicamentosSaida;
    }

    public void SetMedicamentosSaida(List<MedicamentoSaida> medicamentos)
    {
        MedicamentosSaida = medicamentos;
    }

    public int GetQuantidadeSaida()
    {
        return QuantidadeSaida;
    }

    public void SetQuantidadeSaida(int quantidade)
    {
        QuantidadeSaida = quantidade;
    }

    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        var EstoqueAtualizado = (Estoque)entidadeAtualizada;

        this.Data = EstoqueAtualizado.GetData();
        this.TipoOperacao = EstoqueAtualizado.GetTipoOperacao();
        this.MedicamentoEntrada = EstoqueAtualizado.GetMedicamentoEntrada();
        this.FuncionarioEntrada = EstoqueAtualizado.GetFuncionarioEntrada();
        this.QuantidadeEntrada = EstoqueAtualizado.GetQuantidadeEntrada();
        this.PacienteSaida = EstoqueAtualizado.GetPacienteSaida();
        this.MedicamentosSaida = EstoqueAtualizado.GetMedicamentosSaida();
        this.QuantidadeSaida = EstoqueAtualizado.GetQuantidadeSaida();
    }

    public override string ToString()
    {
        if (TipoOperacao == "Entrada")
        {
            return $"{Id} : [ENTRADA] Data: {Data:dd/MM/yyyy} - Medicamento: {MedicamentoEntrada?.GetNome()} - Funcionário: {FuncionarioEntrada?.GetNome()} - Quantidade: {QuantidadeEntrada}";
        }
        else if (TipoOperacao == "Saída")
        {
            string medicamentos = string.Join(", ", MedicamentosSaida.Select(m => $"{m.Medicamento?.GetNome()} ({m.Quantidade})"));
            return $"{Id} : [SAÍDA] Data: {Data:dd/MM/yyyy} - Paciente: {PacienteSaida?.Nome} - Medicamentos: {medicamentos}";
        }
        return $"{Id} : Operação desconhecida";
    }
}

public class MedicamentoSaida
{
    public Medicamento? Medicamento { get; set; }
    public int Quantidade { get; set; }

    public MedicamentoSaida()
    {
    }

    public MedicamentoSaida(Medicamento medicamento, int quantidade)
    {
        Medicamento = medicamento;
        Quantidade = quantidade;
    }
}
