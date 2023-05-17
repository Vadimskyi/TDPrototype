using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class RocketFlyingStrategy : MonoBehaviour
{
	public float speed;
	public float acceleration;
	public RocketExplosion explosionPrefab;
	private Enemy _target;
	private Bullet _bullet;
	private bool _moveBullet;
	private Vector3 _targetPosition;
	private float _currentSpeed;

	private void Awake()
	{
		_bullet = GetComponent<Bullet>();
	}

	private void Start()
	{
		_moveBullet = true;
		_target = _bullet.target;
		_targetPosition = _target.transform.position;
		_currentSpeed = speed;
		Rotate((_targetPosition - transform.position).normalized);
	}

	private void Update()
	{
		if(!_moveBullet) return;

		if(Vector3.Distance(transform.position, _targetPosition) < 0.2f)
		{
			CreateExplosion();
			_moveBullet = false;
			return;
		}
		_currentSpeed += acceleration * Time.deltaTime;
		var direction = (_targetPosition - transform.position).normalized;
		transform.position += direction * _currentSpeed * Time.deltaTime;
	}

	private void Rotate(Vector3 direction)
	{
		float angle = Mathf.Atan2(direction.x, direction.y) * -1 * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	private void CreateExplosion()
	{
		var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		explosion.SetDamage(_bullet.damage);
		Destroy(gameObject);
	}
}
