using UnityEngine;
using System.Collections;

public class Blade : PhysicalEntity
{
	float swingPerTime;
	float startAngle;
	float endAngle;
	
	float timeStart;
	
	public Blade(string spriteName, Vector2 startPos, Vector2 center, float swingPerSec, float startAngle, float totalAngle)
	{
		visual = new FSprite(spriteName);
		position = startPos;
		visual.SetPosition (position);
		
		swingPerTime = swingPerSec;
		
		visual.rotation = startAngle;
		endAngle = startAngle + totalAngle;
		
		timeStart = Time.time;
	}
	
	
	public override void HandleUpdate()
	{
		visual.rotation += (swingPerTime * (Time.deltaTime));
		
		position.x = Mathf.Cos((-visual.rotation + 90) * (Mathf.PI / 180));
		position.y = Mathf.Sin((-visual.rotation + 90) * (Mathf.PI / 180));
		position *= 100;
		
		position += Overlord.CurrentLevel.character.charArt.GetPosition ();
		visual.SetPosition (position);
		
		
		if(visual.rotation >= endAngle)
		{
			Overlord.CurrentLevel.RemoveChild(visual);
			Overlord.CurrentLevel.entities.Remove(this);
		}
	}
	
}
