using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject dummy;
    DontDestroyNumberPLayers _ActivePlayers;

    [HideInInspector]
    public int players;
    void Start()
    {
        _ActivePlayers = FindObjectOfType<DontDestroyNumberPLayers>();
        players = _ActivePlayers.players;
        if (_ActivePlayers.players == 1)
        {
            player1.SetActive(true);
            dummy.SetActive(true);
            players = players + 1;
        }
        else if (_ActivePlayers.players == 2)
        {
            player1.SetActive(true);
            player2.SetActive(true);
        }
        else if (_ActivePlayers.players == 3)
        {
            player1.SetActive(true);
            player2.SetActive(true);
            player3.SetActive(true);
        }
        else if (_ActivePlayers.players == 4)
        {
            player1.SetActive(true);
            player2.SetActive(true);
            player3.SetActive(true);
            player1.SetActive(true);
        }
    }

    public void OnPlayerDeath()
    {
        players--;
    }
}
