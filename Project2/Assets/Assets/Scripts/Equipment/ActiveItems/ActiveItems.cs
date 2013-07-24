using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ActiveItems
{
	
	enum WeaponEquipped
	{
		Gun, Sword
	};
	
	WeaponEquipped currentWeapon = WeaponEquipped.Gun;
	public Activatable currentPrimary = new Activatable();
	public Activatable currentSecondary = new Activatable();
	
	bool ActivationPossible;
	float CDlength;
	
	
	public void AttackWithWeapon(Vector2 activateDirection, Vector2 charPosition)
	{
		if(currentPrimary.IsCoolDownUp())
		{
			currentPrimary.ActivateItem(activateDirection, charPosition);
			ActivationPossible = false;
			currentPrimary.UpdateCD();
		}
	}	
	
	public void ActivateSecondary(Vector2 activateDirection, Vector2 charPosition)
	{
		if(currentSecondary.IsCoolDownUp())
		{
			currentSecondary.ActivateItem(activateDirection, charPosition);
			ActivationPossible = false;
			currentSecondary.UpdateCD();
		}
		
		
		
	}
	
}
