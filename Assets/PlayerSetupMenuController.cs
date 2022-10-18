using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{
    [Header("Character Setup")]
    [SerializeField]
    private GameObject charSelector; //assign your gameobject from the inspector here
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private InputActionReference uINavigation;
    [Range(0, 20)]
    [SerializeField]
    private int selectedBinding;
    [SerializeField]
    private InputBinding inputBinding;
    [SerializeField]
    private Mesh[] meshList;

    private int bindingIndex;
    private string actionName;

    // Inputs
    private Vector2 m_UINavigation;
    private Vector2 movementInput = Vector2.zero;

    // Mesh Swapping
    [Range(0, 3)]
    [SerializeField]
    private int SkinIndex = 0;

    private SkinnedMeshRenderer charRender;
    private MeshFilter charFilter;
    private Mesh mesh;


    // Only continue if valid
    private void OnValidate()
    {
        if(uINavigation == null)
            return;

        GetBindingInfo();
    }

    // Trying to get UI Binding information
    private void GetBindingInfo()
    {
        if(uINavigation.action != null)
            actionName = uINavigation.action.name;
        if(uINavigation.action.bindings.Count > selectedBinding)
        {
            inputBinding = uINavigation.action.bindings[selectedBinding];
            bindingIndex = selectedBinding;
        }
    }

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        Debug.Log(uINavigation.action.bindings);
    }

    // Called once per frame
    void Update()
    {
        if (SkinIndex != null)
        {
            mesh = meshList[SkinIndex];
            charRender = charSelector.GetComponent<SkinnedMeshRenderer>();
            charRender.sharedMesh = mesh;
        }
        Debug.Log("Skin Name: " + mesh.name);
        Debug.Log("");
    }

    // set CharacterMesh
    public void SetMesh()
    {
        mesh = meshList[SkinIndex];
        charRender = charSelector.GetComponent<SkinnedMeshRenderer>();
        charRender.sharedMesh = mesh;
    }
}
