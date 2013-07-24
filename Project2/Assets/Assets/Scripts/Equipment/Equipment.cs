using UnityEngine;
using System.Collections;

public class Equipment
{
	public enum ItemType
	{
		activatable, passive, armor, empty
	};
	protected ItemType _thisType =  ItemType.empty;
	
	int _cost;
	
	public FSprite icon;
	public Vector2 positionOnScreen;
	
	public Equipment()
	{
		icon = new FSprite("ENVIRONMENT/doubleWall");
	}
	
	public void SetCost(int cost)
	{
		_cost = cost;
	}
	
	
	
	public void SetType(ItemType type)
	{
		_thisType = type;
	}
	
	public ItemType GetEquipType()
	{
		return _thisType;
	}
	
	public void SetToSelected()
	{
		icon.color = Color.red;
	}
	
	public void SetToNotSelected()
	{
		icon.color = Color.white;
	}
}
