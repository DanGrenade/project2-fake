using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomGenerator : object
{
	Texture2D levelMap;
	Color32[] rawImageData;
	Room roomContainer;
	
	#region Color Elements
	Color32 floor = new Color32(255,255,255,255);
	Color32 wall  = new Color32(0,0,0,255);
	#endregion 
	
	public RoomGenerator ()
	{
		
	}
	
	public Room Generate (string room)
	{
		roomContainer = new Room();
		roomContainer.Name = room;
		
		int x = 0;
		int y = 0;
		
		levelMap = (Texture2D)Resources.Load("Rooms/" + room);
		
		rawImageData = levelMap.GetPixels32();
		
		for(int i=0; i<rawImageData.Length; i++)
		{
			if (Compare(rawImageData[i], floor))
			{
				roomContainer.Array[x,y] = new Empty();
			}
			
			else if (Compare(rawImageData[i], wall))
			{
				roomContainer.Array[x,y] = new Wall(x, y);
				roomContainer.Array[x,y].color = Color.blue;
			}
			
			else
			{
				roomContainer.Array[x,y] = new Empty();
			}
			
			x++;
			
			if (x >= 20)
			{
				x = 0;
				y++;
			}
			
		}
		int rotation = Overlord.random.Next(0, 4);
		roomContainer.Rotation = rotation;
		
		switch (rotation)
		{
		case 0:
			return roomContainer;
			
		case 1:
			roomContainer.Array = Rotate(roomContainer.Array, 20);
			return roomContainer;	
			
		case 2:
			roomContainer.Array = Rotate(Rotate(roomContainer.Array, 20), 20);
			return roomContainer;
			
		case 3:
			roomContainer.Array = Rotate(Rotate(Rotate(roomContainer.Array, 20), 20), 20);
			return roomContainer;
			
		default:
			return roomContainer;
				
		}
	}
	
	private bool Compare(Color32 tile, Color32 ctile)
	{
		if (tile.r == ctile.r && tile.g == ctile.g && tile.b == ctile.b)
		{
			return true;
		}
		
		else
		{
			return false;
		}
	}

	private Tile[,] Rotate(Tile[,] matrix, int n) 
	{
    	Tile[,] ret = new Tile[n, n];

    	for (int i = 0; i < n; ++i) 
		{
        	for (int j = 0; j < n; ++j) 
			{
            	ret[i, j] = matrix[n - j - 1, i];
       	 	}
   		}

    return ret;
	}
}


