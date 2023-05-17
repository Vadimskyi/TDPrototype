using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float damage;
	public Enemy target;

	public void SetData(Enemy target, float damage)
	{
		this.target = target;
		this.damage = damage;
	}

	public virtual void TargetReached()
	{
		if(target)
			target.TakeDamage(damage);
		Destroy(gameObject);
	}
}
