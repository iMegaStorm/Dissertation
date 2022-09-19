using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Stats")]
    public int foodType;
    public int gatherAmount;
    public float scanRange;
    public LayerMask resourceLayer;
    public bool isMarked;
    //public List<Transform> resourceList;

    [Header("Gathering Info")]
    public float gatherRate;
    private float lastGatherTime;

    [Header("Components")]
    public TeamManager team;
    public Resources resources;
    private AStarPathfinding aStar;
    private DijkstraPathfinding dijkstra;
    private GameGrid gameGrid;

    private void Start()
    {
        aStar = GetComponent<AStarPathfinding>();
        dijkstra = GetComponent<DijkstraPathfinding>();
        resources = FindObjectOfType<Resources>();
        gameGrid = FindObjectOfType<GameGrid>();

        if (team.firstUnitCount < 2)
            isMarked = true;
    }

    public void FindResource()
    {
        //Debug.Log("Called");
        var getResourceList = resources.resourceList;
        Transform closestResource = null;
        float closestDist = 100000000f;

        foreach (Transform resource in getResourceList)
        {
            if (resource == null)
                continue;

            float dist = Vector3.Distance(transform.position, resource.position);

            if(closestResource == null)
            {
                closestResource = resource;
                closestDist = dist;
            }
            else if(dist < closestDist)
            {
                closestResource = resource;
                closestDist = dist;
            }
        }
        
        if(closestResource != null)
        {
            gameGrid.NodeFromWorldPoint(closestResource.position).walkable = true;
            var closestChild = closestResource.GetChild(0);
            closestChild.gameObject.layer = 0;

            if (team.isTeam1 && dijkstra != null)
            {
                dijkstra.target = closestResource;
                //print(closestResource.name);
            }
            else if (!team.isTeam1 && aStar != null)
            {
                aStar.target = closestResource;
                //aStar.Test();
                //aStar.FindPath(aStar.seeker.position, aStar.target.position);
            }

        }
        else
        {
            return;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Checks if the player is at a resource and its the target resource before gathering

        if (team.isTeam1 && dijkstra != null)
        {
            if (other.CompareTag("Resource") && dijkstra.target.transform == other.gameObject.transform)
            {
                //Debug.Log(aStar.target.transform + " " + other.gameObject.transform);
                var resource = other.GetComponent<ResourceType>();

                if (Time.time - lastGatherTime > gatherRate)
                {
                    //Debug.Log("Gathering");
                    lastGatherTime = Time.time;
                    resource.GatherResource(gatherAmount, team);
                }
            }
        }
        else if (!team.isTeam1 && aStar != null)
        {
            if (other.CompareTag("Resource") && aStar.target.transform == other.gameObject.transform)
            {
                //Debug.Log(aStar.target.transform + " " + other.gameObject.transform);
                var resource = other.GetComponent<ResourceType>();

                if (Time.time - lastGatherTime > gatherRate)
                {
                    //Debug.Log("Gathering");
                    lastGatherTime = Time.time;
                    resource.GatherResource(gatherAmount, team);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        //if (aStar.path != null && team.showPath)
        //{
        //    foreach (Node n in aStar.path)
        //    {
        //        Gizmos.color = Color.black;
        //        Gizmos.DrawCube(new Vector3(n.worldPosition.x, 1, n.worldPosition.z), Vector3.one * (1 - .1f));
        //    }
        //}
    }
}
