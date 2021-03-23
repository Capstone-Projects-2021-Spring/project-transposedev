using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipGravity : MonoBehaviour
{
    private bool flipped;

    private Vector3 flippedGravity = new Vector3(0, 19.62f, 0);
    private Vector3 normalGravity = new Vector3(0, -19.62f, 0);

    private float coolDownSeconds = 2;
    private float coolDownTimer;

    private void Start()
    {
        flipped = false;
    }

    void Update()
    {
        if(coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }

        if(coolDownTimer < 0)
        {
            coolDownTimer = 0;
        }

        if (Input.GetButton("Fire3") && coolDownTimer == 0)
        {
            flipGravity();
        }
    }

    void flipGravity()
    {
        if (flipped == false)
        {
            flipped = true;
            Physics.gravity = flippedGravity;
        }
        else if (flipped == true)
        {
            flipped = false;
            Physics.gravity = normalGravity;
        }
    }
}
