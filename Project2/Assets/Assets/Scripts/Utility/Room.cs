using System;

public class Room
{
	public string Name;
	public int Rotation;
	public Tile[,] Array;
	
	public Room ()
	{
		Array = new Tile[20,20];
	}
}


