using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField roomNameInputField;
	[SerializeField] TMP_Text errorText;
	[SerializeField] TMP_Text roomNameText;

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
        MenuManager.Instance.OpenMenu("Main");
        Debug.Log("Joined Lobby");
	}

    public void CreateRoom()
	{
        if (string.IsNullOrEmpty(roomNameInputField.text))
		{
            return;
		}
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("Loading");
	}

	public override void OnJoinedRoom()
	{
		roomNameText.text = PhotonNetwork.CurrentRoom.Name;
		MenuManager.Instance.OpenMenu("Room");
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		errorText.text = "Room creation failed: " + message;
		MenuManager.Instance.OpenMenu("Error");
	}

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
		MenuManager.Instance.OpenMenu("Loading");
	}

	public override void OnLeftRoom()
	{
		MenuManager.Instance.OpenMenu("Main");
	}
}
