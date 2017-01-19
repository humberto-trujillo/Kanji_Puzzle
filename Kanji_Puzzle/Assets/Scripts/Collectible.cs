using UnityEngine;
using System.Collections;

public class Collectible : GamePiece
{
	public bool clearedByBomb = false;
	public bool clearedAtBottom = false;

	void Start () 
	{
		matchValue = MatchValue.None;
	}
}
