using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;

public class Turret : MonoBehaviour
{
	public Transform nozzle;
	public Transform nozzleFlash;
	public Bullet bulletPrefab;
	public Collider2D rangeCollider;
	public float range;
	public float attackSpeed;
	public float damage;
	public float rotationSpeed;
	public Vector3 nozzleOffset;
	public TargetPriority targetPriority;

	private float _attackTimeAccumulator;
	private Enemy _targetEnemy;
	private List<Enemy> _enemiesInRange = new List<Enemy>();

	private void Start()
	{
		rangeCollider.OnTriggerEnter2DAsObservable()
			.Select(collider => collider.GetComponent<Enemy>())
			.Where(enemy => enemy != null)
			.Subscribe(enemy =>
			{
				_enemiesInRange.Add(enemy);
			})
			.AddTo(this);

		rangeCollider.OnTriggerExit2DAsObservable()
			.Select(collider => collider.GetComponent<Enemy>())
			.Where(enemy => enemy != null)
			.Subscribe(enemy =>
			{
				_enemiesInRange.Remove(enemy);
			})
			.AddTo(this);


		EnemyDestroyedEvent.Subscribe(OnEnemyDestroyed);
	}

	private void Update()
	{
		SelectTarget();

		UpdateNozzleRotation();
		UpdateShooting();
	}

	private void SelectTarget()
	{
		//if(_targetEnemy && _enemiesInRange.Contains(_targetEnemy)) return;

		switch(targetPriority)
		{
			case TargetPriority.First:
				_targetEnemy = _enemiesInRange.FirstOrDefault();
				break;
			case TargetPriority.Last:
				_targetEnemy = _enemiesInRange.LastOrDefault();
				break;
		}
	}

	private void UpdateShooting()
	{
		if(_targetEnemy == null) return;

		_attackTimeAccumulator += Time.deltaTime;
		var timeBetweenAttacks = 1 / attackSpeed;

		if(_attackTimeAccumulator > timeBetweenAttacks && NozzleAlignedWithTarget())
		{
			_attackTimeAccumulator = 0;
			Shoot();
		}
	}

	private void Shoot()
	{
		TurrentShotEvent.Invoke(new TurrentShotEvent.Args
		{
			target = _targetEnemy,
			turret = this
		});
	}

	private bool NozzleAlignedWithTarget()
	{
		if(_targetEnemy == null) return false;

		var direction = (_targetEnemy.transform.position - transform.position).normalized;
		var angle = Vector3.Angle(direction, nozzle.up);

		return angle < 5;
	}

	private void UpdateNozzleRotation()
	{
		if(_targetEnemy == null) return;

		Vector3 targetPosition = _targetEnemy.transform.position;

		var direction = (targetPosition - transform.position).normalized;

		//rotate nozzle
		var rotationAngle = Mathf.Atan2(direction.x, direction.y) * -1 * Mathf.Rad2Deg;
		nozzle.rotation = Quaternion.Slerp(nozzle.rotation, Quaternion.AngleAxis(rotationAngle, Vector3.forward), Time.deltaTime * rotationSpeed);
	}

	private void OnDestroy()
	{
		EnemyDestroyedEvent.Unsubscribe(OnEnemyDestroyed);
	}

	private void OnEnemyDestroyed(Enemy enemy)
	{
		_enemiesInRange.Remove(enemy);
	}
}
