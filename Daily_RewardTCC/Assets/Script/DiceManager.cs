using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceManager : MonoBehaviour
{
    public GameObject diceContainer; // Objeto pai dos dados
    public GameObject dicePrefab;
    public int initialDiceCount = 1;
    public int maxDiceCount = 9;
    public Vector3 diceSpacing;
    public float distance;
    public GameObject ancora1, ancora2, ancora3, ancora4, ancora5, ancora6, ancora7, ancora8, ancora9;

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
        diceSpacing = new Vector3(3f, 3f, 3f);
    }

    public void CreateDice(int count)
    {
        count = Mathf.Clamp(count, 1, maxDiceCount - diceList.Count);

        for (int i = 0; i < count; i++)
        {
            Vector3 position = new Vector3(0f, 0f, 0f);
            // Vector3 soma = position + diceSpacing;

            switch (diceList.Count)
            {
                case 0:
                    position = ancora1.transform.position;
                    break;
                case 1:
                    position = ancora2.transform.position;
                    break;
                case 2:
                    position = ancora3.transform.position;
                    break;
                case 3:
                    position = ancora4.transform.position;
                    break;
                case 4:
                    position = ancora5.transform.position;
                    break;
                case 5:
                    position = ancora6.transform.position;
                    break;
                case 6:
                    position = ancora7.transform.position;
                    break;
                case 7:
                    position = ancora8.transform.position;
                    break;
                case 8:
                    position = ancora9.transform.position;
                    break;
     
            }
                    


            GameObject diceObject = Instantiate(dicePrefab, position, Quaternion.identity, diceContainer.transform);
            distance = diceSpacing.x + distance;


            position = position + diceSpacing;

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
            if (dice.GetDiceValue() >= 6)
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
            if (dice.GetDiceValue() < 6)
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