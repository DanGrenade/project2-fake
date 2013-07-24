using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerCharacter
{	
	public enum PlayerState
	{
		Normal,
		Blinking,
		Shopping
	};
	
	public PlayerState currentState = PlayerState.Normal;
	PlayerState prevState = PlayerState.Normal;
	bool paused = false;
	
	#region Player components	
	Movement movement;
	ActiveItems action;
	HealthHandler playerHealth;
	CameraMovement cam;
	public Inventory inventory;
	public GridWatcher GridWatcher;
	#endregion
	
	#region Player stats
	int Speed = 5;
	int maxHealth = 2;
	int drift = 1;
	int CDR;

	#endregion
	
	#region Movement
	Vector2 MovementVector;
	#endregion	
	
	#region Blink data
	
	float blinkEnd; //When we will finish blinking
	float blinkStart; //When the blink began
	Vector2 blinkStartPos; //The initial position when the blink began
	Vector2 blinkEndPos; //The position we will be at when the blink ends
	Vector2 blinkMove; //How much we will move for each second of blink
	
	#endregion

	public FSprite charArt = new FSprite("ENVIRONMENT/tWall");
	
	
	public PlayerCharacter()
	{	
		charArt.color = Color.red;
		
		charArt.x = (int)Futile.screen.halfWidth;
		charArt.y = (int)-Futile.screen.halfHeight;
		
		inventory = new Inventory();
		GridWatcher = new GridWatcher();
		cam = new CameraMovement(new Vector2(charArt.x, charArt.y));
		movement = new Movement(Speed, drift);
		action = new ActiveItems();
		playerHealth = new HealthHandler(maxHealth);
	}
	

    public void HandleUpdate()
    {	
		
		/*if(currentState != PlayerState.Shopping && paused == false)
		{
			if(Overlord.CurrentLevel.input.escButtonPressed)
			{
				Time.timeScale = 0;
				paused = true;
				Overlord.CurrentLevel.pauseMenu.TurnOnMenu();
			}
		}*/
		
		
		if(paused == false)
		{
			if(inventory.changeInActives)
			{
				action.currentPrimary = inventory.GetPrimary ();			
				action.currentSecondary = inventory.GetSecondary();
			}		
			
			if(currentState == PlayerState.Blinking)
			{
				#region Movement for the player whilst blinking
				charArt.SetPosition((blinkMove * ((Time.time - blinkStart))) + blinkStartPos);
				
				if(Time.time >= blinkEnd)
				{ 
					charArt.SetPosition (blinkEndPos);
					currentState = PlayerState.Normal;
					charArt.isVisible = true;
				}
				#endregion
			}	
			
			if(currentState == PlayerState.Normal)
			{
				#region Normal character action
				movement.playerRect = charArt.localRect.CloneAndOffset(charArt.x, charArt.y);
				
				MovementVector = movement.CalculatePlayerMovement(Overlord.CurrentLevel.input.inputVector);
			
				
				charArt.x += (int)MovementVector.x;
		        charArt.y += (int)MovementVector.y;
				
				
				if(Overlord.CurrentLevel.input.primaryHold)
				{
					if(action.currentPrimary.holdable)
					{
						action.AttackWithWeapon(Overlord.CurrentLevel.input.actionDirection, charArt.GetPosition());
					}
					else if(Overlord.CurrentLevel.input.primaryActivated)
					{
						action.AttackWithWeapon (Overlord.CurrentLevel.input.actionDirection, charArt.GetPosition());
					}
				}
				
				if(Overlord.CurrentLevel.input.secondaryHold)
				{
					if(action.currentSecondary.holdable)
					{
						action.ActivateSecondary(Overlord.CurrentLevel.input.actionDirection, charArt.GetPosition());
					}
					else if(Overlord.CurrentLevel.input.secondaryActivated)
					{
						action.ActivateSecondary(Overlord.CurrentLevel.input.actionDirection, charArt.GetPosition());
					}
				}
				#endregion
			}		
			
			cam.MoveCamera((int)charArt.x, (int)charArt.y);
		}
		
    }
	
	public void StartBlink(Vector2 DesiredPosition, float activeTime) 
	{
		blinkEndPos = DesiredPosition;
		currentState = PlayerState.Blinking;
		blinkEnd = Time.time + activeTime;
		blinkStart = Time.time;
		
		blinkMove = (DesiredPosition - charArt.GetPosition ())/activeTime;
		
		blinkStartPos = charArt.GetPosition ();
		
		charArt.isVisible = false;
		
		
		
	}
	
}
