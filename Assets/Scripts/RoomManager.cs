using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoomManager : MonoBehaviourPunCallbacks
{
	public static RoomManager Instance;
	Hashtable hash;

	void Awake()
	{
		Debug.Log("RoomManager Awake");
		if (Instance) // Checks if another RoomManager exists
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
		Instance = this;
		hash = new Hashtable();
		hash.Add("bots", 0);
		PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
	}

	private void Update()
	{
		Debug.Log(PhotonNetwork.LevelLoadingProgress);
	}

	public override void OnEnable()
	{
		base.OnEnable();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
	{
		if (scene.buildIndex == 1 || scene.buildIndex == 2 || scene.buildIndex == 3)
		{
			PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
		}
		else if (scene.buildIndex == 0)
		{
			PlayerMovement[] p_ = FindObjectsOfType<PlayerMovement>();
			PlayerManager[] pm = FindObjectsOfType<PlayerManager>();
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	public void ReturnToRoomMenu()
	{
		PhotonNetwork.LoadLevel(0);
	}

}
