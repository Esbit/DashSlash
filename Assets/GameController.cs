using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject CanvasUI;
    public Intro SceneLoader;
    DontDestroyNumberPLayers players;
    public int AmountCharacters = 0;

    public static GameController Instance { get; private set; }
    private void Awake()
    {
        players = FindObjectOfType<DontDestroyNumberPLayers>();
        AmountCharacters = players.players;
        Debug.Log(AmountCharacters);
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void CharacterKilled()
    {
        AmountCharacters--;
        if (AmountCharacters <= 1)
        {
            CanvasUI.SetActive(true);
            SceneLoader.ShowWin();
        }
    }


}
