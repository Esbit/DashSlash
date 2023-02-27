using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSwitcher : MonoBehaviour
{
    int index = 0;

    public GameObject Join1;
    public GameObject Join2;
    public GameObject Join3;
    public GameObject Join4;
    public Transform[] spawnLocations;
    public GameObject[] players;
    PlayerInputManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<PlayerInputManager>();
        manager.playerPrefab = players[0];
        index++;
    }

    public void SwitchNextSpawnPlayer(PlayerInput input)
    {
        var prevIndex = 0;
        if (index == 4)
        {
            prevIndex = index;
            index = 3;
        }
        manager.playerPrefab = players[index];
        OnPlayerJoined(input);
        if(index == 1)
            Join1.SetActive(false);
        if (index == 2)
            Join2.SetActive(false);
        if (index == 3)
            Join3.SetActive(false);
        if (prevIndex == 4)
            Join4.SetActive(false);
        if (index < 5)
        {
            index++;
        }
    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        //Debug.Log("PlayerInput ID: " + playerInput.playerIndex);

        // Set the player ID, add one to the index to start at Player 1
        playerInput.gameObject.GetComponent<PlayerDetails>().playerID = playerInput.playerIndex + 1;

        // Set the start spawn position of the player using the location at the associated element into the array.
        playerInput.gameObject.GetComponent<PlayerDetails>().startPos = spawnLocations[playerInput.playerIndex].position;
    }
}
