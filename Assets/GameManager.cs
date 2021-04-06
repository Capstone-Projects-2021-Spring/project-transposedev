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
		SpawnHazards();
	}

	void SpawnHazards()
	{
		PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "ExplosiveBarrel"), Vector3.zero, Quaternion.identity);
	}

	public void DestroyHazard(GameObject hazard)
	{
		PhotonNetwork.Destroy(hazard);
		StartCoroutine(CoolDown());
		SpawnHazards();
	}

	public override void OnLeftRoom()
	{
		SceneManager.LoadScene(0);
	}

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
	}

	IEnumerator CoolDown()
	{
		Debug.Log("should be waiting");
		yield return new WaitForSecondsRealtime(500);
	}
}
