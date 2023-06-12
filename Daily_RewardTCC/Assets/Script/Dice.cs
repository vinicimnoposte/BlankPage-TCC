using System.Collections;
using UnityEngine;
using TMPro;
using System;



public class Dice : MonoBehaviour
{
    public SpriteRenderer rend;
    public Sprite[] diceSides;
    public ParticleSystem acertoParticleSystem;
    public ParticleSystem erroParticleSystem;
    public ParticleSystem criticoParticleSystem;

    private bool rolling = false;
    private int diceValue = 0;

    public event Action OnDiceRoll;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        LoadDiceSides();

        acertoParticleSystem = GameObject.Find("Acerto").GetComponent<ParticleSystem>();
        erroParticleSystem = GameObject.Find("Erro").GetComponent<ParticleSystem>();
        criticoParticleSystem = GameObject.Find("Critico").GetComponent<ParticleSystem>();
    }

    public void RollTheDice()
    {
        StartCoroutine(RollCoroutine());
    }

    private IEnumerator RollCoroutine()
    {
        rolling = true;
        int rolls = UnityEngine.Random.Range(10, 20);

        for (int i = 0; i < rolls; i++)
        {
            int randomDiceSide = UnityEngine.Random.Range(0, diceSides.Length);
            rend.sprite = diceSides[randomDiceSide];

            yield return new WaitForSeconds(0.1f);
        }

        diceValue = UnityEngine.Random.Range(1, 11);
        rend.sprite = diceSides[diceValue - 1];

        if (diceValue >= 5 && diceValue < 10)
        {
            PlayParticleEffect(acertoParticleSystem, false);
            erroParticleSystem.Stop();
            criticoParticleSystem.Stop();
        }
        else if (diceValue < 5)
        {
            PlayParticleEffect(erroParticleSystem, false);
            acertoParticleSystem.Stop();
            criticoParticleSystem.Stop();
        }
        else if (diceValue == 10)
        {
            PlayParticleEffect(criticoParticleSystem, true);
            acertoParticleSystem.Stop();
            erroParticleSystem.Stop();
        }

        rolling = false;
        OnDiceRoll?.Invoke();
    }

    private void LoadDiceSides()
    {
        diceSides = new Sprite[10];

        for (int i = 0; i < diceSides.Length; i++)
        {
            string spritePath = "DiceSides/side" + (i + 1);
            diceSides[i] = Resources.Load<Sprite>(spritePath);

            if (diceSides[i] == null)
            {
                Debug.LogError("Failed to load dice side: " + spritePath);
            }
        }
    }

    private void PlayParticleEffect(ParticleSystem particleSystem, bool isCritical)
    {
        if (particleSystem != null)
        {
            particleSystem.Play();
            StartCoroutine(StopParticleEffect(particleSystem));
        }
        else
        {
            Debug.LogError("Particle system is null.");
        }
    }

    private IEnumerator StopParticleEffect(ParticleSystem particleSystem)
    {
        yield return new WaitForSeconds(2f);
        particleSystem.Stop();
    }

    public int GetDiceValue()
    {
        return diceValue;
    }

    public bool IsRolling()
    {
        return rolling;
    }
}
