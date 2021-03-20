using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : PlayerClass
{
    public override void UseAbility()
    {
        Camera cam = transform.parent.gameObject.GetComponentInChildren<Camera>();

        if(cam == null)
        {
            Debug.Log("No camera found");
            return;
        }

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = cam.transform.position;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Raycast hit at " + hit.point);
            gameObject.transform.position = hit.point + new Vector3(0, 5, 0);
        }
    }

    public override void UseAltAbility()
    {
        throw new System.NotImplementedException();
    }
}
