/* Copyright (C) 2023 Vadimskyi - All Rights Reserved
 * Github - https://github.com/Vadimskyi
 * Website - https://www.vadimskyi.com/
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License.
 */

using UnityEngine;

public class Cannon :  Bullet
{

	public override void TargetReached()
	{
		base.TargetReached();
		//Create explosion
	}
}
