using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregadorCena : MonoBehaviour
{
    public string daily_cena;
    public string play_cena;
    public string menu_cena;
    public string sceneID;
    public void Play()
    {
        SceneManager.LoadScene(play_cena);
    }
    public void NextScene()
    {
        SceneManager.LoadScene(daily_cena);
    }

    public void Quit()
    {
        Debug.Log("Quitting, please wait...");
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene(menu_cena);
    }
}
