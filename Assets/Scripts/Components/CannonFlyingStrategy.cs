/* Copyright (C) 2023 Vadimskyi - All Rights Reserved
 * Github - https://github.com/Vadimskyi
 * Website - https://www.vadimskyi.com/
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License.
 */

using UnityEngine;

public class CannonFlyingStrategy : MonoBehaviour
{
	public float speed;
	public float acceleration;
	public RocketExplosion explosionPrefab;
	public AnimationCurve animationCurve;

	private Enemy _target;
	private Bullet _bullet;
	private bool _moveBullet;
	private Vector3 _startingPosition;
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
		_startingPosition = transform.position;
		_targetPosition = _target.transform.position;
		_currentSpeed = speed;
		Rotate((_targetPosition - transform.position).normalized);
	}

	float animationProgress = 0;

	private void Update()
	{
		if(!_moveBullet) return;

		if(Vector3.Distance(transform.position, _targetPosition) < 0.2f)
		{
			CreateExplosion();
			_moveBullet = false;
			return;
		}
		MoveCurved();
	}

	private void MoveCurved()
	{
		/*var value = animationCurve.Evaluate(animationProgress/GetTimeToReachTarget());

		_currentSpeed += acceleration * Time.deltaTime;
		var direction = ((_targetPosition - transform.position) + Vector3.up * value * 5).normalized;
		transform.position += direction * _currentSpeed * Time.deltaTime;
		animationProgress += Time.deltaTime;*/

		float linearT = animationProgress / GetTimeToReachTarget();
		float heightT = animationCurve.Evaluate(linearT) * 4f;
		//Debug.Log($"time: {linearT}, height: {heightT}");

		transform.position = Vector2.Lerp(_startingPosition, _targetPosition, linearT) + new Vector2(0f, heightT);
		animationProgress += Time.deltaTime;
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

	private float GetTimeToReachTarget()
	{
		float target_Distance = Vector3.Distance(transform.position, _targetPosition);
		var firingAngle = 45.0f;
		var gravity = 9.8f;
		// Calculate the velocity needed to throw the object to the target at specified angle.
		float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

		// Extract the X  Y componenent of the velocity
		float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
		float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

		// Calculate flight time.
		float flightDuration = target_Distance / Vx;
		return flightDuration * 1f;
	}
}
