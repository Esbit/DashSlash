using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PlayerSetupMenuController : MonoBehaviour
{
    [Header("Character Setup")]
    [SerializeField]
    private GameObject characterMesh; //assign your gameobject from the inspector here

    // Inputs
    private PlayerInput playerInput;
    private InputAction inputAction;
    private bool nUp = false;
    private bool nDown = false;
    private bool nLeft = false;
    private bool nRight = false;

    // Mesh Swapping
    //[Range(0, 3)]
    [SerializeField]
    private int SkinIndex = 0;
    [SerializeField]
    private Mesh[] meshList;
    [SerializeField]
    private Material[] matList;

    private SkinnedMeshRenderer charRender;
    private MeshFilter charFilter;
    private Mesh mesh;
    private Material material;

    private void Start()
    {
        SetMesh(SkinIndex);
        SetMat(SkinIndex);
        playerInput = GetComponent<PlayerInput>();
        PlayerControlls playerControlls = new PlayerControlls();

        inputAction = playerControlls.UI.Navigation;

        playerControlls.UI.Enable();
        playerControlls.UI.Submit.performed += OnSubmit;
        playerControlls.UI.Navigation.performed += OnMoved;
    }

    private void OnMoved(InputAction.CallbackContext context)
    {
        Vector2 inputDirection = inputAction.ReadValue<Vector2>();

        //Debug.Log(context);
        if (context.performed && inputDirection != Vector2.zero)
        {
            //Debug.Log(inputDirection);

            if (inputDirection.x > 0.5)
            {
                nLeft = false;
                if (nRight == false)
                {
                    nRight = true;
                    Debug.Log("Right " + nRight);
                }
            }
            else if (inputDirection.x < -0.5)
            {
                nRight = false;
                if (nLeft == false)
                {
                    nLeft = true;
                    Debug.Log("Left " + nLeft);
                }
            }

            if (inputDirection.y > 0.5)
            {
                nDown = false;
                if (nUp == false)
                {
                    nUp = true;
                    SkinIndex = (SkinIndex + 1);
                    if (SkinIndex > (meshList.Length - 1))
                    {
                        SkinIndex = 0;
                        Debug.Log("Bigger");
                    }
                    SetMesh(SkinIndex);
                    SetMat(SkinIndex);
                    //Debug.Log("Up " + nUp);
                }
            }
            else if (inputDirection.y < -0.5)
            {
                nUp = false;
                if (nDown == false)
                {
                    nDown = true;
                    SkinIndex = (SkinIndex - 1);
                    if (SkinIndex < 0)
                    {
                        SkinIndex = (meshList.Length - 1);
                        Debug.Log("Less");
                    }
                    SetMesh(SkinIndex);
                    SetMat(SkinIndex);
                    //Debug.Log("Down " + nDown);
                }
            }
        }
        if (inputDirection.x < 0.5 && inputDirection.x > -0.5)
        {
            nRight = false;
            nLeft = false;
        }
        if (inputDirection.y < 0.5 && inputDirection.y > -0.5)
        {
            nUp = false;
            nDown = false;
        }
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        if (context.performed)
        {
            Debug.Log("Submit - Subbed");
        }
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {

    }

    // Called once per frame
    void FixedUpdate()
    {
        //Vector2 input = playerInput.actions["Navigation"].ReadValue<Vector2>();

        //if (input != Vector2.zero)
        //{
        //    Debug.Log("Axis: " + input);
        //}

        //if (playerInput.actions["Submit"].triggered)
        //{
        //    Debug.Log("Submit");
        //}

        //if (SkinIndex != null)
        //{
        //    mesh = meshList[SkinIndex];
        //    charRender = characterMesh.GetComponent<SkinnedMeshRenderer>();
        //    charRender.sharedMesh = mesh;
        //}
        //Debug.Log("Skin Name: " + mesh.name);
    }

    // set CharacterMesh
    public void SetMesh(int SkinIndex)
    {
        mesh = meshList[SkinIndex];
        charRender = characterMesh.GetComponent<SkinnedMeshRenderer>();
        charRender.sharedMesh = mesh;
    }
    public void SetMat(int SkinIndex)
    {
        material = matList[SkinIndex];
        charRender = characterMesh.GetComponent<SkinnedMeshRenderer>();
        charRender.sharedMaterial = material;
    }
}
