using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : MonoBehaviourPunCallbacks
{
	public static GameManager Instance;
    GameObject controller;
	int botCount;

	private void Awake()
	{
		Instance = this;
		botCount = (int)PhotonNetwork.CurrentRoom.CustomProperties["bots"];
	}

	private void Start()
	{
		//SpawnHazards();
		for (int i = 0; i < botCount; i++)
		{
			if (PhotonNetwork.IsMasterClient)
				SpawnAI();
		}
	}

	void SpawnHazards()
	{
		PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "ExplosiveBarrel"), Vector3.zero, Quaternion.identity);
	}

    // used to spawn an AI character...
    void SpawnAI()
    {
		// instantiate AI controller
		Transform spawnPoint = SpawnManager.Instance.GetSpawnPoint();
		GameObject bot = PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "AI"), spawnPoint.position, spawnPoint.rotation);
		string id = bot.GetComponent<AIScript>().GetId();
		Hashtable hash = PhotonNetwork.MasterClient.CustomProperties;

		hash.Add(id + "_kills", 0);
		hash.Add(id + "_deaths", 0);
		PhotonNetwork.MasterClient.SetCustomProperties(hash);
	}

	public void DestroyHazard(GameObject hazard)
	{
		PhotonNetwork.Destroy(hazard);
		Invoke("SpawnHazards", 3);
	}

	public void DestroyAI(GameObject AI)
	{
		PhotonNetwork.Destroy(AI);
		Invoke("SpawnAI", 3);
	}

	public override void OnLeftRoom()
	{
		SceneManager.LoadScene(0);
	}

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
	}
}
