using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : Utility
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    public float maxDistance = 100f;
    private SpringJoint joint;
    private Vector3 currentGrapplePosition;

    void Awake() 
    {
        lr = GetComponent<LineRenderer>();
        DrawRope();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    void LateUpdate() 
    {
        DrawRope();
    }

    public override bool Use()
    {
        return StartGrapple();
    }
    bool StartGrapple() 
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable)) 
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;
            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            
            // distance grapple hook will try to keep from grapple point
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
            
            // adjust these values to change how the movement feels for the grappling hook
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;
            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
        return true;
    }

    bool StopGrapple() 
    {
        lr.positionCount = 0;
        Destroy(joint);
        return true;
    }

    void DrawRope() 
    {
        // if the player is not grappling...
        if(!joint) 
        {
            // do not draw the rope
            return;
        }
        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
    
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling() 
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() 
    {
        return currentGrapplePosition;
    }

	public override bool HoldDown()
	{
        return false;
	}

	public override bool Release()
	{
        return StopGrapple();
    }
}
