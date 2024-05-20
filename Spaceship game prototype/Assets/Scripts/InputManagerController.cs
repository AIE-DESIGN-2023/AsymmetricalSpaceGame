using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerController : MonoBehaviour
{
    //private List<Gamepad> m_controllers;

    //public GameObject playerPrefab;

   
    public GameObject humanPrefab;
    public GameObject alienPrefab;

    public PlayerInputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        /*for (int j = 0; m_controllers.Count > j; j++)
        {
            GameObject newPlayer = PlayerInput.Instantiate(playerPrefab, controlScheme: "Gamepad", pairWithDevice: m_controllers[j]).gameObject;
        }*/
    }

    public void Swap(bool human)
    {
        if(human)
        {
            GetComponent<PlayerInputManager>().playerPrefab = alienPrefab;
        } else
        {
            GetComponent<PlayerInputManager>().playerPrefab = humanPrefab;
        }
    }

        // Update is called once per frame
        void Update()
    {

    }

    /*void OnPlayerJoin(PlayerInput input)
    {
        if (player1 == null)
        {
            player1 = input.gameObject;
            inputManager.playerPrefab = playerPrefab2;
        } else
        {
            player2 = input.gameObject;
        }
    }*/
}