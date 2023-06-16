using UnityEngine;

public class Dice : MonoBehaviour
{
    public SpriteRenderer rend;
    public Sprite[] diceSides;
    public GameObject acertoParticlePrefab;
    public GameObject erroParticlePrefab;
    public GameObject criticoParticlePrefab;

    private bool rolling = false;
    private int diceValue = 0;

    public event System.Action OnDiceRoll;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        LoadDiceSides();
    }

    public void RollTheDice()
    {
        StartCoroutine(RollCoroutine());
    }

    private System.Collections.IEnumerator RollCoroutine()
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

        if (diceValue >= 6 && diceValue < 10)
        {
            PlayParticleEffect(acertoParticlePrefab);
        }
        else if (diceValue < 6)
        {
            PlayParticleEffect(erroParticlePrefab);
        }
        else if (diceValue == 10)
        {
            PlayParticleEffect(criticoParticlePrefab);
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

    private void PlayParticleEffect(GameObject particlePrefab)
    {
        if (particlePrefab != null)
        {
            GameObject particleObject = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();

            if (particleSystem != null)
            {
                particleSystem.Play();
                StartCoroutine(StopParticleEffect(particleSystem, particleObject));
            }
            else
            {
                Debug.LogError("Particle system component not found in the particle prefab.");
            }
        }
        else
        {
            Debug.LogError("Particle prefab is null.");
        }
    }

    private System.Collections.IEnumerator StopParticleEffect(ParticleSystem particleSystem, GameObject particleObject)
    {
        yield return new WaitForSeconds(2f);
        particleSystem.Stop();
        Destroy(particleObject, particleSystem.main.duration);
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