using System;
using UnityEngine;
using System.Collections.Generic;

public class PathfindingOverlord
{
	struct Request
	{
		//public AI pathRequester;
		public Vector2 startPoint;
		public Vector2 endPoint;
	}
	
	Pathfinder pathfinder;
	List<Request> requests;
	
	public PathfindingOverlord ()
	{
		pathfinder = new Pathfinder();
		requests = new List<Request>();
	}
	
	public void Update()
	{
		if(requests.Count != 0)
		{
			pathfinder.FindPath(requests[0].startPoint, requests[0].endPoint);
			requests.RemoveAt(0);
			//Debug.Log("found a path");
		}
	}
	
	public void RequestPath( Vector2 _startPoint, Vector2 _endPoint)
	{
		Request temp = new Request();
		//temp.pathRequester = _tempAI;
		temp.startPoint = _startPoint;
		temp.endPoint = _endPoint;
		requests.Add(temp);
	}
}


