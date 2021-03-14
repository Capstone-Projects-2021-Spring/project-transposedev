using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public void DestroyTarget(GameObject target)
	{
        transform.gameObject.GetComponent<AudioSource>().Play();
        Destroy(target);
	}

    public void RespawnTarget()
	{

	}
}
