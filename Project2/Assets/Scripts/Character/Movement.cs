using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement 
{
	Vector2 tempVect;
	Vector2 newVector;
	Vector2 vertical = new Vector2(0, 1);
	Vector2 horizontal = new Vector2(0, 1);
	
	Vector2 previousVector;
	float Acceleration = 50f;
	float accelerationRatio = 10f;
	float accelerationInitial = 20f;
	
	float drag = 18;
	float dragRatio = 40f;
	
	
	float MaxSpeed;
	float speedValue = 1.4f;
	float speedInitial = 3f;
	
	public Rect playerRect;
	
	#region Finding the closest side params
	enum Side
	{
		North, South, East, West
	};
	Side closestSide;
	
	protected float xDifference1, xDifference2;
	protected float yDifference1, yDifference2;
	
	Vector2 vect = Vector2.zero;
	#endregion
	
	
	List<Rect> IntersectingRects = new List<Rect>();
	
	
	public Movement(int speed, int floatiness)
	{
		MaxSpeed = (speed * speedValue) + speedInitial;
		Acceleration = (floatiness * accelerationRatio) + accelerationInitial;
		drag = (Mathf.Pow(floatiness, -0.1f) * dragRatio) + drag;
	}
	
	
	public Vector2 CalculatePlayerMovement(Vector2 inputVector)
	{
		previousVector += (inputVector * Acceleration);
		previousVector *= (drag);
		previousVector *= Time.deltaTime;
		if(previousVector.magnitude > MaxSpeed)
		{
			previousVector.Normalize();
			previousVector *= MaxSpeed;
		}
		
		newVector = CalculateCollision(previousVector);
		newVector += previousVector;
		
		if(newVector.magnitude > MaxSpeed)
		{			
						
			newVector.Normalize();
			newVector *= MaxSpeed;
		}
		
		return newVector;
	}
	
	Vector2 CalculateCollision(Vector2 vect)
	{
		tempVect = Vector2.zero;
		
		for(int k=0; k < Overlord.CurrentLevel.grid.RectArray.GetLength(1); k++)
		{
			for(int i=0; i < Overlord.CurrentLevel.grid.RectArray.GetLength(0); i++)
			{
				if(Overlord.CurrentLevel.grid.RectArray[i,k].CheckIntersect(playerRect))
				{
					int q = FindPositionInList(Overlord.CurrentLevel.grid.RectArray[i, k], playerRect);
					
					IntersectingRects.Insert(q, Overlord.CurrentLevel.grid.RectArray[i,k]);
				}
			}
		}
		
		for(int i = 0; i < Overlord.CurrentLevel.otherRects.Count; i++)
		{
			if(Overlord.CurrentLevel.otherRects[i].CheckIntersect(playerRect))
			{
				int q = FindPositionInList (Overlord.CurrentLevel.otherRects[i], playerRect);
				
				IntersectingRects.Insert(q, Overlord.CurrentLevel.otherRects[i]);
			}
			
		}
		
		
		
		
		foreach(Rect rect in IntersectingRects)
		{			
			if(rect.CheckIntersect(playerRect))
			{
				tempVect += MovePlayerOutOfObject(rect, playerRect);
				playerRect = playerRect.CloneAndOffset(tempVect.x, tempVect.y);
			}
		}
		
		IntersectingRects.RemoveRange(0, IntersectingRects.Count);
		
		return tempVect;
	}
	
	Vector2 MovePlayerOutOfObject(Rect rect, Rect player)
	{
		closestSide = FindClosestSide(rect, player);
		
		vect = Vector2.zero;
		
		
		
		
		
		switch(closestSide)
		{
		case Side.East:
			if(xDifference1 != xDifference2
				&& xDifference1 != yDifference1
				&& xDifference1 != yDifference2)
			{
				vect.x = xDifference1;
				if(previousVector.x < 0)
				{
					previousVector = new Vector2(0, previousVector.y);
				}
			}
			break;
		case Side.West:
			if(xDifference2 != xDifference1
				&& xDifference2 != yDifference1
				&& xDifference2 != yDifference2)
			{
				vect.x = -xDifference2;
				if(previousVector.x > 0)
				{
					previousVector = new Vector2(0, previousVector.y);
				}
			}
			break;
		case Side.North:
			if(yDifference1 != xDifference1
				&& yDifference1 != xDifference2
				&& yDifference1 != yDifference2)
			{
				vect.y = yDifference1;
				if(previousVector.y < 0)
				{
					previousVector = new Vector2(previousVector.x, 0);
				}
			}
			break;
		case Side.South:
			if(yDifference2 != xDifference1
				&& yDifference2 != xDifference2
				&& yDifference2 != yDifference1)
			{
				vect.y = -yDifference2;
				if(previousVector.y > 0)
				{
					previousVector = new Vector2(previousVector.x, 0);
				}
			}
			break;
		}
		
		return vect;
	}
	
	Side FindClosestSide(Rect rect, Rect playerRect)
	{
		xDifference1 = Mathf.Abs(rect.xMax - playerRect.xMin); //The difference between the players left side and the rects right side
		xDifference2 = Mathf.Abs(rect.xMin - playerRect.xMax); //The difference between the players right side and the rects left side
		yDifference1 = Mathf.Abs(rect.yMax - playerRect.yMin); //The difference between the players bot side and the rects top side
		yDifference2 = Mathf.Abs(rect.yMin - playerRect.yMax); //The difference between the players top side and the rects bot side
		
		float dif = xDifference1;
		
		if(dif > xDifference2)
		{
			dif = xDifference2;
		}
		
		if(dif > yDifference1)
		{
			dif = yDifference1;
		}
		
		if(dif > yDifference2)
		{
			dif = yDifference2;
		}
		
		if(dif == xDifference1)
		{
			return Side.East;
		}
			
		if(dif == xDifference2)
		{
			return Side.West;
		}
		
		if(dif == yDifference1)
		{
			return Side.North;	
		}
		
		if(dif == yDifference2)
		{
			return Side.South;
			
		}
		
		return Side.West;
		
	}
	
	int FindPositionInList(Rect test, Rect playerRect)
	{
		int i = 0;
		float dist = (test.center - playerRect.center).sqrMagnitude;
		
		foreach(Rect rect in IntersectingRects)
		{
			if((rect.center - playerRect.center).sqrMagnitude < dist)
			{
				i++;
			}
		}
		
		return i;
		
	}
	
	
	
	
}
