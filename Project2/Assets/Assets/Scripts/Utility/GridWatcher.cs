using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridWatcher
{
	public int x;
	public int y;
	
	private int _previousPosX;
	private int _previousPosY;
	
	public bool Changed;
	
	public Vector2 GridVector;
	
	public GridWatcher ()
	{
		GridVector = new Vector2();
	}
	
	public void Update(float tempX, float tempY)
	{	
		if(!(_previousPosX == x && _previousPosY == y))
		{
			x = (int)Math.Round(tempX/64);
			y = (int)Math.Round(tempY/-64);
			
			_previousPosX = x;
			_previousPosY = y;
			
			Changed = true;
		}
		
		else
		{
			Changed = false;
		}
	}
}


