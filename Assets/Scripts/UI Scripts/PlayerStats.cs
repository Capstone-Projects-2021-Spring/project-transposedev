using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private int HEALTH_MAX = 100;
    [SerializeField]
    private int HEALTH = 100;

    [SerializeField]
    private int ARMOR = 60;
    [SerializeField]
    private int ARMOR_MAX = 15;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseHealth(int healthLost) //method for player losing health
    {
        int armorLost = (int)(System.Math.Ceiling((double)healthLost / 3)); //armor absorbs one third of damage to player health, rounded up

        if (armorLost >= ARMOR) //so you cannot lose more armor than you have
            armorLost = ARMOR;

        healthLost -= armorLost; //armor lessens damage to health;
        ARMOR -= armorLost; //player loses the armor when it absorbs damage;

        if (healthLost >= HEALTH)
            HEALTH = 0; //Player dies
        else
            HEALTH -= healthLost; 
    }

    public void GainHealth(int healthGained) //method for player gaining health
    {
        if ((HEALTH + healthGained) >= HEALTH_MAX) //Cannot gain health over max
            HEALTH = HEALTH_MAX;
        else
            HEALTH += healthGained;
    }

    public void GainArmor(int armorGained) //method for player gaining armor
    {
        if ((ARMOR + armorGained) >= ARMOR_MAX) //cannot gain armor over max
            ARMOR = ARMOR_MAX;
        else
            ARMOR += ARMOR_MAX;
    }

    public int GetHealth()
    {
        return HEALTH;
    }

    public int GetMaxHealth()
    {
        return HEALTH_MAX;
    }

    public int GetArmor()
    {
        return ARMOR;
    }

    public int GetMaxArmor()
    {
        return ARMOR_MAX;
    }
}
