using UnityEngine;
using System.Collections;

public class Inventory
{
	Activatable _primary = new Activatable();
	Activatable _secondary = new Activatable();
	
	int _vOlts = 0;
	
	public enum Armor
	{
		light, medium, heavy
	};
	Armor _currentArmor = Armor.light;
	
	public bool changeInActives = false;

	
	public void AlterPrimary(Activatable NewPrimary)
	{
		_primary = NewPrimary;
		changeInActives = true;
	}
	
	public Activatable GetPrimary()
	{
		return _primary;
	}
	
	public void AlterSecondary(Activatable NewSecondary)
	{
		_secondary = NewSecondary;
		changeInActives = true;
	}
	
	public Activatable GetSecondary()
	{
		return _secondary;
	}
	
	public void AlterVOlts(int ChangeInVolts)
	{
		_vOlts += ChangeInVolts;
		if(_vOlts < 0)
		{
			_vOlts = 0;
		}
	}
	
	public bool DoesPlayerHaveEnough(int Cost)
	{
		if(_vOlts < Cost) return false;
			else return true;
	}
	
	public void AlterArmor(Armor newArmor)
	{
		_currentArmor = newArmor;
	}
	
}
