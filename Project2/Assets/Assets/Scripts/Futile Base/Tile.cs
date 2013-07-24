using System;
using UnityEngine;

public class Tile : FSprite
{	
	public bool isBroken = false;
	protected int posX, posY;
	
	protected Tile northAdjecentTile = null;
	protected Tile eastAdjecentTile = null;
	protected Tile westAdjecentTile = null;
	protected Tile southAdjecentTile = null;
	
	public FSprite northBorder = null;
	public FSprite eastBorder = null;
	public FSprite westBorder = null;
	public FSprite southBorder = null;
	
	public bool isGem = false;
	
	public Tile (string spriteName, int posX, int posY) : base(spriteName)
	{
		
	}
	
	/*override public void HandleAddedToStage()
	{
		Futile.instance.SignalUpdate += HandleUpdate;
		base.HandleAddedToStage();	
	}
	
	override public void HandleRemovedFromStage()
	{
		Futile.instance.SignalUpdate -= HandleUpdate;
		base.HandleRemovedFromStage();	
	}*/
	
	public Vector2 FindGridPosition(Tile[,] grid)
	{
		for(int k=0; k < grid.GetLength(1); k++)
		{
			for(int i=0; i < grid.GetLength(0); i++)
			{
				if(grid[i,k] == this)
				{
					return new Vector2(i,k);
				}
			}
		}
		
		return new Vector2(0,0);
	}
	
	public virtual void Interact()
	{
		
	}
	
	public virtual bool ActTowards()
	{
		return false;
	}
	
	/*protected void FindAdjecentTiles()
	{	
		Tile tile = this;
		
		try{northAdjecentTile = Game.grid[tile.posX, tile.posY + 1];}
		catch{northAdjecentTile = null;}
		
		Debug.Log(northAdjecentTile.isBroken);
		
		try{eastAdjecentTile = Game.grid[(int)tile.posX + 1, (int)tile.posY];}
		catch{eastAdjecentTile = null;}
		
		try{westAdjecentTile = Game.grid[(int)tile.posX - 1, (int)tile.posY];}
		catch{westAdjecentTile = null;}
		
		try{southAdjecentTile = Game.grid[(int)tile.posX, (int)tile.posY - 1];}
		catch{southAdjecentTile = null;}
		
		Debug.Log("FindAdjecent Tile");
	}*/
	
	protected void Update(int num, Tile adjacent)
	{
		Debug.Log("Update Tile");
		
		//Tile tile = this;
	}
	
	/*protected void UpdateAdjecent()
	{	
		FindAdjecentTiles();
		
		try{northAdjecentTile.Update(1, northAdjecentTile);}
		catch{}
		
		try{eastAdjecentTile.Update(2, eastAdjecentTile);}
		catch{}
		
		try{westAdjecentTile.Update(3, westAdjecentTile);}
		catch{}
		
		try{southAdjecentTile.Update(4, southAdjecentTile);}
		catch{}
		
		Debug.Log("UpdateAdjecent Tile");
	}*/
	
	
}


