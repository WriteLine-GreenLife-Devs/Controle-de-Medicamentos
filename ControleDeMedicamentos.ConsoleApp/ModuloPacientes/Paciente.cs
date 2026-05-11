using System.Dynamic;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class Paciente : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string CNS { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string GetCPF()
    {
        return CPF;
    }
    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        var pacienteAtualizado = (Paciente)entidadeAtualizada;

        Nome = pacienteAtualizado.Nome;
        CPF = pacienteAtualizado.CPF;
        CNS = pacienteAtualizado.CNS;
        Telefone = pacienteAtualizado.Telefone;
    }

    public override string ToString() => $"{Id} : {Nome} - {CPF} - {CNS} - {Telefone}";
}