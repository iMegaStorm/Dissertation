using UnityEngine;
using System.Collections;

[System.Serializable]
public class Node 
{
	
	public bool walkable;
	public Vector3 worldPosition;
	public int gridX;
	public int gridY;

	public int gCost;
	public int hCost;
	public Node parent;
	public int cost;
	
	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY, int _cost) 
	{
		walkable = _walkable;
		cost = _cost;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
	}

	public int fCost 
	{
		get 
		{
			return gCost + hCost;
		}
	}
}
