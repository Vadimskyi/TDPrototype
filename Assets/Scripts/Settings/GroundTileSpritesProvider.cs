using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

[CreateAssetMenu(fileName = "GroundTileSpritesSettings", menuName = "Settings/GroundTileSpritesSettings")]
public class GroundTileSpritesProvider : ScriptableObject
{
	[SerializeField] private List<MapTile> _groundTileSprites;
}
