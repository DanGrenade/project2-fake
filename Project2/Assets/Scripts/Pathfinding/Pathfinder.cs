using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinder
{
	private Node[,] _nodes;
	private float distanceTraveled;
	private float heuristic;
	
	private PriorityQueue<Node> openList = new PriorityQueue<Node>();
	private List<Node> closedList = new List<Node>();

	public Pathfinder ()
	{
		_nodes = new Node[Overlord.CurrentLevel.LevelSize, Overlord.CurrentLevel.LevelSize];
		
		InitializeNodeArray();
	}
	
	private float Heuristic(Vector2 point1, Vector2 point2)
	{
	    return Math.Abs(point1.x - point2.x) + Math.Abs(point1.y - point2.y);
	}
	
	private void ResetSearchNodes()
	{
	    openList.Clear();
	    closedList.Clear();
	
	    for (int x = 0; x < Overlord.CurrentLevel.LevelSize; x++)
	    {
	        for (int y = 0; y < Overlord.CurrentLevel.LevelSize; y++)
	        {
	            Node node = _nodes[x, y];
	
	            if (node == null)
	            {
	                continue;
	            }
	
	            node.InOpenList = false;
	            node.InClosedList = false;
	
	            node.GValue = float.MaxValue;
	            node.Heuristic = float.MaxValue;
	        }
	    }
	}
	
	private List<Vector2> FindFinalPath(Node startNode, Node endNode)
	{
	    closedList.Add(endNode);
	
	    Node parentTile = endNode.Parent;
	
	    // Trace back through the nodes using the parent fields
	    // to find the best path.
	    while (parentTile != startNode)
	    {
	        closedList.Add(parentTile);
	        parentTile = parentTile.Parent;
	    }
	
	    List<Vector2> finalPath = new List<Vector2>();
	
	    // Reverse the path and transform into world space.
	    for (int i = closedList.Count - 1; i >= 0; i--)
	    {
	        finalPath.Add(new Vector2(closedList[i].Position.x, closedList[i].Position.y));
	    }
	
	    return finalPath;
	}
	
	public List<Vector2> FindPath(Vector2 startPoint, Vector2 endPoint)
	{
	    // Only try to find a path if the start and end points are different.
	    if (startPoint == endPoint)
	    {
	        return new List<Vector2>();
	    }
	
	    /////////////////////////////////////////////////////////////////////
	    // Step 1 : Clear the Open and Closed Lists and reset each node’s F 
	    //          and G values in case they are still set from the last 
	    //          time we tried to find a path. 
	    /////////////////////////////////////////////////////////////////////
	    ResetSearchNodes();
	
	    // Store references to the start and end nodes for convenience.
	    Node startNode = _nodes[(int)startPoint.x, (int)startPoint.y];
	   	Node endNode = _nodes[(int)endPoint.x, (int)endPoint.y];
	
	    /////////////////////////////////////////////////////////////////////
	    // Step 2 : Set the start node’s G value to 0 and its F value to the 
	    //          estimated distance between the start node and goal node 
	    //          (this is where our H function comes in) and add it to the 
	    //          Open List. 
	    /////////////////////////////////////////////////////////////////////
	    startNode.InOpenList = true;
	
	    startNode.Heuristic = Heuristic(startPoint, endPoint);
	    startNode.GValue = 0;
	
	    openList.Enqueue(startNode);
	
	    /////////////////////////////////////////////////////////////////////
	    // Setp 3 : While there are still nodes to look at in the Open list : 
	    /////////////////////////////////////////////////////////////////////
	    while (openList.Count() > 0)
	    {
	        /////////////////////////////////////////////////////////////////
	        // a) : Loop through the Open List and find the node that 
	        //      has the smallest F value.
	        /////////////////////////////////////////////////////////////////
	        Node currentNode = openList.Dequeue();
	
	        /////////////////////////////////////////////////////////////////
	        // b) : If the Open List empty or no node can be found, 
	        //      no path can be found so the algorithm terminates.
	        /////////////////////////////////////////////////////////////////
	        if (currentNode == null)
	        {
	            break;
	        }
	
	        /////////////////////////////////////////////////////////////////
	        // c) : If the Active Node is the goal node, we will 
	        //      find and return the final path.
	        /////////////////////////////////////////////////////////////////
	        if (currentNode == endNode)
	        {
	            // Trace our path back to the start.
	            //return FindFinalPath(startNode, endNode);
				return FindFinalPath(startNode, endNode);
	        }
	
	        /////////////////////////////////////////////////////////////////
	        // d) : Else, for each of the Active Node’s neighbours :
	        /////////////////////////////////////////////////////////////////
	        for (int i = 0; i < currentNode.Neighbors.Length; i++)
	        {
	            Node neighbor = currentNode.Neighbors[i];
	
	            //////////////////////////////////////////////////
	            // i) : Make sure that the neighbouring node can 
	            //      be walked across. 
	            //////////////////////////////////////////////////
	            if (neighbor == null || neighbor.Traversable == false)
	            {
	                continue;
	            }
				
				//prevents corners from being cut
				if (i % 2 == 1)
				{
					if (i != 7)
					{
						if(currentNode.Neighbors[i-1] == null || currentNode.Neighbors[i+1] == null)
						{
							continue;
						}	
					}
					
					else if (i == 7)
					{
						if(currentNode.Neighbors[6] == null || currentNode.Neighbors[0] == null)
						{
							continue;
						}
					}
				}
	
	            //////////////////////////////////////////////////
	            // ii) Calculate a new G value for the neighbouring node.
	            //////////////////////////////////////////////////
				if (i % 2 == 0)
				{
	            	distanceTraveled = currentNode.GValue + 10;
				}
				
				else
				{
					distanceTraveled = currentNode.GValue + 14;
				}
				
	            // An estimate of the distance from this node to the end node.
				if(neighbor.Heuristic == -1)
				{
	            	heuristic = Heuristic(neighbor.Position, endPoint);
				}
				neighbor.Heuristic = heuristic;
	
	            //////////////////////////////////////////////////
	            // iii) If the neighbouring node is not in either the Open 
	            //      List or the Closed List : 
	            //////////////////////////////////////////////////
	            if (neighbor.InOpenList == false && neighbor.InClosedList == false)
	            {
	                // (1) Set the neighbouring node’s G value to the G value 
	                //     we just calculated.
	                neighbor.GValue = distanceTraveled;
	                // (2) Set the neighbouring node’s F value to the new G value + 
	                //     the estimated distance between the neighbouring node and
	                //     goal node.
	                neighbor.FValue = distanceTraveled + heuristic;
	                // (3) Set the neighbouring node’s Parent property to point at the Active 
	                //     Node.
	                neighbor.Parent = currentNode;
	                // (4) Add the neighbouring node to the Open List.
	                neighbor.InOpenList = true;
	                openList.Enqueue(neighbor);
	            }
	            //////////////////////////////////////////////////
	            // iv) Else if the neighbouring node is in either the Open 
	            //     List or the Closed List :
	            //////////////////////////////////////////////////
	            else if (neighbor.InOpenList || neighbor.InClosedList)
	            {
	                // (1) If our new G value is less than the neighbouring 
	                //     node’s G value, we basically do exactly the same 
	                //     steps as if the nodes are not in the Open and 
	                //     Closed Lists except we do not need to add this node 
	                //     the Open List again.
	                if (neighbor.GValue > distanceTraveled)
	                {
	                    neighbor.GValue = distanceTraveled;
	                    neighbor.FValue = distanceTraveled + heuristic;
	
	                    neighbor.Parent = currentNode;
	                }
	            }
	        }
	
	        /////////////////////////////////////////////////////////////////
	        // e) Remove the Active Node from the Open List and add it to the 
	        //    Closed List
	        /////////////////////////////////////////////////////////////////
	        currentNode.InClosedList = true;
	    }
	
	    // No path could be found.
	    return new List<Vector2>();
	}
	
	private void InitializeNodeArray()
	{
		for(int k=0; k < Overlord.CurrentLevel.LevelSize; k++)
		{
			for(int i=0; i < Overlord.CurrentLevel.LevelSize; i++)
			{
				if(Overlord.CurrentLevel.grid.RectArray[i,k].height == 0)
				{
					_nodes[i,k] = new Node(i,k);
					_nodes[i,k].Traversable = true;
				}
			}
		}
		
		for(int k=0; k < Overlord.CurrentLevel.LevelSize; k++)
		{
			for(int i=0; i < Overlord.CurrentLevel.LevelSize; i++)
			{
				if(_nodes[i,k] != null)
				{
					if(_nodes[i,k].Traversable == true)
						{
						//north
						try
						{
							if(_nodes[i,k-1].Traversable == true)
							{
								_nodes[i,k].Neighbors[0] = _nodes[i,k-1];
							}
						}catch{}
						
						//northeast
						try
						{
							if(_nodes[i+1,k-1].Traversable == true)
							{
								_nodes[i,k].Neighbors[1] = _nodes[i+1,k-1];
							}
						}catch{}
						
						//east
						try
						{
							if(_nodes[i+1,k].Traversable == true)
							{
								_nodes[i,k].Neighbors[2] = _nodes[i+1,k];
							}
						}catch{}
						
						//southeast
						try
						{
							if(_nodes[i+1,k+1].Traversable == true)
							{
								_nodes[i,k].Neighbors[3] = _nodes[i+1,k+1];
							}
						}catch{}
						
						//south
						try
						{
							if(_nodes[i,k+1].Traversable == true)
							{
								_nodes[i,k].Neighbors[4] = _nodes[i,k+1];
							}
						}catch{}
						
						//southwest
						try
						{
							if(_nodes[i-1,k+1].Traversable == true)
							{
								_nodes[i,k].Neighbors[5] = _nodes[i-1,k+1];
							}
						}catch{}
						
						//west
						try
						{
							if(_nodes[i-1,k].Traversable == true)
							{
								_nodes[i,k].Neighbors[6] = _nodes[i-1,k];
							}
						}catch{}
						
						//northwest
						try
						{
							if(_nodes[i-1,k-1].Traversable == true)
							{
								_nodes[i,k].Neighbors[7] = _nodes[i-1,k-1];
							}
						}catch{}
					}
				}
			}
		}
	}
}


