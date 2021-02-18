using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainShot : MonoBehaviour
{
    public static float remainingShots = 10;
    public Transform shotText;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("pressed mouse button");
            if (remainingShots > 0)
            {
                remainingShots-=1;
            }
            if (remainingShots <= 0)
            {
                Debug.Log("Reloading...");
                remainingShots=MaxAmmo.MaxAmmoAmount;
            }
        }
        shotText.GetComponent<Text>().text = remainingShots.ToString();
    }
}
