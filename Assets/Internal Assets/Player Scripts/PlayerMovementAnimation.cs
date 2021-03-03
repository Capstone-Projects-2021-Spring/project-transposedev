using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAnimation : MonoBehaviour
{
    Animator anim;
    int forwards = 0;
    int backwards = 0;
    int sideways = 0;
    int jump = 0;

    /*
        Animation[i] are all the animations in the player animations folder where 'i' is the 'condition' of the Animator

        Animation[0] = Idle
        Animation[1] = Running
        Animation[2] = Walking Backwards
        Animation[3] = Walking Sideways
        Animation[4] = Jumping
    */

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        forwardsAnimation();
        backwardsAnimation();
        sidewaysAnimation();
        jumpAnimation();
    }

    void forwardsAnimation()
    {
        // Determines if the player is running forward and to use the 'Running' animation
        if (Input.GetAxis("Vertical") == 1)
        {
            forwards = 1;
            anim.SetInteger("condition", 1);
        }

        // If the player stops, us the 'Idle' animation
        if (Input.GetAxis("Vertical") == 0)
        {
            if (forwards == 1)
            {
                anim.SetInteger("condition", 0);
                forwards = 0;
            }
        }
    }

    void backwardsAnimation()
    {
        // Determines if the player is running forward and to use the 'Running' animation
        if (Input.GetAxis("Vertical") == -1)
        {
            backwards = 1;
            anim.SetInteger("condition", 2);
        }

        // If the player stops, us the 'Idle' animation
        if (Input.GetAxis("Vertical") == 0)
        {
            if (backwards == 1)
            {
                anim.SetInteger("condition", 0);
                forwards = 0;
            }
        }
    }

    void sidewaysAnimation()
    {
        // Determines if the player is running forward and to use the 'Running' animation
        if (Input.GetAxis("Horizontal") != 0)
        {
            sideways = 1;
            anim.SetInteger("condition", 3);
        }

        // If the player stops, us the 'Idle' animation
        if (Input.GetAxis("Horizontal") == 0)
        {
            if (sideways == 1)
            {
                anim.SetInteger("condition", 0);
                sideways = 0;
            }
        }
    }

    void jumpAnimation()
    {
        // Determines if the player is running forward and to use the 'Running' animation
        if (Input.GetButton("Jump"))
        {
            jump = 1;
            anim.SetInteger("condition", 4);
        }

        // If the player stops, us the 'Idle' animation
        if (!Input.GetButton("Jump"))
        {
            if (jump == 1)
            {
                anim.SetInteger("condition", 0);
                jump = 0;
            }
        }
    }

}
