using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : Page
{
	protected LevelGenerator lg = new LevelGenerator();
	
	public FStage UI = new FStage("UserInterface");
	
	public int LevelSize = 100;
	
	public Room[,] rooms;
	public Grid grid;
	
	public List<PhysicalEntity> entities = new List<PhysicalEntity>();
	
	public List<Rect> otherRects = new List<Rect>();
	
	public PathfindingOverlord PFO;
	
	Merchant merchant;
	public PlayerCharacter character;
	//public PauseMenu pauseMenu;

	protected void Initialize()
	{
		#region Add UI elements
		Futile.AddStage (UI);
		UI.MoveToTop();
		//		pauseMenu = new PauseMenu();
		#endregion
		
		AddChild(lg.GenerateFloor(100));
		
		#region Add character
		character = new PlayerCharacter();
		AddChild(character.charArt);
		input.ChangeLocal(character.charArt);
		#endregion
		
		#region Add merchant
		merchant = new Merchant();
		otherRects.Add(merchant.merchantRect);
		AddChild(merchant.visual);
		foreach(Equipment buyit in merchant.ItemsForSale)
		{
			UI.AddChild (buyit.icon);
		}
		#endregion
	}
	
	public void LateInitialize()
	{
		PFO = new PathfindingOverlord();
	}
	
	protected void LevelUpdate()
	{		
		input.HandleUpdate ();
		character.HandleUpdate();
		
		for(int i = 0; i < entities.Count; i++)
		{
			entities[i].HandleUpdate();
		}
		
		merchant.HandleUpdate();
	}
}
