using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShootingManager : MonoBehaviour
{
	private void Start()
	{
		TurrentShotEvent.Subscribe(OnTurrentShot);
	}

	private void OnTurrentShot(TurrentShotEvent.Args args)
	{
		var bullet = Instantiate(args.turret.bulletPrefab, args.turret.nozzleFlash.position, args.turret.nozzle.rotation);
		bullet.damage = args.turret.damage;
		bullet.target = args.target;
	}
}
