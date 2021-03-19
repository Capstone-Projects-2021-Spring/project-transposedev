using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour {
    public GameObject Barrel;

    [SerializeField]
    private float explosionRange;

    private void Awake() {
        Barrel.SetActive(true);
        //Explosion.SetActive(false);
    }

    public void Explode() {
        Barrel.SetActive(false);

        Collider[] targets = Physics.OverlapSphere(transform.position, explosionRange);
        foreach (Collider target in targets) {
            if (target.GetComponent<Target>() != null) { // Check it's a target
                target.GetComponent<Target>().TakeDamage(100); // Damage the targets
            }
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Explode();
        }

        
    }


    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
