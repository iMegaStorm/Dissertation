using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    [Header("Team Units")]
    public bool isTeam1;
    public bool showPath;
    public float spawnCheckRate = 1.0f;
    public int count = 0;
    public List<Unit> units = new List<Unit>();
    public bool firstUnit = true;
    public bool isTeam1Marked;
    public bool isTeam2Marked;
    public int firstUnitCount;

    [Header("Unit Build Cost")]
    [SerializeField] int unitFoodCost = 7;
    [SerializeField] int unitWoodCost = 10;

    [Header("Resources")]
    public int food;
    public int wood;

    [Header("Components")]
    public GameObject unitPrefab;
    public Transform unitSpawnPosition;

    public static TeamManager instance;

    private void Awake()
    {
        if (isTeam1)
            instance = this;
    }

    private void Start()
    {
        CreateNewUnit();
        
    }

    private void Update()
    {
        if(food >= unitFoodCost && wood >= unitWoodCost)
        {
            //Debug.Log(food + " " + wood);
            CreateNewUnit();
        }
    }

    public void CreateNewUnit()
    {
        if (food - unitFoodCost < 0 && wood - unitWoodCost < 0)
            return;

        if (firstUnitCount < 2)
            firstUnitCount++;

        //if(firstUnitCount < 2)
        //{
        //    firstUnitCount++;
        //    if(isTeam1)
        //        isTeam1Marked = true;
        //    else
        //        isTeam2Marked = true;
        //}

        //if(isTeam1)
        //{
        //    if (firstUnit)
        //    {
        //        isTeam1Marked = true;
        //        firstUnit = false;
        //    }
        //}
        //else
        //{
        //    if (firstUnit)
        //    {
        //        isTeam2Marked = true;
        //        firstUnit = false;
        //    }
        //}
        //firstUnit = false;
        GameObject unitObj;
        Unit unit;

        unitObj = Instantiate(unitPrefab, unitSpawnPosition.position, Quaternion.identity, transform);
        unit = unitObj.GetComponent<Unit>();

        //Sets the Units TeamManager to this one
        unit.team = this;
        units.Add(unit);

        food -= unitFoodCost;
        wood -= unitWoodCost;

        if (isTeam1)
        {
            GameUI.instance.UpdateTeam1UnitCountText(units.Count);
            GameUI.instance.UpdateTeam1FoodText(food);
            GameUI.instance.UpdateTeam1WoodText(wood);

            //Data Output
            GameManager.instance.UpdateTeamCount(1);
        }
        else
        {
            GameUI.instance.UpdateTeam2UnitCountText(units.Count);
            GameUI.instance.UpdateTeam2FoodText(food);
            GameUI.instance.UpdateTeam2WoodText(wood);

            //Data Output
            GameManager.instance.UpdateTeamCount(2);
        }
    }

    public void HarvestResource(int resourceType, int amount)
    {
        if(resourceType == 1) //Food
        {
            if(isTeam1) //Dijkstra
            {
                food += amount;

                //Update UI
                GameUI.instance.UpdateTeam1FoodText(food);

                //Data Output
                GameManager.instance.UpdateGatherResults(1, 1);
            }
            else //A Star
            {
                food += amount;

                //Update UI
                GameUI.instance.UpdateTeam2FoodText(food);

                //Data Output
                GameManager.instance.UpdateGatherResults(1, 2);
            }
        }
        else if (resourceType == 2) //Wood
        {
            if (isTeam1) //Dijkstra
            {
                wood += amount;

                //Update UI
                GameUI.instance.UpdateTeam1WoodText(wood);

                //Data Output
                GameManager.instance.UpdateGatherResults(2, 1);
            }
            else //A Star
            {
                wood += amount;

                //Update UI
                GameUI.instance.UpdateTeam2WoodText(wood);

                //Data Output
                GameManager.instance.UpdateGatherResults(2, 2);
            }
        }

    }


}
