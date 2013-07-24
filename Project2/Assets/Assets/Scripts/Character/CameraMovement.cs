using UnityEngine;
using System.Collections;

public class CameraMovement
{
	Vector2 totalAlteration = Vector2.zero;
	
	float alterationX = 0;
	float alterationY = 0;
	Vector2 start;
	
	float xDifference = 80;
	float yDifference = 30;
	
	public CameraMovement(Vector2 StartPosition)
	{
		start = StartPosition;
	}
	
	public void MoveCamera(int playerX, int playerY)
	{
		alterationX = 0;
		alterationY = 0;		
		
		#region Check for x movement
		if(playerX > xDifference + start.x)
		{
			alterationX = start.x + xDifference - playerX;
		}		
		else if(playerX < start.x - xDifference)
		{
			alterationX = start.x - xDifference - playerX;
		}	
		#endregion
		
		#region Check for Y movement
		if(playerY > yDifference + start.y)
		{
			alterationY = start.y + yDifference - playerY;
		}		
		else if(playerY < start.y - yDifference)
		{
			alterationY = start.y - yDifference - playerY;
		}			
		#endregion
		
		Overlord.CurrentLevel.x += (int)alterationX;
		Overlord.CurrentLevel.y += (int)alterationY;
		
		start.x -= alterationX;
		start.y -= alterationY;
	}
}
