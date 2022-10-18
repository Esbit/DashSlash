using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Setup")]
    [SerializeField]
    private int MaxPlayers = 2;

    private List<PlayerConfiguration> playerConfigs;

    public static PlayerManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("Trying to create another instance!");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
    }

    public void SetPlayerCharacter(int index, Material color)
    {
        playerConfigs[index].CharMat = color;
        
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player Joined " + pi.playerIndex);
        //if(!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        //{
            //pi.transform.SetParent(transform);
            //playerConfigs.Add(new PlayerConfiguration(pi));
        //}
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }
    public Material CharMat { get; set; }
    public Mesh CharMesh{ get; set; }
}
