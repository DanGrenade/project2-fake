using UnityEngine;
using System.Collections;

public class Projectile : PhysicalEntity
{
	
	Vector2 direction;
	Vector2 startingPoint;
	float speed;
	float range;
	
	Circle circle;
	
	public Projectile(float spd, float rng, float strt, Vector2 dir, Vector2 start, string vis)
	{
		speed = spd;
		range = rng;
		direction = dir;
		startingPoint = start;
		visual = new FSprite(vis);
		
		direction.Normalize();
		
		
		position = direction * strt;
		visual.x = position.x + startingPoint.x;
		visual.y = position.y + startingPoint.y;
		
		circle = new Circle(position + startingPoint, visual.width/2);
		
	}
	
	public override void HandleUpdate()
	{
		
		position += (direction * speed * Time.deltaTime);
		circle.Offset (direction * speed * Time.deltaTime);
		
		visual.SetPosition (position.x + startingPoint.x, position.y + startingPoint.y);
		
		if(position.sqrMagnitude >= range)
		{
			Overlord.CurrentLevel.RemoveChild(visual);
			Overlord.CurrentLevel.entities.Remove(this);
				
		}
		else
		{
			for(int k=0; k < Overlord.CurrentLevel.grid.RectArray.GetLength(1); k++)
			{
				for(int i=0; i < Overlord.CurrentLevel.grid.RectArray.GetLength(0); i++)
				{
					if(Overlord.CurrentLevel.grid.RectArray[i,k] != null)
					{
						if(circle.DoesRectangleIntersect(Overlord.CurrentLevel.grid.RectArray[i,k]))
						{
							Overlord.CurrentLevel.RemoveChild(visual);
							Overlord.CurrentLevel.entities.Remove(this);
						}
					}
				}
			}
		}
		
		
		
	}
	
	
	
}
