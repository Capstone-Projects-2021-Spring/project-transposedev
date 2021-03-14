using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
	TargetManager targetManager;
	[SerializeField] GameObject target;

	const float maxHealth = 100f;
	float currentHealth = maxHealth;

	void Start()
	{
		targetManager = GameObject.Find("TargetManager").GetComponent<TargetManager>();
	}

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
		targetManager.DestroyTarget(target);
        FindObjectOfType<RuleSet>().TargetDestroyed();
	}
}
