using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxAmmo : MonoBehaviour
{
    public static float MaxAmmoAmount = 10;
    public Transform MaxAmmoText;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        MaxAmmoText.GetComponent<Text>().text = MaxAmmoAmount.ToString();
    }
}
