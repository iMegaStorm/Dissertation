using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class DijkstraPathfinding : MonoBehaviour
{
    public Transform seeker;
    public Transform target = null;
    public float moveSpeed;
    public List<Vector3> tracePath;
    private Queue<Node> path;
    private GameGrid gameGrid;
    Dictionary<Node, Node> NextNodeToGoal;
    private int targetIndex;
    private bool stopwatchStarted;
    private bool stopWatchCondition;
    Stopwatch stopWatch;

    [Header("Components")]
    private Unit unit;

    private void Awake()
    {
        gameGrid = FindObjectOfType<GameGrid>();
    }

    void Start()
    {
        unit = GetComponent<Unit>();
    }

    private void Update()
    {
        //Dijkstra(gameGrid.NodeFromWorldPoint(seeker.position), gameGrid.grid[0, 0]);

        if (target != null)
        {
            Dijkstra(gameGrid.NodeFromWorldPoint(seeker.position), gameGrid.NodeFromWorldPoint(target.position));
            stopWatchCondition = true;
        }
        else if (target == null)
        {
            unit.FindResource();
            stopWatchCondition = false;
        }

        //if (!stopwatchStarted && !stopWatchCondition && unit.isMarked)
        //{
        //    //print("Dijkstra");
        //    stopWatch = new Stopwatch();
        //    stopWatch.Start();
        //    stopwatchStarted = true;
        //}
    }

    void Dijkstra(Node start, Node goal)
    {
        if (!stopwatchStarted && !stopWatchCondition && unit.isMarked)
        {
            //print("Stopwatch Started");
            stopWatch = new Stopwatch();
            stopWatch.Start();
            stopwatchStarted = true;
        }
        NextNodeToGoal = new Dictionary<Node, Node>();//Determines for each tile where you need to go to reach the goal. Key=Tile, Value=Direction to Goal
        Dictionary<Node, int> costToReachNode = new Dictionary<Node, int>();//Total Movement Cost to reach the tile

        PriorityQueue<Node> nodeList = new PriorityQueue<Node>();
        nodeList.Enqueue(goal, 0);
        costToReachNode[goal] = 0;

        while (nodeList.Count > 0)
        {
            Node curNode = nodeList.Dequeue();
            
            if (curNode == start)
            {
                if (stopwatchStarted && unit.isMarked)
                {
                    stopWatch.Stop();
                    GameManager.instance.AlgorithmPerformance(stopWatch.Elapsed.ToString(), 1);
                    print("Dijkstra Stopwatch Ended: " + stopWatch.Elapsed);
                    stopwatchStarted = false;
                }

                RetracePath(start, goal);
                StopCoroutine("FollowPath");

                if (path.Count > 0 && target != null) // If path count is greater than zero, then run the path
                {
                    StartCoroutine("FollowPath");
                }
                break;
            }

            foreach (Node neighbor in gameGrid.GetNeighbours(curNode))
            {
                int newCost = costToReachNode[curNode] + neighbor.cost;
                if (costToReachNode.ContainsKey(neighbor) == false || newCost < costToReachNode[neighbor])
                {
                    if (neighbor.walkable)
                    {
                        costToReachNode[neighbor] = newCost;
                        int priority = newCost;
                        nodeList.Enqueue(neighbor, priority);
                        NextNodeToGoal[neighbor] = curNode;
                        //neighbor._Text = costToReachNode[neighbor].ToString();
                    }
                }
            }
        }
    }

    void RetracePath(Node start, Node goal)
    {
        //Get the Path
        //check if tile is reachable
        if (NextNodeToGoal.ContainsKey(start) == false)
        {
            return;
        }

        path = new Queue<Node>();
        Node pathNode = start;
        while (goal != pathNode)
        {
            pathNode = NextNodeToGoal[pathNode];
            path.Enqueue(pathNode);
            //Debug.Log(path.Peek().gridX + " " + path.Peek().gridY);
        }
    }

    IEnumerator FollowPath()
    {
        tracePath = new List<Vector3>();
        //Adding the paths into the list, so they're viewable within the inspector
        //Also providing the path for the seeker to follow
        for (int i = 0; i < path.Count; i++)
        {
            tracePath.Add(new Vector3(path.Peek().worldPosition.x, 0, path.Peek().worldPosition.z));
            //path.Dequeue();
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

    private void OnDrawGizmos()
    {
        if (path != null && GameManager.instance.showDijkstraPath)
        {
            foreach (Node n in path)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(new Vector3(n.worldPosition.x, GameManager.instance.pathHeight, n.worldPosition.z), Vector3.one * (1 - .1f));
            }
        }
    }
}
