using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	public SpriteRenderer spriteRenderer;
	public float speed;
	public float health;
	public Vector3 targetPosition;

	private Healthbar _healthbar;
	private Queue<Vector3> _path;
	private float _maxHealth;

	private void Start()
	{
		_healthbar = GetComponentInChildren<Healthbar>();
		_maxHealth = health;
	}

	public void TakeDamage(float damage)
	{
		health -= damage;
		if(_healthbar) _healthbar.UpdatHealth(health, _maxHealth);
		if(health <= 0)
		{
			EnemyDestroyedEvent.Invoke(this);
			Destroy(gameObject);
		}
	}

	public void MoveTowardsTarget(float deltaTime)
	{
		if(Vector3.Distance(transform.position, targetPosition) < 0.1f)
		{
			if(_path.Count < 1) return;
			targetPosition = _path.Dequeue();
		}

		var direction = (targetPosition - transform.position).normalized;
		transform.position += direction * speed * Time.deltaTime;

		var rotationAngle = Mathf.Atan2(direction.x, direction.y) * -1 * Mathf.Rad2Deg;
		spriteRenderer.transform.rotation = Quaternion.Slerp(spriteRenderer.transform.rotation, Quaternion.AngleAxis(rotationAngle, Vector3.forward), Time.deltaTime * 10);
	}

	public void SetPath(Vector2[] path)
	{
		_path = new Queue<Vector3>(path.Select(s => new Vector3(s.x, s.y, 0)));
		targetPosition = _path.Dequeue();
	}
}
