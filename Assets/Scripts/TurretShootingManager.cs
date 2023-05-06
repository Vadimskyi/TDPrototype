using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShootingManager : MonoBehaviour
{
	public Bullet bulletPrefab;


	private void Start()
	{
		TurrentShotEvent.Subscribe(OnTurrentShot);
	}

	private void OnTurrentShot(TurrentShotEvent.Args args)
	{
		var bullet = Instantiate(bulletPrefab, args.turret.nozzle.position, args.turret.nozzle.rotation);
		bullet.damage = args.turret.damage;
		bullet.target = args.target;
	}
}
