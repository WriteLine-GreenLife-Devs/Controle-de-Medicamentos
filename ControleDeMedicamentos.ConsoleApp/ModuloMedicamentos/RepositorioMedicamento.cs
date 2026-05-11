using ControleDeMedicamentos.ConsoleApp.Compartilhado;

class RepositorioMedicamento : RepositorioBase<Medicamento>
{
    public RepositorioMedicamento(string nomeArquivo) : base(nomeArquivo)
    {
    }
}