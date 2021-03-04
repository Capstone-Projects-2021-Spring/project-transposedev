using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAnimation : MonoBehaviour
{
    /*****************/
    /*   VARIABLES   */
    /*****************/

    Animator anim;
    int forwards = 0;
    int backwards = 0;
    int sideways = 0;
    int jump = 0;

    /*
        Animation[i] are all the animations in the player animations 
        folder where 'i' is the 'condition' of the Animator.

        Animation[0] = Idle
        Animation[1] = Running
        Animation[2] = Walking Backwards
        Animation[3] = Walking Sideways
        Animation[4] = Jumping

        Should add more animations and transitions for the player's model using this script 
        to write out the logic and the 'player_movement_animator' Animator object in Unity.
    */

/**********************************************************************************************************/

    /*********************/
    /*   UNITY METHODS   */
    /*********************/

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

/**********************************************************************************************************/

    /*********************/
    /*   OTHER METHODS   */
    /*********************/

    // Determines if the player is running forward and to use the 'Running' animation
    void forwardsAnimation()
    {
        // if the 'Forward' button/key is being pressed...
        if (Input.GetAxis("Vertical") == 1)
        {
            // running forward animation plays
            forwards = 1;
            anim.SetInteger("condition", 1);
        }

        // If the player stops moving and the last animation played was 'Running'...
        if (Input.GetAxis("Vertical") == 0)
        {
            // if the player's last animation was running forward...
            if (forwards == 1)
            {
                // transition back from the 'Running' animation to the 'Idle' animation
                anim.SetInteger("condition", 0);
                forwards = 0;
            }
        }
    }

    // Determines if the player is moving backwards and to use the 'Walking Backwards' animation
    void backwardsAnimation()
    {
        // if the 'Backward' button/key is being pressed...
        if (Input.GetAxis("Vertical") == -1)
        {
            // moving backwards animation plays
            backwards = 1;
            anim.SetInteger("condition", 2);
        }

        // If the player stops moving and the last animation played was 'Walking Backwards'...
        if (Input.GetAxis("Vertical") == 0)
        {
            if (backwards == 1)
            {
                // transition back from the 'Walking Backwards' animation to the 'Idle' animation
                anim.SetInteger("condition", 0);
                forwards = 0;
            }
        }
    }

    // Determines if the player is moving sideways and to use the 'Walking Sideways' animation
    void sidewaysAnimation()
    {
        // if the 'Left' or 'Right' button/key is being pressed...
        if (Input.GetAxis("Horizontal") != 0)
        {
            // moving sideways animation plays
            sideways = 1;
            anim.SetInteger("condition", 3);
        }
        // If the player stops moving and the last animation played was 'Walking Sideways'...
        if (Input.GetAxis("Horizontal") == 0)
        {
            if (sideways == 1)
            {
                // transition back from the 'Walking Sideways' animation to the 'Idle' animation
                anim.SetInteger("condition", 0);
                sideways = 0;
            }
        }
    }

    // Determines if the player is jumping and to use the 'Jump' animation
    void jumpAnimation()
    {
        // if the 'Jump' button/key is being pressed...
        if (Input.GetButton("Jump"))
        {
            // jump animation plays
            jump = 1;
            anim.SetInteger("condition", 4);
        }

        // If the player stops moving and the last animation played was 'Jump'...
        if (!Input.GetButton("Jump"))
        {
            if (jump == 1)
            {
                // transition back from the 'Jump' animation to the 'Idle' animation
                anim.SetInteger("condition", 0);
                jump = 0;
            }
        }
    }

}
