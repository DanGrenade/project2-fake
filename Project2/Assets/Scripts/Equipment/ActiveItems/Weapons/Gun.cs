using UnityEngine;
using System.Collections;

public class Gun : Activatable
{
	float start = 2;
	protected float speed;
	
	public Gun()
	{
		icon = new FSprite("ENVIRONMENT/doubleWall");
		
		holdable = true;
		baseCD = 0.5f;
		activationVis = "walls";
		range = 150000;
		speed = 900;
	}
	
	public override void ActivateItem(Vector2 ActivationDirection, Vector2 charPosition)
	{
		Overlord.CurrentLevel.entities.Add(new Projectile(speed, range, start, ActivationDirection, charPosition, activationVis));
		Overlord.CurrentLevel.AddChild (Overlord.CurrentLevel.entities[Overlord.CurrentLevel.entities.Count - 1].visual);
	}
	
}
