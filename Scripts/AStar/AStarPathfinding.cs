using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class AStarPathfinding : MonoBehaviour
{ 
	public Transform seeker;
	public Transform target = null;
	[SerializeField] GameGrid grid;
	public List<Node> path;
	public List<Vector3> tracePath;
	private int targetIndex;
	public float moveSpeed;
	private bool stopwatchStarted;
	private bool stopWatchCondition;
	Stopwatch stopWatch;
	bool test;
	[Header("Components")]
	private Unit unit;

	void Awake() 
	{
		grid = FindObjectOfType<GameGrid>();
	}

    private void Start()
    {
		unit = GetComponent<Unit>();
	}

    void Update() 
	{
        if (target != null)
        {
            FindPath(seeker.position, target.position);
            stopWatchCondition = true;
        }
        if (target == null)
        {
			unit.FindResource();
			stopWatchCondition = false;
		}
    }

	public void FindPath(Vector3 startPos, Vector3 targetPos) 
	{
        if (!stopwatchStarted && !stopWatchCondition && unit.isMarked)
        {
            //print("Stopwatch Started");
            stopWatch = new Stopwatch();
            stopWatch.Start();
            stopwatchStarted = true;
        }
		//print("Method Called");

        Node startNode = grid.NodeFromWorldPoint(startPos); //Sets startNode equal to the grids startPos
		Node targetNode = grid.NodeFromWorldPoint(targetPos); //Sets targetNode equal to the grids targetPos

		List<Node> openSet = new List<Node>();
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(startNode);

        while (openSet.Count > 0) 
		{
			Node node = openSet[0]; //currentNode is equal to the first element in the openSet list
			for (int i = 1; i < openSet.Count; i ++) 
			{
				if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost) //If the openSet nodes fCost is lower/equal than the currentNode fCost 
				{
					if (openSet[i].hCost < node.hCost) //If the openSet hCost is less than node hCost
						node = openSet[i]; //Set node equal to the openSet
				}
			}

			openSet.Remove(node); //Remove currentNode from the openSet list
			closedSet.Add(node); //Add currentNode to the closedSet list

			if (node == targetNode) //If the node has found the end node 
			{
				if (stopwatchStarted && unit.isMarked)
				{
					stopWatch.Stop();
					GameManager.instance.AlgorithmPerformance(stopWatch.Elapsed.ToString(), 2);
					print("AStar Stopwatch Ended: " + stopWatch.Elapsed);
					stopwatchStarted = false;
                }
                RetracePath(startNode, targetNode); //begin retracing the path
				StopCoroutine("FollowPath"); //Safety precaution

				if(path.Count > 0 && target != null) // If path count is greater than zero or target is not null, then run the path
                {
					StartCoroutine("FollowPath"); //Begin traversing the path
                }

				return;
			}

			foreach (Node neighbour in grid.GetNeighbours(node)) 
			{
				if (!neighbour.walkable || closedSet.Contains(neighbour)) 
				{
					continue; //If its not walkable or contains the neighbour in the closedSet then continue checking other neighbours
				}

				int newCostToNeighbour = node.gCost + GetDistance(node, neighbour); //Calculates the gCost to the current neighbour
				if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) 
				{
					neighbour.gCost = newCostToNeighbour; //Sets gCost to the newMovementCostToNeighbour
					neighbour.hCost = GetDistance(neighbour, targetNode); //Sets the hCost from the neighbour node to the targetNode
					neighbour.parent = node; //Set neighbour parent equal to currentNode

					if (!openSet.Contains(neighbour)) //If it doesn't contain the neighbour in the openSet
						openSet.Add(neighbour); //Add the neighbour to the openSet
				}
			}
		}
	}

	//Used to retrace the path backwards from the end node to the start node
	void RetracePath(Node startNode, Node endNode) 
	{
		path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode) 
		{
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse();
		grid.path = path;
    }

    IEnumerator FollowPath()
    {
		 tracePath = new List<Vector3>();
		//Adding the paths into the list, so they're viewable within the inspector
		//Also providing the path for the seeker to follow
		for (int i = 0; i < path.Count; i++)
        {
			tracePath.Add(new Vector3(path[i].worldPosition.x, 0, path[i].worldPosition.z));
        }

		targetIndex = 0;
        Vector3 currentWaypoint = tracePath[0];
        while (tracePath.Count > 0)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
				//Debug.Log(targetIndex);
				if (targetIndex >= tracePath.Count)
                {
                    yield break;
                }
                currentWaypoint = tracePath[targetIndex];
            }
			//Debug.Log("Waypoint " + tracePath[targetIndex]);
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

	//Returns the distance between nodeA and nodeB
    int GetDistance(Node nodeA, Node nodeB) 
	{
		int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (distX > distY)
			return 14*distY + 10* (distX-distY);
		
		return 14*distX + 10 * (distY-distX);
	}
	private void OnDrawGizmos()
	{
		if (path != null && GameManager.instance.showAStarPath)
        {
            foreach (Node n in path)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(new Vector3(n.worldPosition.x, GameManager.instance.pathHeight, n.worldPosition.z), Vector3.one * (1 - .1f));
            }
        }
    }
}


//public void GetWorldPositions()
//   {
//       for (int i = 0; i < path.Count; i++)
//       {
//		seeker.transform.position = new Vector3(path[i].worldPosition.x, height, path[i].worldPosition.z);
//		//print(path[i].worldPosition.x + " " + path[i].worldPosition.y + " " + path[i].worldPosition.z);
//       }
//   }