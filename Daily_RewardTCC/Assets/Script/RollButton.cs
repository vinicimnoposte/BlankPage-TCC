using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RollButton : MonoBehaviour
{
    public Button rollButton;
    public DiceManager diceManager;
    public Dice dice;

    public void Start()
    {
        rollButton.onClick.AddListener(() => RollDice());
    }

    public void RollDice()
    {
        foreach (Dice dice in diceManager.diceList)
        {
            dice.RollTheDice();
        }
    }

}
