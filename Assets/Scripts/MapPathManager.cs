/* Copyright (C) 2023 Vadimskyi - All Rights Reserved
 * Github - https://github.com/Vadimskyi
 * Website - https://www.vadimskyi.com/
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License.
 */

using UnityEngine;
using System.Linq;

public class MapPathManager : MonoBehaviour
{
	public bool drawPath;
	public LineRenderer lineRenderer;
	public Vector2[] path;
	
	private void Start()
	{
		if (drawPath)
		{
			DrawPath();
		}
	}
	
	private void DrawPath()
	{
		lineRenderer.positionCount = path.Length;
		for(int i = 0; i < path.Length; i++)
		{
			lineRenderer.SetPosition(i, new Vector3(path[i].x, path[i].y, 0));
		}
	}
}
