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
    DontDestroyNumberPLayers ActivePlayers;
    void Start()
    {
        ActivePlayers = FindObjectOfType<DontDestroyNumberPLayers>();
        if (ActivePlayers.players == 1)
        {
            player1.SetActive(true);
            dummy.SetActive(true);
        }
        else if (ActivePlayers.players == 2)
        {
            player1.SetActive(true);
            player2.SetActive(true);
        }
        else if (ActivePlayers.players == 3)
        {
            player1.SetActive(true);
            player2.SetActive(true);
            player3.SetActive(true);
        }
        else if (ActivePlayers.players == 4)
        {
            player1.SetActive(true);
            player2.SetActive(true);
            player3.SetActive(true);
            player1.SetActive(true);
        }
    }
}
