using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Opcao B"); // Substitua "NomeDaCenaDoJogo" pelo nome da cena que deseja carregar
    }

    public void QuitGame()
    {
        // Fechar o jogo
        Application.Quit();
    }
}
