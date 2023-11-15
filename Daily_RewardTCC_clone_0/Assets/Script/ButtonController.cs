using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public DiceManager diceManager; 
    public TMP_Text diceCountText;  

    private void Start()
    {
        UpdateDiceCount();
    }

    public void AddDice()
    {
        diceManager.CreateDice(1);
        UpdateDiceCount();
    }

    public void RemovedDice()
    {
        diceManager.RemoveDice();
        UpdateDiceCount();
    }

    public void UpdateDiceCount()
    {
        int diceCount = diceManager.diceList.Count;
        diceCountText.text = diceCount.ToString();
    }

}