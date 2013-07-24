using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Merchant
{
	#region Data for actual merchant
	public FSprite visual;	//Merchant sprite
	public Rect merchantRect; //Merchants physics box
	
	Vector2 distanceFromPlayer;	//Vector from merchant to player
	float distanceNecessaryToSpeak = 150;	//How close player needs to be to speak
	#endregion
	
	#region Data for shop items and player items
	public List<Equipment> ItemsForSale = new List<Equipment>();	//Stuff in the shop
	public List<Equipment> PlayersItems = new List<Equipment>();	//Player's comparable items
	#endregion
	
	#region Shop states
	enum MenuState	//Possible shop states
	{
		Off, SelectingBuy, SelectingReplace
	};
	MenuState currentState = MenuState.Off;	//The current state
	
	int SelectionInShop = 0;	//Which item we have selected in shop
	int SelectionInHand = 0;	//Which item we have selected in hand
	#endregion
	
	#region Data for placing items
	int numberOfColumns = 4;	//How many columns of items we will have in the shop
	
	int x = 100;	//Distance between options X
	int y = 100;	//Distnace between options Y
	#endregion
	
	public Merchant()
	{
		#region Set up merchant
		visual = new FSprite("ENVIRONMENT/crossWall"); //Set the merchants visual
		visual.SetPosition (200, -200); //Set location
		merchantRect = visual.localRect.CloneAndOffset(visual.x, visual.y);	//Set rect location
		#endregion
		
		#region Add items to shop
		ItemsForSale.Add (new Blink());
		ItemsForSale.Add (new Gun());
		ItemsForSale.Add (new Sword());
		ItemsForSale.Add (new Blink());
		ItemsForSale.Add (new Gun());
		ItemsForSale.Add (new Sword());
		ItemsForSale.Add (new Blink());
		ItemsForSale.Add (new Gun());
		ItemsForSale.Add (new Sword());
		ItemsForSale.Add (new Blink());
		ItemsForSale.Add (new Gun());
		ItemsForSale.Add (new Sword());
		ItemsForSale.Add (new Blink());
		ItemsForSale.Add (new Gun());
		ItemsForSale.Add (new Sword());
		
		ItemsForSale[SelectionInShop].SetToSelected();	//Set first item to selected
		
		foreach(Equipment item in ItemsForSale)
		{
			item.icon.isVisible = false;
		}
		
		CalculatePositions();	//Place the items in their respective locations
		#endregion
		
		PlayersItems.Add (new Equipment());	//Placeholder player items
		foreach(Equipment item in PlayersItems)
		{
			item.icon.isVisible = false;
		}
		currentState = MenuState.Off; //Menu State off
		
	}
	
	public void HandleUpdate()
	{
		
		switch(currentState)
		{
		#region Turning shop On
		case MenuState.Off:
			distanceFromPlayer = Overlord.CurrentLevel.character.charArt.GetPosition () - merchantRect.center;
			
			if(distanceFromPlayer.magnitude < distanceNecessaryToSpeak)
			{
				if(Overlord.CurrentLevel.input.selectButtonPressed)
				{
					if(ItemsForSale.Count > 0) TurnShopOn ();
				}
			}
			break;	
		#endregion
			
		case MenuState.SelectingBuy:
			#region Moving between options
			
			#region Pressing Down
			if(Overlord.CurrentLevel.input.pressedDown)
			{				
				ItemsForSale[SelectionInShop].SetToNotSelected();
				if(SelectionInShop + numberOfColumns < ItemsForSale.Count)
				{
					SelectionInShop += numberOfColumns;
				}
				else
				{
					SelectionInShop = SelectionInShop % numberOfColumns;
				}
				
				
			}
			#endregion
			#region Pressing Up
			else if(Overlord.CurrentLevel.input.pressedUp)
			{
				ItemsForSale[SelectionInShop].SetToNotSelected();
				if(SelectionInShop - numberOfColumns < 0)
				{
					if(SelectionInShop % numberOfColumns > (ItemsForSale.Count - 1) % numberOfColumns)
					{
						SelectionInShop = (((SelectionInShop % numberOfColumns)) - ((ItemsForSale.Count - 1) % numberOfColumns) + (ItemsForSale.Count - 1)) - numberOfColumns;
					}
					else
					{
						SelectionInShop = ((SelectionInShop % numberOfColumns)) - ((ItemsForSale.Count - 1) % numberOfColumns) + (ItemsForSale.Count - 1);
					}
				}
				else
				{
					SelectionInShop -= numberOfColumns;
				}
			}
			#endregion
			#region Pressing Right
			if(Overlord.CurrentLevel.input.pressedRight)
			{
				ItemsForSale[SelectionInShop].SetToNotSelected();
				if((SelectionInShop + 1) % numberOfColumns == 0)
				{
					SelectionInShop = SelectionInShop + 1 - numberOfColumns;
				}
				else if((SelectionInShop + 1) >= ItemsForSale.Count)
				{
					SelectionInShop -= ((SelectionInShop) % numberOfColumns);
				}
				else
				{
					SelectionInShop += 1;
				}
				
				
			}
			#endregion
			#region Pressing Left
			else if(Overlord.CurrentLevel.input.pressedLeft)
			{
				ItemsForSale[SelectionInShop].SetToNotSelected();	
				if(SelectionInShop % numberOfColumns == 0)
				{
					if(((SelectionInShop) + numberOfColumns) >= ItemsForSale.Count)
					{
						SelectionInShop = ItemsForSale.Count - 1;
					}
					else
					{
						SelectionInShop = (SelectionInShop - 1) + numberOfColumns;
					}
				}
				else
				{
					SelectionInShop -= 1;
				}
				
			}
			#endregion
			ItemsForSale[SelectionInShop].SetToSelected();
			
			#endregion
			
			#region Show player comparable items
			switch(ItemsForSale[SelectionInShop].GetEquipType())
			{
			case Equipment.ItemType.activatable:
				#region Show activatables
				if(PlayersItems[0].GetEquipType() != Equipment.ItemType.activatable)
				{
					for(int i = 0; i < PlayersItems.Count; i++)
					{
						PlayersItems.RemoveAt(i);
					}
					
					PlayersItems.Add (Overlord.CurrentLevel.character.inventory.GetPrimary());
					PlayersItems.Add (Overlord.CurrentLevel.character.inventory.GetSecondary());
					
					for(int i = 0; i < PlayersItems.Count; i++)
					{
						PlayersItems[i].icon.y = - 500;
						PlayersItems[i].icon.x = 80 + (i * 200);
						
						Overlord.CurrentLevel.UI.AddChild (PlayersItems[i].icon);
						
					}
				}
				break;
				#endregion
			case Equipment.ItemType.armor:
				#region Show armor
				if(PlayersItems[0].GetEquipType() != Equipment.ItemType.armor)
				{
					for(int i = 0; i < PlayersItems.Count; i++)
					{
						PlayersItems.RemoveAt(i);
					}
				}
				break;
				#endregion
			case Equipment.ItemType.passive:
				#region Show passives
				if(PlayersItems[0].GetEquipType() != Equipment.ItemType.armor)
				{
					for(int i = 0; i < PlayersItems.Count; i++)
					{
						PlayersItems.RemoveAt(i);
					}
				}	
				break;
				#endregion
			}
			#endregion
			
			#region Pressing Back
			if(Overlord.CurrentLevel.input.backButtonPressed)
			{
				TurnShopOff ();
			}
			#endregion
		
			#region Selecting current option
			if(Overlord.CurrentLevel.input.selectButtonPressed)
			{
				ItemsForSale[SelectionInShop].SetToNotSelected();
				currentState = MenuState.SelectingReplace;
				
			}
			#endregion
				
			break;
		case MenuState.SelectingReplace:
			#region Handle option controls	
			
			#region Selecting Right
			if(Overlord.CurrentLevel.input.pressedRight)
			{
				PlayersItems[SelectionInHand].SetToNotSelected();
				if((SelectionInHand + 1) >= PlayersItems.Count)
				{
					SelectionInHand = 0;
				}
				else
				{
					SelectionInHand += 1;
				}
			}
			#endregion
			#region Selecting Left
			else if(Overlord.CurrentLevel.input.pressedLeft)
			{
				PlayersItems[SelectionInHand].SetToNotSelected();
				if((SelectionInHand - 1) < 0)
				{
					SelectionInHand = PlayersItems.Count - 1;
				}
				else
				{
					SelectionInHand -= 1;
				}
				
			}
			#endregion
			
			PlayersItems[SelectionInHand].SetToSelected();
			
			#endregion
			
			#region Selecting Back
			if(Overlord.CurrentLevel.input.backButtonPressed)
			{
				PlayersItems[SelectionInHand].SetToNotSelected();
				SelectionInHand = 0;
				currentState = MenuState.SelectingBuy;
			}
			#endregion
			
			#region Selecting Options
			if(Overlord.CurrentLevel.input.selectButtonPressed)
			{
				ReplaceItem();
			}
			#endregion
			
			
			break;
		}
	}
	
	
	
	public void TurnShopOn()
	{
		foreach(Equipment item in ItemsForSale)
		{
			item.icon.isVisible = true;
		}		
		foreach(Equipment item in PlayersItems)
		{
			item.icon.isVisible = true;
		}
		currentState = MenuState.SelectingBuy;
		Overlord.CurrentLevel.character.currentState = PlayerCharacter.PlayerState.Shopping;
	}
	
	public void TurnShopOff()
	{
		foreach(Equipment item in ItemsForSale)
		{
			item.icon.isVisible = false;
		}
		foreach(Equipment item in PlayersItems)
		{
			item.icon.isVisible = false;
		}
		currentState = MenuState.Off;
		Overlord.CurrentLevel.character.currentState = PlayerCharacter.PlayerState.Normal;
	}
	
	
	public void ReplaceItem()
	{
		Overlord.CurrentLevel.UI.RemoveChild (PlayersItems[SelectionInHand].icon);
		
		PlayersItems[SelectionInHand] = ItemsForSale[SelectionInShop];
		
		
		switch(PlayersItems[SelectionInHand].GetEquipType())
		{
		case Equipment.ItemType.activatable:
			if(SelectionInHand == 0)
			{
				Overlord.CurrentLevel.character.inventory.AlterPrimary ((Activatable)PlayersItems[SelectionInHand]);
			}
			else if(SelectionInHand == 1)
			{
				Overlord.CurrentLevel.character.inventory.AlterSecondary ((Activatable)PlayersItems[SelectionInHand]);
			}
			break;
		case Equipment.ItemType.armor:
			//Overlord.CurrentLevel.character.inventory.AlterArmor
			
			break;
		case Equipment.ItemType.passive:
			
			
			break;			
		}
		
		
		
		
		Overlord.CurrentLevel.UI.RemoveChild (ItemsForSale[SelectionInShop].icon);
		
		ItemsForSale.RemoveAt (SelectionInShop);
		
		
		
		Overlord.CurrentLevel.UI.AddChild (PlayersItems[SelectionInHand].icon);
		
		for(int i = 0; i < PlayersItems.Count; i++)
		{
			PlayersItems[i].icon.y = - 500;
			PlayersItems[i].icon.x = 80 + (i * 200);
			
		}
		
		CalculatePositions();
		
		if(SelectionInShop >= ItemsForSale.Count)
		{
			SelectionInShop = ItemsForSale.Count - 1;
		}
		
		currentState = MenuState.SelectingBuy;
		
		if(ItemsForSale.Count == 0)
		{
			TurnShopOff ();
		}
		
	}
	
	public void CalculatePositions()
	{
		int currentX = 0;
		int currentY = 0;
		
		for(int i = 0; i < ItemsForSale.Count; i++)
		{
			if(i % numberOfColumns == 0)
			{
				currentX = x;
				currentY -= y;
			}
			else
			{
				currentX += x;
			}
			
			ItemsForSale[i].icon.SetPosition(currentX, currentY);
			ItemsForSale[i].positionOnScreen = ItemsForSale[i].icon.GetPosition();
		}
		
		
	}
	
}
