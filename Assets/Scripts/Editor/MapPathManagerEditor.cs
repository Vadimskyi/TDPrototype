/* Copyright (C) 2023 Vadimskyi - All Rights Reserved
 * Github - https://github.com/Vadimskyi
 * Website - https://www.vadimskyi.com/
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License.
 */

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapPathManager))]
public class MapPathManagerEditor : Editor
{
	private MapPathManager _mapPathManager;

	private void OnEnable()
	{
		_mapPathManager = (MapPathManager) target;
	}

	private void OnSceneGUI()
	{
		Handles.Label(_mapPathManager.transform.position + new Vector3(-0.3f, 0.3f, 0), "Enemy spawn point");

		//draw path
		for(int i = 0; i < _mapPathManager.path.Length; i++)
		{
			Handles.Label(_mapPathManager.path[i] + new Vector2(-0.3f, 0.3f), "Path point " + i);
			_mapPathManager.path[i] = Handles.PositionHandle(_mapPathManager.path[i], Quaternion.identity);
		}
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
	}
}
