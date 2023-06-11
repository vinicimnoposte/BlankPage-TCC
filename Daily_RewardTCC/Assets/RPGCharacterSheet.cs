using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RPGCharacterSheet : MonoBehaviour
{
    // Atributos do personagem
    public int strength;
    public int dexterity;
    public int intelligence;
    public int charisma;

    

    // Pontos de atributo disponíveis para distribuir
    public int attributePoints = 22;

    // Elementos da UI
    //public InputField strengthText;
    //public InputField intelligenceText;
    public TMP_Text strTxt;
    public TMP_Text intTxt;
    public Text dexterityText;
    public Text charismaText;
    public Text attributePointsText;
    //public InputField str, dex, intel, chars, att;

    public GameObject menuDistribuicao;


    public void Start()
    {
       
    }
    void Update()
    {
        // Atualiza a UI com os valores dos atributos e pontos disponíveis
        //strTxt.text = strengthText.text;
       // strength = int.Parse(strengthText.text);
       // strTxt.text = strength.ToString();
       // intelligence = int.Parse(intelligenceText.text);
       // intTxt.text = intelligence.ToString();
       // dexterityText.text = dexterity.ToString();
       // charismaText.text = charisma.ToString();
       // attributePointsText.text = attributePoints.ToString();
       // CheckStrenght();
    }

    void LockInput(InputField input)
    {
        if (input.text.Length > 0)
        {
            Debug.Log("Text has been entered");
        }
        else if (input.text.Length == 0)
        {
            Debug.Log("Main Input Empty");
        }
    }

    public void CheckCurrentPoints()
    {
        if(attributePoints <= 0)
        {
            menuDistribuicao.SetActive(false);
        }
    }

    // Adiciona um ponto ao atributo de força
    public void AddStrength()
    {
        if (attributePoints > 0)
        {
            strength++;
            attributePoints--;
        }
    }

    //public void CheckStrenght()
    //{
    //    //TODO
    //    if (strength > 15)
    //    {
    //        for (int i = 0; i < attributePoints; i++)
    //        {
    //            strength--;
    //            if (attributePoints - strength == 0)
    //            {
    //                break;
    //            }
    //        }
    //    }
    //}

    // Adiciona um ponto ao atributo de destreza
    public void AddDexterity()
    {
        if (attributePoints > 0)
        {
            dexterity++;
            attributePoints--;
        }
    }

    // Adiciona um ponto ao atributo de inteligência
    public void AddIntelligence()
    {
        if (attributePoints > 0)
        {
            intelligence++;
            attributePoints--;
        }
    }

    // Adiciona um ponto ao atributo de carisma
    public void AddCharisma()
    {
        if (attributePoints > 0)
        {
            charisma++;
            attributePoints--;
        }
    }
}
