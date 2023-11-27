using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SorteioController : MonoBehaviour
{
    public TextMeshProUGUI resultadoText;
    private bool sorteioAtivo = false;
    private int quantidadeDados = 1; // Inicializa com 1 dado


    public void IniciarSorteio()
    {
        if (!sorteioAtivo)
        {
            sorteioAtivo = true;
            resultadoText.text = "Sorteando...";
            StartCoroutine(SorteioCoroutine(quantidadeDados));
        }
    }

    private IEnumerator SorteioCoroutine(int quantidade)
    {
        for (int i = 0; i < 10; i++)
        {
            int numeroSorteado = Random.Range(0, 11);
            resultadoText.text = "Rolling:" + (i + 1) + ": " + numeroSorteado;
            yield return new WaitForSeconds(0.5f); // Ajuste o tempo de espera conforme necessário
        }

        resultadoText.text = "Sorteio Concluído!";
        sorteioAtivo = false;
    }

    public void AdicionarDado()
    {
        quantidadeDados++;
        AtualizarContador();
    }

    public void ExcluirDado()
    {
        quantidadeDados = Mathf.Max(1, quantidadeDados - 1); // Garante que nunca será menor que 1
        AtualizarContador();
    }

    private void AtualizarContador()
    {
        // Atualiza o texto do contador na interface do usuário
        // (você pode adicionar um TextMeshProUGUI para exibir o contador)
    }
}
