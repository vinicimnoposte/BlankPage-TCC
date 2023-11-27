using UnityEngine;

public class ControleBotoes : MonoBehaviour
{
    public SorteioController sorteioController;

    public void BotaoAdicionarClicado()
    {
        sorteioController.AdicionarDado();
        // Chame outras ações necessárias quando o botão de adição for clicado
    }

    public void BotaoExcluirClicado()
    {
        sorteioController.ExcluirDado();
        // Chame outras ações necessárias quando o botão de exclusão for clicado
    }

    public void BotaoSortearClicado()
    {
        sorteioController.IniciarSorteio();
        // Chame outras ações necessárias quando o botão de sorteio for clicado
    }
}
