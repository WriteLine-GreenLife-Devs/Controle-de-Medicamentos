using System.Security.Cryptography;

namespace ControleDeMedicamentos.ConsoleApp.Compartilhado
{
    public abstract class EntidadeBase
    {
        public string Id { get; set; } = string.Empty;

        public EntidadeBase()
        {
            if (string.IsNullOrEmpty(Id))
                Id = Convert
                        .ToHexString(RandomNumberGenerator.GetBytes(3))
                        .ToUpper()
                        .Substring(0, 5);
        }

        public abstract void AtualizarDados(EntidadeBase entidadeAtualizada);
    }
}