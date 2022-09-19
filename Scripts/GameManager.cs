using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("A Star Pathfinding")]
    public bool showAStarPath;
    public bool isFollowingAStarUnit;

    [Header("Dijkstra Pathfinding")]
    public bool showDijkstraPath;
    public bool isFollowingDijkstraUnit;

    [Header("GameGrid")]
    public bool showGameGrid;

    [Header("Path Gizmo Height")]
    public int pathHeight;

    [Header("Record Data")]
    public bool algorithmsPerformance;
    public bool resourceResults;

    [Header("Dijkstra Data")]
    public int unitCountTeam1;
    public int foodCompletedTeam1;
    public int treeCompletedTeam1;
    public int foodGatheredTeam1;
    public int treeGatheredTeam1;

    [Header("A Star Data")]
    public int unitCountTeam2;
    public int foodCompletedTeam2;
    public int treeCompletedTeam2;
    public int foodGatheredTeam2;
    public int treeGatheredTeam2;

    [HideInInspector] public string aStar;
    [HideInInspector] public string dijkstra;

    private void Awake()
    {
        instance = this;
    }

    public void AlgorithmPerformance(string time, int _team)
    {
        if(_team == 1)
        {
            TextOutput.instance.AlgorithmPerformance(1);
            dijkstra = time.ToString();
        }
        else if (_team == 2)
        {
            TextOutput.instance.AlgorithmPerformance(2);
            aStar = time.ToString();
        }
    }

    public void UpdateTeamCount(int _team)
    {
        //Updates Team Count
        if (_team == 1)
        {
            unitCountTeam1 += 1;
        }
        else if (_team == 2)
        {
            unitCountTeam2 += 1;
        }
    }

    public void UpdateResourceGatheredResults(int _resourceType, int _team)
    {
        //Gathered Resources (Total)
        if (_resourceType == 1) // Food
        {
            if (_team == 1) //Dijkstra
            {
                foodCompletedTeam1 += 1;
            }
            else if (_team == 2) //A Star
            {
                foodCompletedTeam2 += 1;
            }
        }
        else if (_resourceType == 2) // Wood
        {
            if (_team == 1) // Dijkstra
            {
                treeCompletedTeam1 += 1;
            }
            else if (_team == 2) // A Star
            {
                treeCompletedTeam2 += 1;
            }
        }
    }

    public void UpdateGatherResults(int _resourceType, int _team)
    {
        //Gathered Resources (Total)
        if (_resourceType == 1) // Food
        {
            if(_team == 1) //Dijkstra
            {
                foodGatheredTeam1 += 1;
            }
            else if (_team == 2) //A Star
            {
                foodGatheredTeam2 += 1;
            }
        }
        else if (_resourceType == 2) // Wood
        {
            if(_team == 1) // Dijkstra
            {
                treeGatheredTeam1 += 1;
            }
            else if (_team == 2) // A Star
            {
                treeGatheredTeam2 += 1;
            }
        }
    }
}

    //public void UpdateAStarTeam(int _unitCount, int _foodCount, int _treeCount, int _totalFoodCount, int _totalTreeCount)
    //{
    //    aStarUnitCount = _unitCount;
    //    aStarFoodCount = _foodCount;
    //    aStarTreeCount = _treeCount;
    //    aStarFoodTotalCount = _totalFoodCount;
    //    aStarTreeTotalCount = _totalTreeCount;
    //}

    //public void UpdateDijkstraTeam(int _unitCount, int _foodCount, int _treeCount, int _totalFoodCount, int _totalTreeCount)
    //{
    //    dijkstraUnitCount = _unitCount;
    //    dijkstraFoodCount = _foodCount;
    //    dijkstraTreeCount = _treeCount;
    //    dijkstraFoodTotalCount = _totalFoodCount;
    //    dijkstraTreeTotalCount = _totalTreeCount;
    //}