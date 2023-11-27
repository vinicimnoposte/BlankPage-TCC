using UnityEngine;

public class ControleBotoes : MonoBehaviour
{
    public SorteioController sorteioController;

    public void BotaoAdicionarClicado()
    {
        sorteioController.AdicionarDado();
        // Chame outras a��es necess�rias quando o bot�o de adi��o for clicado
    }

    public void BotaoExcluirClicado()
    {
        sorteioController.ExcluirDado();
        // Chame outras a��es necess�rias quando o bot�o de exclus�o for clicado
    }

    public void BotaoSortearClicado()
    {
        sorteioController.IniciarSorteio();
        // Chame outras a��es necess�rias quando o bot�o de sorteio for clicado
    }
}
