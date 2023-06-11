using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public DiceManager diceManager; // Refer�ncia ao DiceManager
    public TMP_Text diceCountText; // Refer�ncia ao Text para exibir a contagem de dados

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

    private void UpdateDiceCount()
    {
        int diceCount = diceManager.diceList.Count;
        diceCountText.text = diceCount.ToString();
    }

}
