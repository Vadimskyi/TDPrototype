using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed;
	public float health;
	public Vector3 targetPosition;

	public void TakeDamage(float damage)
	{
		health -= damage;
		if(health <= 0)
		{
			EnemyDestroyedEvent.Invoke(this);
			Destroy(gameObject);
		}
	}

	public void MoveTowardsTarget(float deltaTime)
	{
		if(Vector3.Distance(transform.position, targetPosition) < 0.1f)
			return;

		var direction = (targetPosition - transform.position).normalized;
		transform.position += direction * speed * Time.deltaTime;
	}

	public void UpdateTargetPosition(Vector3 targetPosition)
	{
		this.targetPosition = targetPosition;
	}

}
