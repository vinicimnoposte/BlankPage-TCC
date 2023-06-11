using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSpawner : MonoBehaviour
{
    public GameObject dicePrefab; // Prefab do dado
    public int initialDiceCount = 1; // Quantidade inicial de dados
    public int maxDiceCount = 10; // Quantidade m�xima de dados

    private void Start()
    {
        SpawnDice();
    }

    private void SpawnDice()
    {
        GameObject scrollViewContent = transform.GetChild(0).gameObject; // Obt�m o objeto cont�iner da lista dentro do ScrollView

        // Limpa todos os dados existentes dentro do conte�do da lista
        foreach (Transform child in scrollViewContent.transform)
        {
            Destroy(child.gameObject);
        }

        // Cria e posiciona os dados na tela
        for (int i = 0; i < initialDiceCount; i++)
        {
            Vector3 position = new Vector3(0f, -i * 100f, 0f); // Posi��o do dado na tela (ajuste conforme necess�rio)
            GameObject diceObject = Instantiate(dicePrefab, scrollViewContent.transform);
            diceObject.transform.localPosition = position;
        }
    }
}