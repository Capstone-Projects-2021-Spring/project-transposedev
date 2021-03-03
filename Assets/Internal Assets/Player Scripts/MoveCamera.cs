using UnityEngine;

// allows a player to look around the in-game world
public class MoveCamera : MonoBehaviour {

    // used to represent the in-game player model
    public Transform player;

    // updates the position of the cmaera relative to the player's direction 
    void Update() {
        transform.position = player.transform.position;
    }
}
