using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyNumberPLayers : MonoBehaviour
{
    int players = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayersActiive()
    {
        players++;
        Debug.Log("Nunber of Players Active: " + players);
    }
}
