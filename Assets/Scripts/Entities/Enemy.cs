using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	public float speed;
	public float health;
	public Vector3 targetPosition;

	private UnityEngine.AI.NavMeshAgent _agent;
	private Queue<Vector3> _path;

	private void Start()
	{
		_agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		_agent.updateRotation = false;
		_agent.updateUpAxis = false;
	}

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
		{
			if(_path.Count < 1) return;
			targetPosition = _path.Dequeue();
		}

		var direction = (targetPosition - transform.position).normalized;
		transform.position += direction * speed * Time.deltaTime;
	}

	public void UpdateTargetPosition(Vector3 tp)
	{
		_agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

		targetPosition = tp;

		_agent.SetDestination(targetPosition);

		NavMeshPath path = new NavMeshPath();

		_agent.CalculatePath(targetPosition, path);
		_path = new Queue<Vector3>(path.corners.Select(s => new Vector3(s.x, s.y, 0)));

		_agent.enabled = false;
		targetPosition = _path.Dequeue();
	}
}
