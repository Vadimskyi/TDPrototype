using System.Collections.Generic;
using UnityEngine;
using VadimskyiLab.Events;

public class EnemyManager : MonoBehaviour
{
	public Enemy enemyPrefab;
	public int enemiesPerWave;

	public float waveDuration;

	[SerializeField]
	private int _currentWave;

	private float _currentWaveTime;
	private float _spawnEnemyRate;
	private float _spawnEnemyTimer;
	private MapPathManager _mapPathManager;
	private List<Enemy> _spawnedEnemies = new List<Enemy>();

	private void Awake()
	{
		_mapPathManager = GetComponent<MapPathManager>();
	}

	private void Start()
	{
		StartNewWave();
	}

	private void Update()
	{
		_currentWaveTime += Time.deltaTime;
		_spawnEnemyTimer += Time.deltaTime;

		if(_currentWaveTime >= waveDuration)
		{
			StartNewWave();
		}

		if(_spawnEnemyTimer >= _spawnEnemyRate)
		{
			_spawnEnemyTimer = 0;
			SpawnNewEnemy();
		}

	}

	private void StartNewWave()
	{
		_currentWaveTime = 0;
		_currentWave++;
		_spawnEnemyRate = waveDuration / enemiesPerWave;
		_spawnEnemyTimer = 0;
	}

	private void SpawnNewEnemy()
	{
		var enemy = Instantiate(enemyPrefab);
		enemy.SetPath(_mapPathManager.path);
		_spawnedEnemies.Add(enemy);
		EnemySpawnedEvent.Invoke(enemy);
	}

}
