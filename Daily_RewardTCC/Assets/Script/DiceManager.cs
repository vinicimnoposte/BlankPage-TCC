using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public GameObject canvaaas;
    public GameObject dicePrefab; // Prefab do dado
    public int initialDiceCount = 1; // Quantidade inicial de dados
    public int maxDiceCount = 9; // Quantidade máxima de dados
    public Vector3 diceSpacing; // Espaçamento entre os dados
    public float distance;

    public List<Dice> diceList = new List<Dice>(); // Lista dos dados

    private void Start()
    {
        //CreateDice(initialDiceCount);
        diceList.Clear();
        diceSpacing = new Vector3(3f, 0f, 0f);
    }

    public void CreateDice(int count)
    {
        // Verifica se o número de dados a serem criados excede a quantidade máxima permitida
        count = Mathf.Clamp(count, 1, maxDiceCount - diceList.Count);

        // Cria e posiciona os dados na tela
        for (int i = 0; i < count; i++)
        {
            Vector3 position = new Vector3(0f, 0f, 0f); // Posição do dado na tela
            Vector3 soma = position + diceSpacing;
            GameObject diceObject = Instantiate(dicePrefab, new Vector3(distance + 1f,0f,0f), Quaternion.identity); // Instancia o dado
            distance = diceSpacing.x + distance;

            Dice dice = diceObject.GetComponent<Dice>(); // Obtém a referência do componente Dice
            diceList.Add(dice); // Adiciona o dado à lista
        }

       
    }

    public void RemoveDice()
    {
       

        if (diceList.Count >= 1)
        {
            // Remove o último dado da lista
            Dice lastDice = diceList[diceList.Count - 1];
            diceList.Remove(lastDice);
            Destroy(lastDice.gameObject);
        }
    }
}
