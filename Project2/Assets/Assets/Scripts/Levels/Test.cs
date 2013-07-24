using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : Level
{
	Wall tempTile;
	List<Vector2> path;
	FContainer pathVis;
	
	public override void Start () 
	{
		Initialize();
		
		rooms = lg.InitializeRoomArray();
		grid = lg.ExpandRoomsToArray(rooms, LevelSize);
		pathVis = new FContainer();
		
		AddChild(pathVis);
		AddChild(grid);

		LateInitialize();
	}
	
	public override void HandleUpdate()
	{
		LevelUpdate();
		
		if(Overlord.CurrentLevel.character.GridWatcher.Changed)
		{
			pathVis.RemoveAllChildren();
			
			//PFO.RequestPath(character.GridWatcher.GridVector, new Vector2(50, 50));

			/*foreach (Vector2 vect2 in path)
			{
				tempTile = new Wall((int)vect2.x, (int)vect2.y);
				tempTile.SetPosition(new Vector2((vect2.x * 64) + 32, (vect2.y * -64) - 32));
				tempTile.color = Color.yellow;
				pathVis.AddChild(tempTile);
			}*/
		}
	}
}
