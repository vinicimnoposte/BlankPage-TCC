using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceManager : MonoBehaviour
{
    public GameObject diceContainer;
    public GameObject dicePrefab;
    public int maxDiceCount = 9;
    public GameObject[] ancoraPoints;
    public TextMeshProUGUI sumText;
    public TextMeshProUGUI acertosText;
    public TextMeshProUGUI errosText;
    //public float destroyInitialDiceDelay = 3f; // Tempo de espera antes de destruir os dados iniciais

    private int acertosCount = 0;
    private int errosCount = 0;
    private bool isRolling = false;
    private bool rollingInProgress = false;
    private bool additionalDiceRollingInProgress = false;

    public List<Dice> diceList = new List<Dice>();
    private Vector3 diceSpacing;
    private float distance;
    private Coroutine rollingCoroutine; // Usado para controlar as rolagens
    public ButtonController bC;
    public GameObject botaoAddDice;
    //private Coroutine destroyInitialDiceCoroutine; // Usado para destruir os dados iniciais

    private void Start()
    {
        diceSpacing = new Vector3(3f, 3f, 3f);
        bC = GameObject.FindGameObjectWithTag("BUTAO").GetComponent<ButtonController>();
    }

    public void Update()
    {
        if(diceList.Count >= 9)
        {
            botaoAddDice.SetActive(false);
        }
        else
        {
            botaoAddDice.SetActive(true);
        }
    }

    public void CreateDice(int count)
    {
        if (rollingInProgress)
            return;

        count = Mathf.Clamp(count, 1, maxDiceCount);

        StartCoroutine(RollAndAddDice(count));
    }

    private IEnumerator RollAndAddDice(int count)
    {
        rollingInProgress = true;

        for (int i = 0; i < count; i++)
        {
            if (diceList.Count >= maxDiceCount)
            {
                Dice removedDice = diceList[diceList.Count]; // Pega o dado mais novo >:)
                diceList.RemoveAt(diceList.Count);
                Destroy(removedDice.gameObject);

                removedDice.OnDiceRoll -= UpdateSum;
                removedDice.OnDiceRoll -= CountAcertos;
                removedDice.OnDiceRoll -= CountErros;
                removedDice.OnDiceRoll -= CheckRollingState;

                bC.UpdateDiceCount();
            }

            Vector3 position = GetNextDicePosition();
            GameObject diceObject = Instantiate(dicePrefab, position, Quaternion.identity, diceContainer.transform);

            Dice dice = diceObject.GetComponent<Dice>();
            diceList.Add(dice);

            dice.OnDiceRoll += UpdateSum;
            dice.OnDiceRoll += CountAcertos;
            dice.OnDiceRoll += CountErros;
            dice.OnDiceRoll += CheckRollingState;

            yield return new WaitUntil(() => !dice.IsRolling());
        }

        rollingInProgress = false;

        // Inicia o coroutine para destruir os dados iniciais após o tempo especificado
        //destroyInitialDiceCoroutine = StartCoroutine(DestroyInitialDiceAfterDelay(destroyInitialDiceDelay));
    }
    //CTRL + K + C
    //private IEnumerator DestroyInitialDiceAfterDelay(float delay)
    //{
    //    yield return new WaitForSeconds(delay);

    //    // Destrói os dados iniciais
    //    for (int i = 0; i < maxDiceCount; i++)
    //    {
    //        if (i < diceList.Count)
    //        {

    //            Dice removedDice = diceList[i];
    //            diceList.RemoveAt(i);
    //            Destroy(removedDice.gameObject);

    //            removedDice.OnDiceRoll -= UpdateSum;
    //            removedDice.OnDiceRoll -= CountAcertos;
    //            removedDice.OnDiceRoll -= CountErros;
    //            removedDice.OnDiceRoll -= CheckRollingState;


    //        }
    //    }

    //    // Inicia o coroutine para rolar os dados adicionais
    //    rollingCoroutine = StartCoroutine(RollAdditionalDice());
    //}

    //private IEnumerator RollAdditionalDice()
    //{
    //    additionalDiceRollingInProgress = true;

    //    foreach (Dice dice in diceList)
    //    {
    //        dice.RollTheDice();
    //        yield return new WaitUntil(() => !dice.IsRolling());
    //    }

    //    additionalDiceRollingInProgress = false;
    //}

    private Vector3 GetNextDicePosition()
    {
        int diceCount = diceList.Count;
        Vector3 position = Vector3.zero;

        if (diceCount < ancoraPoints.Length)
        {
            position = ancoraPoints[diceCount].transform.position;
        }
        else
        {
            //int anchorIndex = diceCount % ancoraPoints.Length;
            //position = ancoraPoints[anchorIndex].transform.position;
            //return new Vector3(0, 0, 0);
        }

        return position;
    }

    public void RemoveDice()
    {
        if (diceList.Count >= 1)
        {
            Dice removedDice = diceList[0]; // Pega o dado mais antigo
            diceList.RemoveAt(0);
            Destroy(removedDice.gameObject);

            removedDice.OnDiceRoll -= UpdateSum;
            removedDice.OnDiceRoll -= CountAcertos;
            removedDice.OnDiceRoll -= CountErros;
            removedDice.OnDiceRoll -= CheckRollingState;

            UpdateSum();
            CountAcertos();
            CountErros();
            bC.UpdateDiceCount();
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
