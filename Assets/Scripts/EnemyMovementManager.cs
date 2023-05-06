using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VadimskyiLab.Events;

public class EnemyMovementManager : MonoBehaviour
{
	public GameObject start;
	public GameObject end;

	private List<Enemy> _spawnedEnemies = new List<Enemy>();

	private void Awake()
	{
		EnemySpawnedEvent.Subscribe(OnEnemySpawned);
		EnemyDestroyedEvent.Subscribe(OnEnemyDestroyed);
	}

	private void OnDestroy()
	{
		EnemySpawnedEvent.Unsubscribe(OnEnemySpawned);
		EnemyDestroyedEvent.Unsubscribe(OnEnemyDestroyed);
	}

	private void Start()
	{
	}

	private void Update()
	{
		MoveEnemies();
	}

	private void OnEnemySpawned(Enemy enemy)
	{
		enemy.transform.position = start.transform.position;
		enemy.UpdateTargetPosition(end.transform.position);
		_spawnedEnemies.Add(enemy);
	}

	private void OnEnemyDestroyed(Enemy enemy)
	{
		_spawnedEnemies.Remove(enemy);
	}

	private void MoveEnemies()
	{
		foreach(var enemy in _spawnedEnemies)
		{
			enemy.MoveTowardsTarget(Time.deltaTime);
		}
	}
}
