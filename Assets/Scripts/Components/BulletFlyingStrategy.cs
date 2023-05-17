using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class BulletFlyingStrategy : MonoBehaviour
{
	public float speed;
	public float force;
	private Enemy _target;
	private Bullet _bullet;
	private bool _moveBullet;
	private Vector3 _targetPositionIfDestroyed;

	private void Awake()
	{
		_bullet = GetComponent<Bullet>();
	}

	private void Start()
	{
		_moveBullet = true;
		_target = _bullet.target;
		_targetPositionIfDestroyed = (_target.transform.position - transform.position) * 5;
	}

	private void Update()
	{
		if(!_moveBullet) return;

		var targetPosition = _target? _target.transform.position : _targetPositionIfDestroyed;

		if(Vector3.Distance(transform.position, targetPosition) < 0.2f)
		{
			_bullet.TargetReached();
			_moveBullet = false;
			return;
		}

		var direction = (targetPosition - transform.position).normalized;
		transform.position += direction * speed * Time.deltaTime;
		Rotate(direction);
		_targetPositionIfDestroyed = _target? (_target.transform.position - transform.position) * 5 : _targetPositionIfDestroyed;
	}

	private void Rotate(Vector3 direction)
	{
		float angle = Mathf.Atan2(direction.x, direction.y) * -1 * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
