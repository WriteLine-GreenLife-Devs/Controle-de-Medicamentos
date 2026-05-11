using System.Net;
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPacientes;

public class TelaPaciente : TelaBase<Paciente>
{
    public TelaPaciente(string nomeEntidade, RepositorioBase<Paciente> repositorio) : base(nomeEntidade, repositorio)
    {
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho($"Visualização de {nomeEntidade}");

        var lista = repositorio.SelecionarTodos();
        if (lista.Count == 0)
        {
            ValidarMSG.Aviso("Nenhum registro encontrado", nomeEntidade);
            return;
        }
        foreach (var paciente in lista)
            Console.WriteLine(paciente);

        Console.ReadLine();
    }

    protected override Paciente ObterDadosCadastrais()
    {
        bool verifica = false;
        try
        {
            Console.Write($"Digite o nome do {nomeEntidade}: ");
            string nome = Console.ReadLine()?.ToUpper() ?? string.Empty;

            verifica = ValidarTamanhoNome(nome, 3, 100);

            if (verifica == false)
            {
                Console.WriteLine("Tamanho do Nome do Fornecedor deve conter entre 3 à 100 caracteres! Tente Novamente.");
                Thread.Sleep(3000);
                while (Console.KeyAvailable) Console.ReadKey(true);
                Console.Clear();
                return null;
            }

            Console.Write($"Digite o CPF do {nomeEntidade}: ");
            string cpf = Console.ReadLine() ?? string.Empty;

            cpf = VerificarCPF(cpf);

            if (cpf == "")
            {
                Console.WriteLine("CPF inválido, por favor tente novamente!");
                Thread.Sleep(3000);
                while (Console.KeyAvailable) Console.ReadKey(true);
                Console.Clear();
                return null;
            }

            verifica = VerificarCPFExistente(cpf);

            if (verifica == true)
            {
                Console.WriteLine("CPF já existente, por favor tente novamente!");
                return null;
            }

            Console.Write($"Digite o número do cartão do SUS do {nomeEntidade}: ");
            string cns = Console.ReadLine() ?? string.Empty;

            verifica = ValidarTamanhoString(cns, 15, 15);

            if (verifica == false)
            {
                Console.WriteLine("O Cartão do SUS deve conter 15 caracteres. Por favor, tente novamente.");
                Thread.Sleep(3000);
                while (Console.KeyAvailable) Console.ReadKey(true);
                Console.Clear();
                return null;
            }

            Console.Write($"Digite o telefone de contato do {nomeEntidade}: ");
            string telefone = Console.ReadLine() ?? string.Empty;

            telefone = VerificarTelefoneP(telefone);

            if (telefone == "")
            {
                Thread.Sleep(3000);
                while (Console.KeyAvailable) Console.ReadKey(true);
                Console.Clear();
                return null;
            }

            return new Paciente { Nome = nome, CPF = cpf, CNS = cns, Telefone = telefone };
        }
        catch (System.Exception)
        {
            Console.WriteLine("Erro, verifique se os parêmetros foram passados corretamente!");
            Thread.Sleep(3000);
            while (Console.KeyAvailable) Console.ReadKey(true);
            Console.Clear();
            return null;
        }
    }
    public string VerificarTelefoneP(string telefone)
    {
        string apenasNumeros = System.Text.RegularExpressions.Regex.Replace(telefone ?? "", @"[^\d]", "");

        int tamanho = apenasNumeros.Length;

        if (tamanho == 10)
        {
            return long.Parse(apenasNumeros).ToString(@"(00) 0000-0000");
        }
        else if (tamanho == 11)
        {
            return long.Parse(apenasNumeros).ToString(@"(00) 0 0000-0000");
        }
        else
        {
            Console.WriteLine("Número de Telefone inválido (formato validado: 10-11 dígitos), tente novamente!");
            return "";
        }
    }
    public bool ValidarTamanhoString(string texto, int tamanhoMinimo, int tamanhoMaximo)
    {
        if (string.IsNullOrEmpty(texto))
        {
            return false;
        }

        int tamanho = texto.Length;

        if (tamanho >= tamanhoMinimo && tamanho <= tamanhoMaximo)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public string VerificarCPF(string cpf)
    {
        string apenasNumeros = System.Text.RegularExpressions.Regex.Replace(cpf ?? "", @"[^\d]", "");
        int tamanho = apenasNumeros.Length;

        if (tamanho == 11)
        {
            return Convert.ToUInt64(apenasNumeros).ToString(@"000\.000\.000\-00");
        }
        else
        {
            Console.WriteLine("CPF inválido (deve conter 11 dígitos), tente novamente!");
            return null;
        }
    }
    public bool VerificarCPFExistente(string cpf)
    {
        bool verifica = false;

        var listaPacientes = repositorio.SelecionarTodos();

        foreach (var paciente in listaPacientes)
        {
            if (paciente.GetCPF() == cpf)
            {
                verifica = true;
            }
        }
        return verifica;
    }
    public bool ValidarTamanhoNome(string texto, int tamanhoMinimo, int tamanhoMaximo)
    {
        if (string.IsNullOrEmpty(texto))
        {
            return false;
        }

        int tamanho = texto.Length;

        if (tamanho >= tamanhoMinimo && tamanho <= tamanhoMaximo)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public string VerificarTelefone(string telefone)
    {
        string apenasNumeros = System.Text.RegularExpressions.Regex.Replace(telefone ?? "", @"[^\d]", "");

        int tamanho = apenasNumeros.Length;

        if (tamanho == 10)
        {
            return long.Parse(apenasNumeros).ToString(@"(00) 0000-0000");
        }
        else if (tamanho == 11)
        {
            return long.Parse(apenasNumeros).ToString(@"(00) 0 0000-0000");
        }
        else
        {
            Console.WriteLine("Número de Telefone inválido (formato validado: 10-11 dígitos), tente novamente!");
            return "";
        }
    }

}