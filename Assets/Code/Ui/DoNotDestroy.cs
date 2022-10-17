                                        using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroy : MonoBehaviour
{
    [SerializeField]
    //GameObject music;
    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
        if (musicObj.Length > 1 || ("JUEGO" == SceneManager.GetActiveScene().name))
        {
            //music.SetActive(false);
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject); 
    }
}