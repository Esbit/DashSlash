using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuNavigationManager : MonoBehaviour
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        //EventSystem.current.SetSelectedGameObject(null);
        print("Button Highlighted");
    }
    public void OnSelect(BaseEventData eventData)
    {
        //do your stuff when selected
        print("Button Selected");
    }

    // public GameObject pauseFirstButton, optionsFirstButton, optionsClosedButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
