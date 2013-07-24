using UnityEngine;
using System.Collections;

public class Sword : Activatable
{	
	public FSprite swordVisual;
	float totalRange = 120;
	Vector2 startPos;
	float swingLength = 0.5f;
	float swingPerTime;
	
	public Sword()
	{
		icon = new FSprite("ENVIRONMENT/doubleWall");
		holdable = false;
		activationVis = "ENVIRONMENT/doubleWall";
		baseCD = 0.5f;
		range = 6;
		swingPerTime = totalRange / swingLength;
	}
	
	
	public override void ActivateItem(Vector2 ActivationDirection, Vector2 charPosition)
	{
		swordVisual = new FSprite(activationVis);
		
		#region Find start angle
		float startAngle = 360 - Vector2.Angle(ActivationDirection, new Vector2(0,1));
		if((Vector3.Cross (ActivationDirection, new Vector2(0,1))).z > 0) startAngle = 360 - startAngle;
		
		startAngle -= totalRange/2;
		#endregion
			
			
		
		startPos.x = Mathf.Cos((-startAngle + 90) * (Mathf.PI / 180));
		startPos.y = Mathf.Sin((-startAngle + 90) * (Mathf.PI / 180));
		
		startPos *= 100;
		
		startPos += charPosition;
		
		swordVisual.SetPosition(startPos);
		
		Overlord.CurrentLevel.entities.Add (new Blade(activationVis, startPos, charPosition, swingPerTime, startAngle, totalRange));
		Overlord.CurrentLevel.AddChild(Overlord.CurrentLevel.entities[Overlord.CurrentLevel.entities.Count - 1].visual);
		
		
	}
}