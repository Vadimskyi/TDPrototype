using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SimpleSpriteAnimation))]
public class RocketExplosion : MonoBehaviour
{
	private float _expDamage;
	private SimpleSpriteAnimation _animation;

	private void Awake()
	{
		_animation = GetComponent<SimpleSpriteAnimation>();
	}

	private void Start()
	{
		_animation.StartAnimation();
		StartCoroutine(DestroyAfterAnimation());
	}

	public void SetDamage(float damage)
	{
		_expDamage = damage;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		var enemy = col.GetComponent<Enemy>();
		if(!enemy) return;
		enemy.TakeDamage(_expDamage);
	}

	private IEnumerator DestroyAfterAnimation()
	{
		yield return new WaitForSeconds(_animation.duration);
		Destroy(gameObject);
	}
}
