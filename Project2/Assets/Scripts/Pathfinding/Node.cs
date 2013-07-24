using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : IComparable<Node>
{
	public Vector2 Position;

	public Node[] Neighbors;
	public Node Parent;
	
	public bool Traversable;
	public bool InOpenList;
	public bool InClosedList;
	
	public float Heuristic;
	public float GValue;
	public float FValue;
	
	public Node (int posX, int posY)
	{
		Position = new Vector2(posX, posY);
		Neighbors = new Node[8];
	}
	
	public int CompareTo(Node other)
	{
		if (this.FValue < other.FValue) return -1;
		else if (this.FValue > other.FValue) return 1;
		else return 0;
	}
}


