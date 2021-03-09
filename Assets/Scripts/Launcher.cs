using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }

    // Callback called by Photon on successful connection to the master server
	public override void OnConnectedToMaster()
	{
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
	}

	public override void OnJoinedLobby()
	{
        Debug.Log("Joined Lobby");
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
