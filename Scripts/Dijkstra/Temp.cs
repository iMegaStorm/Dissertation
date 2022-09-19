using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    private GameGrid gameGrid;
    public bool showGrid;
    [SerializeField] List<int> distanceList;
    [SerializeField] List<Node> nodeList;
    int gridX;
    int gridY;

    private void Start()
    {
        gameGrid = FindObjectOfType<GameGrid>();
        gridX = (int)gameGrid.gridWorldSize.x;
        gridY = (int)gameGrid.gridWorldSize.y;
        Debug.Log((int)gameGrid.gridWorldSize.y);
        DijkstrasPathfinding();
    }

    void DijkstrasPathfinding()
    {
        InitialDistances();

        nodeList = new List<Node>();

        //Set the start location index to 0, in the 1D array
        distanceList[gameGrid.IndexFromWorldPoint(transform.position)] = 0;

        //Adds the node that the unit starts on, into the node list
        nodeList.Add(gameGrid.NodeFromWorldPoint(transform.position));

        while (nodeList.Count > 0)
        {
            List<Node> newNodeList = new List<Node>();

            foreach (Node node in nodeList)
            {
                int index = node.gridX + node.gridY * gridX;
                Debug.Log(index);

                if (index - gridX > 0 && distanceList[index - gridX] == -1) //North
                {
                    distanceList[index - gridX] = distanceList[index] + 1;
                    newNodeList.Add(gameGrid.grid[node.gridX, node.gridY - 1]);
                    //Debug.Log((node.gridX) + " " + (node.gridY - 1));
                }
                if (index + 1 < gridY && distanceList[index + 1] == -1) //East
                {
                    distanceList[index + 1] = distanceList[index] + 1;
                    newNodeList.Add(gameGrid.grid[node.gridX + 1, node.gridY]);
                    Debug.Log((node.gridX + 1) + " " + (node.gridY));
                }
                if (index + gridX < gridX && distanceList[index + gridX] == -1) //South
                {
                    distanceList[index + gridX] = distanceList[index] + 1;
                    newNodeList.Add(gameGrid.grid[node.gridX, node.gridY + 1]);
                    Debug.Log((node.gridX) + " " + (node.gridY + 1));
                }
                if (index - 1 > 0 && distanceList[index - 1] == -1) //West
                {
                    distanceList[index - 1] = distanceList[index] + 1;
                    newNodeList.Add(gameGrid.grid[node.gridX - 1, node.gridY]);
                    Debug.Log((node.gridX - 1) + " " + (node.gridY));
                }
            }
            nodeList = newNodeList;
        }
    }

    void InitialDistances()
    {
        distanceList = new List<int>();
        for (int i = 0; i < gameGrid.gridWorldSize.x; i++)
        {
            for (int j = 0; j < gameGrid.gridWorldSize.y; j++)
            {
                distanceList.Add(-1); //Set every other node to infinite i.e -1
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gameGrid.gridWorldSize.x, 1, gameGrid.gridWorldSize.y));

        if (gameGrid.grid != null && showGrid)
        {
            foreach (Node n in gameGrid.grid)
            {
                if (distanceList[n.gridX + n.gridY * gridX] > -1)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (1 - .1f));
                }
                //else if(distanceList[n.gridX + n.gridY * gridX] == -1)
                //{
                //    Gizmos.color = Color.white;
                //}


            }
        }
    }
}
