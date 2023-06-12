using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class DiceManager : MonoBehaviour
{
    public GameObject canvaaas;
    public GameObject dicePrefab;
    public int initialDiceCount = 1;
    public int maxDiceCount = 9;
    public Vector3 diceSpacing;
    public float distance;

    public List<Dice> diceList = new List<Dice>();
    public TextMeshProUGUI sumText;
    public TextMeshProUGUI acertosText;
    public TextMeshProUGUI errosText;

    private int acertosCount = 0;
    private int errosCount = 0;
    private bool isRolling = false;

    private void Start()
    {
        diceList.Clear();
        diceSpacing = new Vector3(3f, 0f, 0f);
    }

    public void CreateDice(int count)
    {
        count = Mathf.Clamp(count, 1, maxDiceCount - diceList.Count);

        for (int i = 0; i < count; i++)
        {
            Vector3 position = new Vector3(0f, 0f, 0f);
            Vector3 soma = position + diceSpacing;
            GameObject diceObject = Instantiate(dicePrefab, new Vector3(distance + 1f, 0f, 0f), Quaternion.identity);
            distance = diceSpacing.x + distance;

            Dice dice = diceObject.GetComponent<Dice>();
            diceList.Add(dice);

            dice.OnDiceRoll += UpdateSum;
            dice.OnDiceRoll += CountAcertos;
            dice.OnDiceRoll += CountErros;
            dice.OnDiceRoll += CheckRollingState;
        }
    }

    public void RemoveDice()
    {
        if (diceList.Count >= 1)
        {
            Dice lastDice = diceList[diceList.Count - 1];
            diceList.Remove(lastDice);
            Destroy(lastDice.gameObject);

            lastDice.OnDiceRoll -= UpdateSum;
            lastDice.OnDiceRoll -= CountAcertos;
            lastDice.OnDiceRoll -= CountErros;
            lastDice.OnDiceRoll -= CheckRollingState;

            UpdateSum();
            CountAcertos();
            CountErros();
        }
    }

    public void UpdateSum()
    {
        int sum = 0;

        foreach (Dice dice in diceList)
        {
            sum += dice.GetDiceValue();
        }

        sumText.text = sum.ToString();
    }

    public void CountAcertos()
    {
        acertosCount = 0;

        foreach (Dice dice in diceList)
        {
            if (dice.GetDiceValue() >= 5)
            {
                acertosCount++;
            }
        }

        acertosText.text = acertosCount.ToString();
    }

    public void CountErros()
    {
        errosCount = 0;

        foreach (Dice dice in diceList)
        {
            if (dice.GetDiceValue() < 5)
            {
                errosCount++;
            }
        }

        errosText.text = errosCount.ToString();
    }

    public void CheckRollingState()
    {
        foreach (Dice dice in diceList)
        {
            if (dice.IsRolling())
            {
                isRolling = true;
                return;
            }
        }

        isRolling = false;
        UpdateSum();
        CountAcertos();
        CountErros();
    }
}
