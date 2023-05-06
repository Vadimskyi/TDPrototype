using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VadimskyiLab.Events;

public class TurrentShotEvent : EventBase<TurrentShotEvent, TurrentShotEvent.Args>
{
	public class Args
	{
		public Enemy target;
		public Turret turret;
	}
}
