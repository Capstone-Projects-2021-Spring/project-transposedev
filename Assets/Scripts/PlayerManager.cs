using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
	GameObject controller;
	[SerializeField]private int kills;
    [SerializeField]private int deaths;
    

	void Awake()
	{
        PV = GetComponent<PhotonView>();
	}

	// Start is called before the first frame update
	void Start()
    {
        if (PV.IsMine)
		{
			CreateController();
		}
    }

	void CreateController()
	{
		// Instantiate player controller
		Transform spawnPoint = SpawnManager.Instance.GetSpawnPoint();
		controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnPoint.position, spawnPoint.rotation, 0, new object[] { PV.ViewID });
	}

	public void Die()
	{
		PhotonNetwork.Destroy(controller);
		CreateController();
        deaths++;
        Debug.Log("I just died, current deaths: " + DeathCount());
	}

    public void AddKill()
    {
        kills++;
    }

    public int KillCount()
    {
        return kills;
    }

    public int DeathCount()
    {
        return deaths;
    }
}
