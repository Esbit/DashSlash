using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField]
    float time = 30f;
    [SerializeField]
    string nombre;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "JUEGO" || SceneManager.GetActiveScene().name == "COMING SOON")
            return;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(nombre);
    }

    public void ShowWin()
    {
        StartCoroutine(Wait());
    }
}
