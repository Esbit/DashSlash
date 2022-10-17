using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("JUEGO");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Main()
    {
        SceneManager.LoadScene("MENU");
    }
    public void Cargando()
    {
        SceneManager.LoadScene("CARGANDO");
    }

    public void Characters()
    {
        SceneManager.LoadScene("CHARACTERS");
    }

    public void GameSelector()
    {
        SceneManager.LoadScene("Game Selector");
    }

    public void Shop()
    {
        SceneManager.LoadScene("SHOP");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("HOW TO PLAY");
    }

    public void Credits()
    {
        SceneManager.LoadScene("CREDITS");
    }

    public void Soon()
    {
        SceneManager.LoadScene("COMING SOON");
    }
}
