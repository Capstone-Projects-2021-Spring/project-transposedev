using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
    //private NetworkManagerLobby networkManager = null;
    //public GameObject landingPagePanel = null;
    public InputField ipAddressInputField = null;
    public Button JoinButton = null;

    private void OnEnable()
    {
        /*
        NetworkManagerLobby.OnClientConnected += HandleClientConnected;
        NetworkManagerLobby.OnClientDisConnected += HandleClientDisConnected;
        */
    }
    private void OnDisable()
    {
        /*
        NetworkManagerLobby.OnClientConnected -= HandleClientConnected;
        NetworkManagerLobby.OnClientDisConnected -= HandleClientDisConnected;
        */
    }
    public void JoinLobby()
    {
        string ipAddress = ipAddressInputField.text;
        //newworkManager.networkAddress = ipAddress;
        //networkManager.StartClient();
        JoinButton.interactable = false;
    }
    private void HandleClientConnected()
    {
        JoinButton.interactable = true;
        gameObject.SetActive(false);
        //landingPagePanel.SetActive(false);
    }
    private void HandleClientDisConnected()
    {
        JoinButton.interactable = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
