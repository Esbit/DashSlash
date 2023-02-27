using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyNumberPLayers : MonoBehaviour
{
    public int players = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "COMING SOON")
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayersActiive()
    {
        players++;
        Debug.Log("Nunber of Players Active: " + players);
    }
}
