using UnityEngine;
using System.Collections;

public class HealthHandler
{
	int healthStatParam = 100;
	
	
	int maxHealth;
	int totalHealth;
	
	
	public HealthHandler(int PlayerHealth)
	{
		maxHealth = PlayerHealth * healthStatParam;
	}
	
	public HealthHandler(float MaxOtherHealth)
	{
		maxHealth = (int)MaxOtherHealth;
	}
	
	public void DamageDeal(int damageDealt)
	{
		totalHealth -= damageDealt;
	}
	
	public void RegainHealth(int healthRecovered)
	{
		totalHealth += healthRecovered;
	}
	
	public void HealthStatAlteration(int NewHealthStat)
	{
		maxHealth = NewHealthStat * healthStatParam;
		if(totalHealth > maxHealth)
		{
			totalHealth = maxHealth;
		}
	}
	
	public bool IsThisDead()
	{
		if(totalHealth < 0) return true;
		else return false;
	}
	
}
