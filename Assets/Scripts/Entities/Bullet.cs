using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float damage;
	public Enemy target;

	public float speed;

	private Vector3 _targetPositionIfDestroyed;
	private bool _scheduleForDestroy;

	public void SetData(Enemy target, float damage)
	{
		this.target = target;
		this.damage = damage;
		_targetPositionIfDestroyed = (target.transform.position - transform.position) * 5;
	}

	private void Update()
	{
		if(_scheduleForDestroy) return;

		var targetPosition = target? target.transform.position : _targetPositionIfDestroyed;

		if(Vector3.Distance(transform.position, targetPosition) < 0.2f)
		{
			_scheduleForDestroy = true;
			OnTargetHit();
		}

		var direction = (targetPosition - transform.position).normalized;
		transform.position += direction * speed * Time.deltaTime;
		float angle = Mathf.Atan2(direction.x, direction.y) * -1 * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		_targetPositionIfDestroyed = target? (target.transform.position - transform.position) * 5 : _targetPositionIfDestroyed;
	}

	private void OnTargetHit()
	{
		if(target)
			target.TakeDamage(damage);
		Destroy(gameObject);
	}
}
