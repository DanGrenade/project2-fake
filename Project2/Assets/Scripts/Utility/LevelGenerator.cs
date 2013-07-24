using System;
using UnityEngine;

public class LevelGenerator
{
	private FContainer _floor;
	public Tile[,] _floorGrid;
	private Room[,] _rooms;
	private Rect[,] _rects;
	private Grid _grid;
	
	private	int _currentRoomX = 0;
	private	int _currentRoomY = 0;
	private	int _currentRoomPosX = 0;
	private	int _currentRoomPosY = 0;
	
	public LevelGenerator ()
	{
	}
	
	public FContainer GenerateFloor(int size)
	{
		_floorGrid = new Tile[size, size];
		_floor = new FContainer();
		
		for(int k=0; k < _floorGrid.GetLength(1); k++)
		{
			for(int i=0; i < _floorGrid.GetLength(0); i++)
			{
				_floorGrid[i,k] = new Floor(i, k);
				
				_floorGrid[i,k].x = (i * 64) + 32;
				_floorGrid[i,k].y = (k * -64) - 32;
				
				_floor.AddChild(_floorGrid[i,k]);
			}
		}
		return _floor;
	}
	
	public Room[,] InitializeRoomArray()
	{
		_rooms = new Room[5,5];
		
		for(int k=0; k < _rooms.GetLength(1); k++)
		{
			for(int i=0; i < _rooms.GetLength(0); i++)
			{
				_rooms[i,k] = Overlord.instance.roomGenerator.Generate("room" + Overlord.random.Next(1, 10));
				
				try
				{
					if(_rooms[i,k].Name == _rooms[i-1,k].Name)
					{
						i--;
					}
				}catch{}
				
				try
				{
					if(_rooms[i,k].Name == _rooms[i,k-1].Name)
					{
						i--;
					}
				}catch{}
			}
		}
		return _rooms;
	}
	
	public Grid ExpandRoomsToArray(Room[,] rooms, int size)
	{		
		_grid = new Grid(size);
		
		for(int k=0; k < size; k++)
		{
			for(int i=0; i < size; i++)
			{
				_grid.Array[i,k] = rooms[_currentRoomX, _currentRoomY].Array[_currentRoomPosX, _currentRoomPosY];
				
				if(!(_grid.Array[i,k] is Empty))
				{
					_grid.Array[i,k].x = (i * 64) + 32;
					_grid.Array[i,k].y = (k * -64) - 32;
					_grid.RectArray[i,k] = new Rect((i * 64), ((k * -64) - 64), 64, 64);
					_grid.AddChild(_grid.Array[i,k]);
				}

				_currentRoomPosX++;
				if(_currentRoomPosX > 19)
				{
					_currentRoomX++;
					_currentRoomPosX = 0;
				}
			}

			_currentRoomPosY++;
			_currentRoomX = 0;
			
			if(k == 19)
			{
				_currentRoomY = 1;
				_currentRoomPosY = 0;
			}

			else if(k == 39)
			{
				_currentRoomY = 2;
				_currentRoomPosY = 0;
			}
			
			else if(k == 59)
			{
				_currentRoomY = 3;
				_currentRoomPosY = 0;
			}
			
			else if(k == 79)
			{
				_currentRoomY = 4;
				_currentRoomPosY = 0;
			}
		}
		DrawOuterWalls();
		return _grid;
	}
	
	private void DrawOuterWalls()
	{
		//top
		for(int i=0; i < _grid.Array.GetLength(0); i++)
		{
			_floorGrid[i,0] = new Wall(i, 0);
			
			_floorGrid[i,0].x = (i * 64) + 32;
			_floorGrid[i,0].y = (0 * -64) - 32;
			
			_floor.AddChild(_floorGrid[i,0]);
			_grid.RectArray[i,0] = new Rect((i * 64), ((0 * -64) - 64), 64, 64);
		}
		
		//bottom
		for(int i=0; i < _grid.Array.GetLength(0); i++)
		{
			_floorGrid[i,(_grid.Array.GetLength(0)-1)] = new Wall(i, (_grid.Array.GetLength(0)-1));
			
			_floorGrid[i,(_grid.Array.GetLength(0)-1)].x = (i * 64) + 32;
			_floorGrid[i,(_grid.Array.GetLength(0)-1)].y = (_grid.Array.GetLength(0) * -64) + 32;
			
			_floor.AddChild(_floorGrid[i,(_grid.Array.GetLength(0)-1)]);
			_grid.RectArray[i,(_grid.Array.GetLength(0)-1)] = new Rect((i*64), (((_grid.Array.GetLength(0)-1 ) * -64) - 64), 64, 64);
		}
		
		//left
		for(int i=0; i < _grid.Array.GetLength(1); i++)
		{
			_floorGrid[0,i] = new Wall(0, i);
			
			_floorGrid[0,i].x = (0 * 64) + 32;
			_floorGrid[0,i].y = (i * -64) - 32;
			
			_floor.AddChild(_floorGrid[0,i]);
			_grid.RectArray[0,i] = new Rect((0 * 64), ((i * -64) - 64), 64, 64);
		}
		
		//right
		for(int i=0; i < _grid.Array.GetLength(1); i++)
		{
			_floorGrid[(_grid.Array.GetLength(1)-1),i] = new Wall((_grid.Array.GetLength(1)-1), i);
			
			_floorGrid[(_grid.Array.GetLength(1)-1),i].x = (_grid.Array.GetLength(1) * 64) - 32;
			_floorGrid[(_grid.Array.GetLength(1)-1),i].y = (i * -64) - 32;
			
			_floor.AddChild(_floorGrid[(_grid.Array.GetLength(1)-1),i]);
			_grid.RectArray[(_grid.Array.GetLength(1)-1),i] = new Rect(((_grid.Array.GetLength(1)-1) * 64), ((i * -64) - 64), 64, 64);
		}
	}
}


