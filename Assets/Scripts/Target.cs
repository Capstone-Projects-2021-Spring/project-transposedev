using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
	const float maxHealth = 100f;
	float currentHealth = maxHealth;

	void Update()
	{
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	public void TakeDamage(float damage)
	{
		currentHealth -= damage;
		Debug.Log("Damaged! Health at: " + currentHealth);
	}

	void Die()
	{
		
	}
}
