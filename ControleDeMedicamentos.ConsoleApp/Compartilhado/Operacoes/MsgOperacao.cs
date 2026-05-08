using ControleDeMedicamentos.ConsoleApp.Utilidades;

namespace ControleDeMedicamentos.ConsoleApp.Compartilhado.Operacoes;

public static class MsgOperacao
{
    public static void Exibir(ResultadoOperacao resultado, string id, string nomeEntidade, string acao)
    {
        switch (resultado)
        {
            case ResultadoOperacao.Sucesso:
                ValidarMSG.Sucesso($"\"{id}\" {acao} com sucesso.", nomeEntidade);
                break;

            case ResultadoOperacao.NaoEncontrado:
                ValidarMSG.Erro($"Registro não encontrado para {acao}.", nomeEntidade);
                break;

            case ResultadoOperacao.PossuiVinculos:
                ValidarMSG.Erro($"\"{id}\" não pode ser {acao} pois possui vínculos.", nomeEntidade);
                break;

            case ResultadoOperacao.ErroValidacao:
                ValidarMSG.Erro($"Erro de validação ao {acao}.", nomeEntidade);
                break;
        }
        ValidarMSG.MensagemContinuar();
    }
}
