using UnityEngine;
using System.Collections;

public class Circle
{
	Vector2 center;
	float radius;
	
	Vector2 distCheck;
	float cornerCheck;
	
	public Circle(Vector2 Center, float Radius)
	{
		center = Center;
		radius = Radius;
	}
	
	public void Offset(Vector2 MoveBy)
	{
		center += MoveBy;
	}
	
	
	public bool DoesRectangleIntersect(Rect rect)
	{
		distCheck.x = Mathf.Abs (center.x - rect.center.x);
		distCheck.y = Mathf.Abs (center.y - rect.center.y);
		
		if(distCheck.x > (rect.width/2 + radius)
			|| distCheck.y > (rect.height/2 + radius))
		{
			return false;
		}
		
		if(distCheck.x <= (rect.width/2)
			|| distCheck.y <= (rect.height/2))
		{
			return true;
		}
		
		cornerCheck = Mathf.Pow(distCheck.x - rect.width/2, 2) +
						Mathf.Pow(distCheck.y - rect.height/2, 2);
		
		return(cornerCheck <= Mathf.Pow(radius, 2));
		
	}
}
