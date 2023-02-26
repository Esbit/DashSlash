using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button ready;

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Submit")) && SceneManager.GetActiveScene().name == "CHARACTERS")
        {
            PlayGame();
        }
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Submit")) && SceneManager.GetActiveScene().name == "COMING SOON")
        {
            Main();
        }
    }
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
