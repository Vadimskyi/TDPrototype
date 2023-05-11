using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
	public SpriteRenderer health;

	public void UpdatHealth(float value, float maxHealth)
	{
		health.size = new Vector2(value / maxHealth, health.size.y);
		health.transform.localPosition = new Vector3((1.15f - health.size.x) / 2, health.transform.localPosition.y, health.transform.localPosition.z);
	}
}
