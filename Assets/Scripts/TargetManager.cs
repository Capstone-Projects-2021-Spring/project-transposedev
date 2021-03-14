using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public int num_targets;

    public void DestroyTarget(GameObject target)
	{
        num_targets--;
        transform.gameObject.GetComponent<AudioSource>().Play();
        Destroy(target);
	}

    public void RespawnTargets()
	{
        FindObjectOfType<GenerateTargets>().spawnTargets();
	}

    private void Update()
    {
        if(num_targets <= 0)
        {
            RespawnTargets();
        }
    }
}
