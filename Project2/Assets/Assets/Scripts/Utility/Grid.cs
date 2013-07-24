using System;
using UnityEngine;

public class Grid : FContainer
{
	public Tile[,] Array;
	public Rect[,] RectArray;
	
	public Grid (int size)
	{
		Array = new Tile[size, size];
		RectArray = new Rect[size, size];
	}
}


