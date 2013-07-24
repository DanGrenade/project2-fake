using UnityEngine;
using System.Collections;

public class Blink : Activatable
{
	Vector2 DesiredPos;
	float tempVal;
	float blinkTime = 0.05f;
	
	float Angles = 45 * Mathf.Deg2Rad;
	
	public Blink() : base()
	{
		icon = new FSprite("ENVIRONMENT/doubleWall");
		icon.x = 200;
		icon.y = 200;
		
		positionOnScreen = new Vector2(200, 200);
		
		holdable = false;
		baseCD = 0.5f;
		range = 64 * 4;
	}
	
	public override void ActivateItem(Vector2 ActivationDirection, Vector2 charPosition)
	{
		
		ActivationDirection = FindAngle(ActivationDirection);
		
		DesiredPos = ActivationDirection * range;
		DesiredPos += charPosition;
		
		DesiredPos.x += 32;
		DesiredPos.y += 32;
		
		tempVal = DesiredPos.x % 64.0f;
		
		
		if(tempVal > 32) DesiredPos.x += tempVal;
			else DesiredPos.x -= tempVal;
		
		
		tempVal = DesiredPos.y % 64.0f;
		
		if(tempVal > 32) DesiredPos.y += tempVal;
			else DesiredPos.y -= tempVal;
		
		DesiredPos.x -= 32;
		DesiredPos.y -= 32;
		
		Overlord.CurrentLevel.character.StartBlink(DesiredPos, blinkTime);
	}
	
	Vector2 FindAngle(Vector2 startingAngle)
	{
		float angle = Mathf.Atan2(startingAngle.x, startingAngle.y);
		
		
		if (angle % Angles != 0)
		{
			float newAngle = Mathf.Round(angle / Angles) * Angles;
   			startingAngle = new Vector2(Mathf.Sin(newAngle), Mathf.Cos(newAngle));
		}
		
		return startingAngle;
	}
}
