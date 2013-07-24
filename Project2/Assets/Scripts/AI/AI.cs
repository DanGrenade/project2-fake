using System;
using UnityEngine;
using System.Collections.Generic;

public class AI : FSprite
{
	public AI ()
	{
	}
	
	public Vector2 VectorToPlayer()
	{
		return Overlord.CurrentLevel.character.charArt.GetPosition() - GetPosition();
	}
}


