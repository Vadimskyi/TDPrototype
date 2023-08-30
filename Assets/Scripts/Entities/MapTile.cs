using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	[Serializable]
	public class MapTile
	{
		public Sprite sprite;
		public TileType type;
	}
	
	public enum TileType
	{
		GrassNoRoad,
		RoadStraightVertical,
		RoadStraightHorizontal,
		RoadSpawnUp,
		RoadSpawnDown,
		RoadSpawnLeft,
		RoadSpawnRight,
		RoadDeadEndUp,
		RoadDeadEndDown,
		RoadDeadEndLeft,
		RoadDeadEndRight,
		RoadCornerUpLeft,
		RoadCornerUpRight,
		RoadCornerDownLeft,
		RoadCornerDownRight,
		RoadCornerWideUpLeft,
		RoadCornerWideUpRight,
		RoadCornerWideDownLeft,
		RoadCornerWideDownRight,
		RoadTUp,
		RoadTDown,
		RoadTLeft,
		RoadTRight,
	}
}

