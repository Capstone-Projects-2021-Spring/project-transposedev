using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviourPunCallbacks
{
	public static GameManager Instance;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		//SpawnHazards();
		SpawnAI();
	}

	void SpawnHazards()
	{
		PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "ExplosiveBarrel"), Vector3.zero, Quaternion.identity);
	}

	void SpawnAI()
	{
		PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "AI"), Vector3.zero, Quaternion.identity);
	}

	public void DestroyHazard(GameObject hazard)
	{
		PhotonNetwork.Destroy(hazard);
		Invoke("SpawnHazards", 3);
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
