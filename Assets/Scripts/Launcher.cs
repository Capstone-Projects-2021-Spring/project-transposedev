using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
	public static Launcher Instance;

    [SerializeField] TMP_InputField roomNameInputField;
	[SerializeField] TMP_Text errorText;
	[SerializeField] TMP_Text roomNameText;
	[SerializeField] Transform roomListContent;
	[SerializeField] GameObject roomListItemPrefab;
	[SerializeField] Transform playerListContent;
	[SerializeField] GameObject playerListItemPrefab;

	void Awake()
	{
		Instance = this;
	}

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

		// TODO place holder for nickname/username system
		PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
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

	public void JoinRoom(RoomInfo info)
	{
		PhotonNetwork.JoinRoom(info.Name);
		MenuManager.Instance.OpenMenu("Loading");
	}

	public override void OnJoinedRoom()
	{
		roomNameText.text = PhotonNetwork.CurrentRoom.Name;
		MenuManager.Instance.OpenMenu("Room");

		Player[] players = PhotonNetwork.PlayerList;

		for (int i = 0; i < players.Length; i++)
		{
			Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
		}
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

	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		foreach (Transform trans in roomListContent)
		{
			Destroy(trans.gameObject);
		}
		for (int i = 0; i < roomList.Count; i++)
		{
			Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
		}
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
	}

	public void StartGame()
	{

	}
}
