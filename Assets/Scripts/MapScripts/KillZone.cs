using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {

        if(other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            other.gameObject.GetComponent<PlayerMovement>().Die();
        }

        if (other.gameObject.GetComponent<PlayerMovement_Grappler>() != null)
        {
            other.gameObject.GetComponent<PlayerMovement_Grappler>().Die();
        }
    }

}
