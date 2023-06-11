using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public SpriteRenderer rend;
    public Sprite[] diceSides;
    public ParticleSystem acertoParticleSystem;
    public ParticleSystem erroParticleSystem;
    public ParticleSystem criticoParticleSystem;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        LoadDiceSides();

        // Procura e atribui as referências dos sistemas de partículas em tempo de execução
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
        // Número de rotações do dado antes de mostrar o resultado final
        int rolls = Random.Range(10, 20);

        for (int i = 0; i < rolls; i++)
        {
            int randomDiceSide = Random.Range(0, diceSides.Length);
            Debug.Log("Random dice side index: " + randomDiceSide);
            rend.sprite = diceSides[randomDiceSide];

            yield return new WaitForSeconds(0.1f);
        }

        int finalSide = Random.Range(1, 11); // Altera o intervalo para 1-10 (10 lados)
        Debug.Log("Final dice side: " + finalSide);
        rend.sprite = diceSides[finalSide - 1];

        // Ativa as partículas com base no resultado do dado
        if (finalSide >= 5 && finalSide < 10)
        {
            PlayParticleEffect(acertoParticleSystem, false);
            erroParticleSystem.Stop();
            criticoParticleSystem.Stop();
        }
        else if (finalSide < 5)
        {
            PlayParticleEffect(erroParticleSystem, false);
            acertoParticleSystem.Stop();
            criticoParticleSystem.Stop();
        }
        else if (finalSide == 10)
        {
            PlayParticleEffect(criticoParticleSystem, true);
            acertoParticleSystem.Stop();
            erroParticleSystem.Stop();
        }
    }

    private void LoadDiceSides()
    {
        diceSides = new Sprite[10]; // Altera o tamanho para 10

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
}
