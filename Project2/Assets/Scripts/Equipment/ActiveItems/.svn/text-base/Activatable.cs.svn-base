using UnityEngine;
using System.Collections;

public class Activatable : Equipment
{
	float tempCD;
	
	public bool holdable;
	
	public bool OnCooldown = false;
	protected float waitForTime = 0;
	
	protected float baseCD;
	protected string activationVis;
	
	protected float range;
	
	public Activatable()
	{
		_thisType = Equipment.ItemType.activatable;
	}
	
	public virtual void ActivateItem(Vector2 ActivationDirection, Vector2 charPosition)
	{
		
	}
	
	public virtual void UpdateCD()
	{
		waitForTime = Time.time + baseCD;
	}
	
	public bool IsCoolDownUp()
	{
		if(Time.time >= waitForTime) return true;
		else return false;
	}
}
