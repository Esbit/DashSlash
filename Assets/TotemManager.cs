using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotemManager : MonoBehaviour
{
    public GameObject totemData;
    public TMP_Text ButtonText;

    private bool active = true;

    public void ActivateData()
    {
        if (active)
        {
            ButtonText.text = "Hide avatars";
            totemData.SetActive(true);
            active = false;

        }
        else
        {
            ButtonText.text = "Show avatars";
            totemData.SetActive(false);
            active = true;
        }
    }
}
